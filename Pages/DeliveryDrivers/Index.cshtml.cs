using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using DeliveryCart.Models;

namespace DeliveryCart_Customer.Pages.DeliveryDrivers
{
    public class IndexModel : PageModel
    {
        private readonly Data.DeliveryCartContext _context;

        public IndexModel(Data.DeliveryCartContext context)
        {
            _context = context;
        }

        public IList<DeliveryDriver> DeliveryDriver { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DeliveryDriver != null)
            {
                DeliveryDriver = await _context.DeliveryDriver.ToListAsync();
            }
        }
    }
}
