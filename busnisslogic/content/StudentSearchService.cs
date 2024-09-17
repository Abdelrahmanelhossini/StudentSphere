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
using System.Threading.Tasks.Dataflow;

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
            string? studentName=null,
            int? level = null,
            int? minDegrees = null,
            int? maxDegrees = null,
            int? minRank = null,
            int? maxRank = null)
        {
            var query = from enroll in _context.enroll
                        select enroll;

            #region Search by Name
            if (!string.IsNullOrWhiteSpace(studentName))
            {
                query = from enroll in query
                        join student in _context.Students on enroll.StudentId equals student.StudentId
                        where student.Name.Contains(studentName)
                        select enroll;

            }

            #endregion


            #region Search by level
            if (level.HasValue)
            {
                query = from enroll in query
                        join Student in _context.Students on enroll.StudentId equals Student.StudentId
                        //join Level in _context.levels on Student.Levelid equals Level.LevelId
                        where Student.Levelid == level.Value
                        select enroll;
            }

            #endregion

            var enrolledData = query
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();

            var group = from enroll in enrolledData
                        join Student in _context.Students on enroll.StudentId equals Student.StudentId
                        join Course in _context.courses on enroll.CourseId equals Course.CourseId
                        group enroll by new { Student.StudentId } into g
                        select new StudentSearchResult
                        {
                            StudentName = g.FirstOrDefault().Student.Name,
                            CourseGrades = g.Select(r => new
                            {
                                CourceName=r.Course.CourseName,
                                Degree = r.degree.HasValue ? (int)r.degree.Value : 0
                            }).ToDictionary(x => x.CourceName, x => x.Degree),
                            TotalDegrees = g.Sum(x => x.degree.HasValue ? x.degree.Value : 0)
                        };




            
            


            var results = group.ToList();


            //var rankquery = from result in results

            //                orderby result.TotalDegrees descending

            //                select new StudentSearchResult
            //                {
            //                    StudentName = result.StudentName,
            //                    CourseGrades = result.CourseGrades,
            //                    TotalDegrees = result.TotalDegrees,
            //                    Rank = results.IndexOf(result)+1
            //                };
            //if (minDegrees.HasValue || maxDegrees.HasValue)
            //{
            //    rankquery = from result in rankquery

            //                where (!minRank.HasValue || result.Rank >= minRank.Value)
            //                       && (!maxRank.HasValue || result.Rank <= maxRank.Value)
            //                select result;

            //}
            var rankedResults = (from result in results
                                 orderby result.TotalDegrees descending
                                 select result).ToList();

            
            var rankquery = (from result in rankedResults
                             let rank = rankedResults.IndexOf(result)+1
                             
                             select new StudentSearchResult
                             {
                                 StudentName = result.StudentName,
                                 CourseGrades = result.CourseGrades,
                                 TotalDegrees = result.TotalDegrees,
                                 Rank = rank
                             }).ToList();

            
            if (minDegrees.HasValue || maxDegrees.HasValue)
            {
                rankquery = (from result in rankquery
                             where (!minDegrees.HasValue || result.TotalDegrees >= minDegrees.Value) &&
                                   (!maxDegrees.HasValue || result.TotalDegrees <= maxDegrees.Value)
                             select result).ToList();
            }



            return rankquery.ToList();


        }
    }
}





