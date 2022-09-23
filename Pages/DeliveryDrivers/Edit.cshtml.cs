using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using DeliveryCart.Models;

namespace DeliveryCart_Customer.Pages.DeliveryDrivers
{
    public class EditModel : PageModel
    {
        private readonly Data.DeliveryCartContext _context;

        public EditModel(Data.DeliveryCartContext context)
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

            var deliverydriver =  await _context.DeliveryDriver.FirstOrDefaultAsync(m => m.deliveryDriverID == id);
            if (deliverydriver == null)
            {
                return NotFound();
            }
            DeliveryDriver = deliverydriver;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DeliveryDriver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryDriverExists(DeliveryDriver.deliveryDriverID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeliveryDriverExists(int id)
        {
          return (_context.DeliveryDriver?.Any(e => e.deliveryDriverID == id)).GetValueOrDefault();
        }
    }
}
