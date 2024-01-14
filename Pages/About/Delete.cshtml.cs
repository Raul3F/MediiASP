using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Models;

namespace BarberShop.Pages.About
{
    public class DeleteModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public DeleteModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AboutPage About { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.About.FirstOrDefaultAsync(m => m.ID == id);

            if (about == null)
            {
                return NotFound();
            }
            else
            {
                About = about;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.About.FindAsync(id);
            if (about != null)
            {
                About = about;
                _context.About.Remove(About);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
