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
        private RestaurantEntities restaurantEntities = new RestaurantEntities();

        private double MenuPriceAfterDiscount(double price)
        {
            double discount = double.Parse(ConfigurationManager.AppSettings.Get("menuDiscount"));
            return price - discount / 100 * price;
        }

        public List<ProductsDisplay> GetProductsByCategory(string category)
        {
            List<ProductsDisplay> productsDisplays = new List<ProductsDisplay>();

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

        public ProductsDisplay GetProductDetails(string productName)
        {
            try
            {
                var query = (from product in restaurantEntities.Product
                             where product.Name.Equals(productName)
                             select new ProductsDisplay
                             {
                                 Name = product.Name,
                                 Quantity = product.Quantity.ToString(),
                                 Price = product.Price.ToString(),
                                 Image1 = product.Image1,
                                 Image2 = product.Image2,
                                 Image3 = product.Image3,
                                 ProductType = ProductTypeEnum.Product
                             }).First();
                query.Allergens = restaurantEntities.GetAllergensByProduct(productName).ToList();

                return query;
            }
            catch
            {
                var query = (from menu in restaurantEntities.Menu
                             where menu.Name.Equals(productName)
                             select new ProductsDisplay
                             {
                                 Name = menu.Name,
                                 Quantity = (from menu_product in restaurantEntities.Menu_Product
                                             where menu.MenuID.Equals(menu_product.MenuID)
                                             select menu_product.Quantity).Sum().ToString(),
                                 Price = (from product in restaurantEntities.Product
                                          join menu_product in restaurantEntities.Menu_Product
                                          on product.ProductID equals menu_product.ProductID
                                          where menu.MenuID.Equals(menu_product.MenuID)
                                          select product.Price).Sum().ToString(),
                                 Image1 = menu.Image1,
                                 Image2 = menu.Image2,
                                 Image3 = menu.Image3,
                                 ProductType = ProductTypeEnum.Menu
                             }).First();
                query.Allergens = restaurantEntities.GetAllergensByMenu(productName).ToList();
                return query;
            }
        }
    }
}
