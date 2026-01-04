using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class IncomeData
    {
        [Key]
        public int IncomeId { get; set; }
        [Required]
        public string IncomeName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }= DateTime.Now;
        public DateTime MonthOfIncome { get; set; }
    }
}


//CREATE TABLE IncomeData
//(
//    IncomeId INT IDENTITY(1,1) PRIMARY KEY,
//    IncomeName NVARCHAR(200) NOT NULL,
//    TotalAmount DECIMAL(18,2) NOT NULL,
//    IsActive INT NOT NULL,
//    CreatedBy INT NOT NULL,
//    CreateDate DATETIME NOT NULL DEFAULT GETDATE(),
//   MonthOfIncome smalldatetime
//);
