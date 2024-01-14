using System.Security.Policy;
using BarberShop.Models;

namespace BarberShop.ViewModels
{
    public class ShopIndexData
    {
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
