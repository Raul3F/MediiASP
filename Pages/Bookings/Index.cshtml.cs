using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Models;
using System.Net;

namespace BarberShop.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public IndexModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;
        public BookingData BookingD { get; set; }
        public int BookingID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookingD = new BookingData();
            BookingD.Bookings = await _context.Booking
            .Include(b => b.Shop)
            .Include(b => b.BookingCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.BookingTime)
            .ToListAsync();
            if (id != null)
            {
                BookingID = id.Value;
                Booking booking = BookingD.Bookings
                .Where(i => i.ID == id.Value).Single();
                BookingD.Categories = booking.BookingCategories.Select(s => s.Category);
            }
        }
    }
}
