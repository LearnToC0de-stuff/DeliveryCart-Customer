using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeliveryCart.Models
{
    public class Customer
    {
        public int customerID { get; set; }
        [Required]
        [Display(Name = "Customer's full name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Please include between two and 25 letters")]
        public string? customerName { get; set; }
        [Required]
        [Display(Name = "Customer's email address")]
        [EmailAddress]
        public string? customerEmail { get; set; }
        [Required]
        [Display(Name = "Customer's home address")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Please include between three and 40 letters")]
        public string? customerAddress { get; set; }
        [Required]
        [Display(Name = "Customer's phone number")]
    
        public int customerPhoneNumber { get; set; }

        public ICollection<Order>? Orders { get; set; }

    }
}