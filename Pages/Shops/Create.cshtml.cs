using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberShop.Data;
using BarberShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberShop.Pages.Shops
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public CreateModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Shop Shop { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Shop.Add(Shop);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
