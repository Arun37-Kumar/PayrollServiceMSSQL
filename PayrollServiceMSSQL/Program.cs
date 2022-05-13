using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollServiceMSSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Service");

            EmployeeRepo repo = new EmployeeRepo();
            repo.GetAllEmployees();
            Console.ReadLine();
        }
    }
}
