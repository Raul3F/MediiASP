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
    public class IndexModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public IndexModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        public IList<Consumer> Consumer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Consumer = await _context.Consumer.ToListAsync();
        }
    }
}
