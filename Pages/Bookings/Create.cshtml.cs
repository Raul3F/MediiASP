using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberShop.Data;
using BarberShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Pages.Bookings
{
    public class CreateModel : BookingCategoriesPageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;

        public CreateModel(BarberShop.Data.BarberShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            //var currentUser = User?.Identity?.Name;

            var bookingList = _context.Booking
                //.Include(b => b.BookingTime)
                .Select(x => new
                {
                    x.ID,
                    Bookingdt = "You have a booking " + x.BookingTime + " at " + x.Shop.Name + " for " + x.BookingCategories
                });

            ViewData["ShopID"] = new SelectList(_context.Shop, "ID", "Name");

            //ViewData["User"] = currentUser;

            //var consumer = _context.Consumer.FirstOrDefault(c => c.Email == currentUser);

            //ViewData["Consumer"] = consumer?.ID;

            var booking = new Booking();

            booking.BookingCategories = new List<BookingCategory>();

            PopulateAssignedCategoryData(_context, booking);

            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var currentUser = User?.Identity?.Name;

            if (currentUser != null)
            {
                var consumer = _context.Consumer.FirstOrDefault(c => c.Email == currentUser);

                if (consumer != null)
                {
                    Booking.ConsumerID = consumer.ID;
                }
            }

            var newBooking = new Booking();

            newBooking.Status = "Active";
            newBooking.Shop = await _context.Shop.FindAsync(Booking.ShopID);

            if (selectedCategories != null)
            {
                newBooking.BookingCategories = new List<BookingCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookingCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBooking.BookingCategories.Add(catToAdd);
                }
            }
            Booking.BookingCategories = newBooking.BookingCategories;
            Booking.Status = newBooking.Status;
            
            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }

}
