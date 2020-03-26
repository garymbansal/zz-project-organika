using System;
using System.ComponentModel.DataAnnotations;

namespace Organika.ApiService.ViewModels
{
    public class InventoryVM
    {
        public System.Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}