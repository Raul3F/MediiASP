using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Models;

namespace BarberShop.Pages.Consumers
{
    public class EditModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public EditModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Consumer Consumer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer =  await _context.Consumer.FirstOrDefaultAsync(m => m.ID == id);
            if (consumer == null)
            {
                return NotFound();
            }
            Consumer = consumer;
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

            _context.Attach(Consumer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumerExists(Consumer.ID))
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

        private bool ConsumerExists(int id)
        {
            return _context.Consumer.Any(e => e.ID == id);
        }
    }
}
