namespace BarberShop.Models
{
    public class BookingCategory
    {
        public int ID { get; set; }
        public int BookingID { get; set; }
        public Booking Booking { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
