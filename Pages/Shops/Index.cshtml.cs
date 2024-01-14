using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShop.Data;
using BarberShop.Models;
using BarberShop.ViewModels;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Pages.Shops
{
    public class IndexModel : PageModel
    {
        private readonly BarberShop.Data.BarberShopContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(BarberShop.Data.BarberShopContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Shop> Shop { get;set; } = default!;

        public ShopIndexData ShopData { get; set; }
        public int ShopID { get; set; }
        public int BookID { get; set; }

        public string LocationSort { get; set; }
        public string NameSort { get; set; }

        public string CurrentFilter { get; set; }

        public string UserRole { get; set; }

        public async Task OnGetAsync(int? id, int? bookingID, string sortOrder, string searchString)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            //UserRole = GetUserRoleFromDatabase(userId);

            ShopData = new ShopIndexData();

            LocationSort = String.IsNullOrEmpty(sortOrder) ? "location_cresc" : "";
            NameSort = sortOrder == "name_cresc" ? "name_descr" : "name_cresc";

            CurrentFilter = searchString;

            ShopData.Shops = await _context.Shop
            .Include(i => i.Bookings)
            .OrderBy(i => i.Name)
            .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                ShopData.Shops = ShopData.Shops.Where(s => s.Name.Contains(searchString)
                                                        || s.Location.Contains(searchString));
            }

            if (id != null)
            {
                ShopID = id.Value;
                Shop Shop = ShopData.Shops
                .Where(i => i.ID == id.Value).Single();
                ShopData.Bookings = Shop.Bookings;
            }

            switch (sortOrder)
            {
                case "location_cresc":
                    ShopData.Shops = ShopData.Shops.OrderBy(s => s.Location);
                    break;

                case "name_cresc":
                    ShopData.Shops = ShopData.Shops.OrderBy(s => s.Name);
                    break;

                case "name_descr":
                    ShopData.Shops = ShopData.Shops.OrderByDescending(s => s.Name);
                    break;

                default:
                    ShopData.Shops = ShopData.Shops.OrderBy(s => s.ID);
                    break;
            }

            ViewData["UserRole"] = UserRole;
        }

    }
}
