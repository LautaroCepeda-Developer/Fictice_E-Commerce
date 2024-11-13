using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CompanyData
{
    public class CompanyDataDTO
    {
        public int Id { get; set; } = 1;
        public required string CompanyName { get; set; }

        public required string CompanyLegalAddres { get; set; }

        public required string EmpressBankAccount { get; set; }
        public decimal Tax { get; set; } 
    }
}
