using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Models;

namespace BarberShop.Pages.Consumers
{
    public class DetailsModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public DetailsModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        public Consumer Consumer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer = await _context.Consumer.FirstOrDefaultAsync(m => m.ID == id);
            if (consumer == null)
            {
                return NotFound();
            }
            else
            {
                Consumer = consumer;
            }
            return Page();
        }
    }
}
