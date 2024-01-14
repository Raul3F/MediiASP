using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    public class Consumer
    {
        public int ID { get; set; }

        public string Email { get; set; }
        [RegularExpression(@"^[A-Z]+[a-z\s]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau Ana-Maria")]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [RegularExpression(@"^[A-Z]+[a-z\s]*$", ErrorMessage = "Numele de familie trebuie sa inceapa cu majuscula (ex. Pop")]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Telefonul trebuie sa fie de forma '0722-123-123' sau '0722.123.123' sau '0722 123 123'")]
        public string? Phone { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
