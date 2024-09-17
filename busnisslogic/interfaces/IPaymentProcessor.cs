using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.interfaces
{
    public interface IPaymentProcessor
    {
        public  Task ProcessPayment(int studentId, int amount);
    }
}
