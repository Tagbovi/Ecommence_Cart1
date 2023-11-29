using System;
using System.ComponentModel.DataAnnotations;

namespace ecomence_Cart.CartModel
{
    public class Cart
    {
        [Key]
        public int ItemID { get; set; }
        public string ItemName { get; set; }

        [Required]
        
        public int Quantity { get; set; }

        [Required]
     
        public double UnitPrice { get; set; }
        public int PhoneNumbers { get; set; }
        public DateTime AddedTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpDatedTime { get; set; } = null;
        

    }
}
