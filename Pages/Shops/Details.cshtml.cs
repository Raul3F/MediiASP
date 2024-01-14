using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Models;

namespace BarberShop.Pages.Shops
{
    public class DetailsModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public DetailsModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        public Shop Shop { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shop.FirstOrDefaultAsync(m => m.ID == id);
            if (shop == null)
            {
                return NotFound();
            }
            else
            {
                Shop = shop;
            }
            return Page();
        }
    }
}
