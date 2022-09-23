using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeliveryCart.Models
{
    public class Customer
    {
        public int customerID { get; set; }
        public string? customerName { get; set; }
        public string? customerEmail { get; set; }
        public string? customerAddress { get; set; }
        public int customerPhoneNumber { get; set; }

        public ICollection<Order>? Orders { get; set; }

    }
}