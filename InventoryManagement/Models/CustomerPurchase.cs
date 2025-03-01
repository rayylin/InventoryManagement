﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InventoryManagement.Models
{
    public class CustomerPurchase
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string CusPurchaseId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Product ID")]
        [Required(ErrorMessage = "Must fill! ")]
        [MaxLength(60, ErrorMessage = "Too long")]
        public string ProductId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Store Id")]
        [Required(ErrorMessage = "Must fill! ")]
        [MaxLength(60, ErrorMessage = "Too long")]
        public string StoreId { get; set; }

        public int Quantity { get; set; }
        [DisplayName("Unit Cost ($)")]
        public double UnitCost { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Status { get; set; }

        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
    }
}
