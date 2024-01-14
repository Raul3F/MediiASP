using Microsoft.AspNetCore.Mvc.RazorPages;
using BarberShop.Data;

namespace BarberShop.Models
{
    public class BookingCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(BarberShopContext context,
        Booking booking)
        {
            var allCategories = context.Category;
            var bookingCategories = new HashSet<int>(
            booking.BookingCategories.Select(c => c.CategoryID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookingCategories.Contains(cat.ID)
                });
            }
        }

        public void UpdateBookingCategories(BarberShopContext context, string[] selectedCategories, Booking bookingToUpdate)
        {
            if (selectedCategories == null)
            {
                bookingToUpdate.BookingCategories = new List<BookingCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookingCategories = new HashSet<int>(bookingToUpdate.BookingCategories.Select(c => c.Category.ID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookingCategories.Contains(cat.ID))
                    {
                        bookingToUpdate.BookingCategories.Add(new BookingCategory
                        {
                            BookingID = bookingToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                    else
                    {
                        if (bookingCategories.Contains(cat.ID))
                        {
                            BookingCategory courseToRemove = bookingToUpdate.BookingCategories.SingleOrDefault(i => i.CategoryID == cat.ID);
                            context.Remove(courseToRemove);
                        }
                    }
                }
            }
        }
    }
}
