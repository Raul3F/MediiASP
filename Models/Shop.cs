using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    public class Shop
    {
        public int ID { get; set; }
        [Display(Name = "Barber Shop Name")]
        public string Name { get; set; }
        public string Location { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan Start {  get; set; }
        [DataType(DataType.Time)]
        public TimeSpan End { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
