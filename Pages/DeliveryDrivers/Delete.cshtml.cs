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
    public class DeleteModel : PageModel
    {
        private readonly Data.DeliveryCartContext _context;

        public DeleteModel(Data.DeliveryCartContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DeliveryDriver DeliveryDriver { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeliveryDriver == null)
            {
                return NotFound();
            }

            var deliverydriver = await _context.DeliveryDriver.FirstOrDefaultAsync(m => m.deliveryDriverID == id);

            if (deliverydriver == null)
            {
                return NotFound();
            }
            else 
            {
                DeliveryDriver = deliverydriver;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DeliveryDriver == null)
            {
                return NotFound();
            }
            var deliverydriver = await _context.DeliveryDriver.FindAsync(id);

            if (deliverydriver != null)
            {
                DeliveryDriver = deliverydriver;
                _context.DeliveryDriver.Remove(DeliveryDriver);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
