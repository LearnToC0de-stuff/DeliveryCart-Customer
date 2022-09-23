using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeliveryCart.Models
{
    public class DeliveryDriver
    {
        public int deliveryDriverID { get; set; }
        public string? deliveryDriverName { get; set; }
        public int? deliveryDriverPhoneNumber { get; set; }

        public int orderID { get; set; } // Foreign Key
        // Relationships
        public ICollection<Order>? Orders { get; set; } // One driver to many orders

    }
}