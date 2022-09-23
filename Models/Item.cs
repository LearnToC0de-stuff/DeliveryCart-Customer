using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeliveryCart.Models
{
    public class Item
    {
        public int itemID { get; set; }

        public string? itemDescription { get; set; }
        public string? itemName { get; set; }
        public double itemPrice { get; set; }

        // Relationships
        public ICollection<Order>? Orders { get; set; } //item can be in multiple different orders

    }
}