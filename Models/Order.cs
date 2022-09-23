using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeliveryCart.Models
{
    public class Order
    {
        public int orderID { get; set; }
        public DateTime orderDate { get; set; }

        public string? customerID { get; set; } //foreign key 
        public List<Item>? orderItems { get; set; } //list of multiple items

        public Customer? Customer { get; set; } // one to many
        public DeliveryDriver? DeliveryDriver { get; set; } // one to many
    }
}