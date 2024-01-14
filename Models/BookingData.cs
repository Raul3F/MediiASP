namespace BarberShop.Models
{
    public class BookingData
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<BookingCategory> BookingCategories { get; set; }
    }
}
