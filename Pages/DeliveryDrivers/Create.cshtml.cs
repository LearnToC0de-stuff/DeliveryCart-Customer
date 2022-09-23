using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using DeliveryCart.Models;

namespace DeliveryCart_Customer.Pages.DeliveryDrivers
{
    public class CreateModel : PageModel
    {
        private readonly Data.DeliveryCartContext _context;

        public CreateModel(Data.DeliveryCartContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DeliveryDriver DeliveryDriver { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.DeliveryDriver == null || DeliveryDriver == null)
            {
                return Page();
            }

            _context.DeliveryDriver.Add(DeliveryDriver);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
