using Restaurant.Models.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Restaurant.Models.BusinessLogicLayer
{
    class MealBLL
    {
        private double MenuPriceAfterDiscount(double price)
        {
            double discount = double.Parse(ConfigurationManager.AppSettings.Get("menuDiscount"));
            return price - discount / 100 * price;
        }

        public List<ProductsDisplay> GetProductsByCategory(string category)
        {
            List<ProductsDisplay> productsDisplays = new List<ProductsDisplay>();
            RestaurantEntities restaurantEntities = new RestaurantEntities();

            var query = (from product in restaurantEntities.Product
                         where product.Category.Name.Equals(category)
                         select new ProductsDisplay
                         {
                             Name = product.Name,
                             Quantity = product.Quantity.ToString(),
                             Price = product.Price.ToString()
                         }
                         )?.ToList();
            productsDisplays.AddRange(query);

            var query1 = restaurantEntities.GetAllMenusWithPriceByCategory(category).ToList();
            foreach (var menu in query1)
            {
                productsDisplays.Add(new ProductsDisplay()
                {
                    Name = menu.Name,
                    Quantity = (menu.Quantity ?? 0).ToString(),
                    Price = MenuPriceAfterDiscount(menu.Price ?? 0).ToString()
                });

            }

            return productsDisplays;
        }
    }
}
