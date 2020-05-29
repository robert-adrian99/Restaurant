using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Helps;
using Restaurant.Models.Entity;

namespace Restaurant.Models.BusinessLogicLayer
{
    class OrderBLL
    {
        private RestaurantEntities restaurantEntities = new RestaurantEntities();

        public void AddOrder(double price, List<ProductsInCart> productsInCart)
        {
            var userQuery = (from user in restaurantEntities.User
                         where user.Active.Equals(true)
                         select user).First();

            Order newOrder = new Order()
            {
                OrderNumber = Properties.Settings.Default.OrderNumber++,
                UserID = userQuery.UserID,
                Status = OrderStatus.Registerd.ToString(),
                Date = DateTime.Now,
                Price = price
            };
            Properties.Settings.Default.Save();

            restaurantEntities.Order.Add(newOrder);

            foreach (var product in productsInCart)
            {
                if (product.ProductType == ProductTypeEnum.Product)
                {
                    for (int numberOfProducts = 0; numberOfProducts < product.Quantity; numberOfProducts++)
                    {
                        restaurantEntities.Order_Product.Add(new Order_Product
                        {
                            OrderID = newOrder.OrderID,
                            ProductID = (from productEntity in restaurantEntities.Product
                                         where productEntity.Name.Equals(product.Name)
                                         select productEntity.ProductID).First()
                        });
                    }
                }
                else
                {
                    for (int numberOfMenus = 0; numberOfMenus < product.Quantity; numberOfMenus++)
                    {
                        restaurantEntities.Order_Menu.Add(new Order_Menu
                        {
                            OrderID = newOrder.OrderID,
                            MenuID = (from menu in restaurantEntities.Menu
                                      where menu.Name.Equals(product.Name)
                                      select menu.MenuID).First()
                        });
                    }
                }
            }

            restaurantEntities.SaveChanges();
        }


    }
}
