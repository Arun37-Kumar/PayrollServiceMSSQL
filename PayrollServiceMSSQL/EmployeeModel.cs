using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollServiceMSSQL
{
    class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Basic_Pay { get; set; }
        public DateTime Startdate { get; set; }
        public char Gender { get; set; }
        public long Phone { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public double Deductions { get; set; }
        public double Taxable_Pay { get; set; }
        public double Income_Tax { get; set; }
        public double Net_Pay { get; set; }
        public int Dept_Id { get; set; }

    }
}
