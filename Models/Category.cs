namespace BarberShop.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<BookingCategory>? BookingCategories { get; set; }
    }
}
