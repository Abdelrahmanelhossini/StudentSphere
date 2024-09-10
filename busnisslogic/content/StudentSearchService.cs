using busnisslogic.interfaces;
using domain_and_repo;
using domain_and_repo.DTO;
using domain_and_repo.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.content
{
    public class StudentSearchService : IStudentSearchService
    {


        private readonly Db_context _context;

        public StudentSearchService(Db_context context)
        {
            _context = context;
        }
        public List<StudentSearchResult> SearchStudents(
            string? studentName,
            int? level = null,
            int? minDegrees = null,
            int? maxDegrees = null,
            int? minRank = null,
            int? maxRank = null)
        {
            
            var query = _context.enroll
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .AsQueryable();// بدل استخدام بدل متحمل الداتا كلها علي الميموري.AsEnumerable()  

            #region Search by Name
            if (!string.IsNullOrWhiteSpace(studentName))
            {
                query = query.Where(enroll => enroll.Student.Name.Contains(studentName));
            }
            #endregion

            #region Search by Level
            if (level.HasValue)
            {
                query = query.Where(enroll => enroll.Student.Levelid == level.Value);
            }
            #endregion

            
            var enrolledData = query.ToList();


            var groupedQuery = enrolledData
            .GroupBy(e => new { e.Student.StudentId, e.Student.Name })
            .Select(g =>
            {
            var studentResult = new StudentSearchResult
            {
                StudentName = g.Key.Name,
                CourseGrades = g.Aggregate(
                    new Dictionary<string, int>(),
                    (dict, x) =>
                    {
                        dict[x.Course.CourseName] = x.degree.HasValue ? (int)x.degree.Value : 0;
                        return dict;
                    }),
                TotalDegrees = g.Sum(x => x.degree ?? 0)
            };
            return studentResult;
        })
            .ToList();

            #region Filter by Degrees
            if (minDegrees.HasValue || maxDegrees.HasValue)
            {
                groupedQuery = groupedQuery
                    .Where(result =>
                        (!minDegrees.HasValue || result.TotalDegrees >= minDegrees.Value) &&
                        (!maxDegrees.HasValue || result.TotalDegrees <= maxDegrees.Value)
                    )
                    .ToList();
            }
            #endregion

            var rankedStudents = groupedQuery
                .OrderByDescending(result => result.TotalDegrees)
                .Select((result, index) => new StudentSearchResult
                {
                    StudentName = result.StudentName,
                    CourseGrades = result.CourseGrades, 
                    TotalDegrees = result.TotalDegrees,
                    Rank = index + 1
                })
                .ToList();

            #region  Filter by Rank
            if (minRank.HasValue || maxRank.HasValue)
            {
                rankedStudents = rankedStudents
                    .Where(student =>
                        (!minRank.HasValue || student.Rank >= minRank.Value) &&
                        (!maxRank.HasValue || student.Rank <= maxRank.Value)
                    )
                    .ToList();
            }
            #endregion

            return rankedStudents;
        }

    }
}









/*public List<StudentSearchResult> SearchStudents(string? studentName, int? level = null, int? minDegrees = null,
    int? maxDegrees = null, int? minRank = null, int? maxRank = null)
{
    var query = from student in _context.Students
                select student;

    #region Search by Name
    if (!string.IsNullOrWhiteSpace(studentName))
    {
        query = from student in query
                where student.Name.Contains(studentName)
                select student;
    }
    #endregion 

    #region Search By Level
    if (level.HasValue)
    {
        query = from student in query
                where student.Levelid == level
                select student;
    } 
    #endregion 


    var query = _context.enroll
        .Include(e => e.Student)

        .Include(e => e.Course)
        .Where(e =>
            (string.IsNullOrEmpty(studentName) || e.Student.Name.Contains(studentName)) &&
            (!level.HasValue || e.Student.Levelid == level.Value))
        .GroupBy(e => new { e.Student.StudentId, e.Student.Name })
        .AsEnumerable()  
        .Select(g => new
        {

            StudentName = g.Key.Name,
            CourseGrades = g.ToDictionary(x => x.Course.CourseName, x => x.degree),
            TotalDegrees = g.Sum(x => x.degree ?? 0)
        })
        .Where(s =>
            (!minDegrees.HasValue || s.TotalDegrees >= minDegrees.Value) &&
            (!maxDegrees.HasValue || s.TotalDegrees <= maxDegrees.Value))
        .ToList();


    var rankedStudents = query
        .OrderByDescending(s => s.TotalDegrees)
        .Select((s, index) => new StudentSearchResult
        {

            StudentName = s.StudentName,
            CourseGrades = s.CourseGrades.ToDictionary(
                k => k.Key,
                v => v.Value != null ? Convert.ToInt32(v.Value) : 0
            ),
            TotalDegrees = (int)s.TotalDegrees,
            Rank = index + 1
        })
        .Where(s =>
            (!minRank.HasValue || s.Rank >= minRank.Value) &&
            (!maxRank.HasValue || s.Rank <= maxRank.Value))
        .ToList();

    return rankedStudents;


    //return query.AsNoTracking().ToList();
}
}
}*/
