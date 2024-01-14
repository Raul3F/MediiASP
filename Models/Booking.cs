using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    public class Booking
    {
        public int ID { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Booking Detail")]
        public DateTime BookingTime { get; set; }
        public int? ShopID { get; set; }
        public Shop? Shop { get; set; }

        public int? ConsumerID { get; set; }
        public Consumer? Consumer { get; set; }
        public ICollection<BookingCategory>? BookingCategories { get; set; }
    }
}
