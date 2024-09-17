using busnisslogic.interfaces;
using domain_and_repo;
using domain_and_repo.models;
using domain_and_repo.UnitofWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.content
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly Db_context context;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentProcessor(Db_context _context, IUnitOfWork unitOfWork)
        {
            context = _context;
            _unitOfWork = unitOfWork;
        }
        public async Task ProcessPayment(int studentId, int amount)
        {
            var stud = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (stud == null)
                throw new Exception("Student not found");


            var Level = await _unitOfWork.Levels.GetByIdAsync(stud.Levelid);

            if (amount > Level.ValuePaid)
                throw new Exception("Amount exceeds level price");

           
            

            

            // add payment
            var payment = new Payment
            {
                StudentId = studentId,
                LevelId = Level.LevelId,
                PaidValue = amount,

                PymentDate = DateTime.UtcNow
            };

            context.payments.Add(payment);

           

            stud.TotalFees -= amount;

            

            context.Students.Update(stud);

            await context.SaveChangesAsync();



        }
    }
}

