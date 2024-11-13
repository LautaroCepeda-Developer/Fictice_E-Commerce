using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CompanyData
{
    public class CompanyDataUpdateDTO
    {
        public string CompanyName { get; set; }
        public string CompanyLegalAddres { get; set; }
        public string EmpressBankAccount { get; set; }
        public decimal Tax { get; set; }
    }
}
