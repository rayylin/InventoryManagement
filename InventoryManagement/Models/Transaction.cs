using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Column(TypeName ="nvarchar(12)")]
        public string AccountNumber { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string BeneficiaryName { get; set; }
        [Column(TypeName = "nvarchar(11)")]
        public string BankName { get; set; }
        public string SWIFT { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
