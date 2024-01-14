using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Models;

namespace BarberShop.Pages.Bookings
{
    public class EditModel : BookingCategoriesPageModel
    {
        private readonly BarberShopContext _context;

        public EditModel(BarberShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await _context.Booking
                .Include(b => b.Shop)
                .Include(b => b.BookingCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Booking == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Booking);

            ViewData["ShopID"] = new SelectList(_context.Set<Shop>(), "ID", "Name", Booking.ShopID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingToUpdate = await _context.Booking
                .Include(i => i.Shop)
                .Include(i => i.BookingCategories).ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (bookingToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                bookingToUpdate,
                "Booking",
                i => i.Status,
                i => i.BookingTime,
                i => i.ShopID,
                i => i.Shop))
            {
                bookingToUpdate.BookingCategories.Clear();
                UpdateBookingCategories(_context, selectedCategories.Split(','), bookingToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(bookingToUpdate.ID))
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

            // Model state is not valid, so populate data and return to the page
            PopulateAssignedCategoryData(_context, bookingToUpdate);
            ViewData["ShopID"] = new SelectList(_context.Set<Shop>(), "ID", "Name", bookingToUpdate.ShopID);

            return Page();
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.ID == id);
        }
    }
}
