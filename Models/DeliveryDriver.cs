using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeliveryCart.Models
{
    public class DeliveryDriver
    {
        public int deliveryDriverID { get; set; }
        [Required]
        [Display (Name = "Driver's full name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Please include between two and 25 letters")]
        public string? deliveryDriverName { get; set; }
        [Required]
        [Display (Name = "Driver's phone number")]
        public int? deliveryDriverPhoneNumber { get; set; }

        //public int orderID { get; set; } // Foreign Key
        // Relationships
        public ICollection<Order>? Orders { get; set; } // One driver to many orders

    }
}