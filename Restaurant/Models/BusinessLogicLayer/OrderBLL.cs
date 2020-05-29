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
        private User activeUser = new User();

        public OrderBLL()
        {
            activeUser = (from user in restaurantEntities.User
                          where user.Active
                          select user).First();
        }

        public void AddOrder(double price, List<ProductsInCart> productsInCart)
        {
            Order newOrder = new Order()
            {
                OrderNumber = Properties.Settings.Default.OrderNumber++,
                UserID = activeUser.UserID,
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

        public List<OrdersDisplay> GetActiveOrders()
        {
            var activeOrders = (from order in restaurantEntities.Order
                                where !order.Status.Equals(OrderStatus.Canceled.ToString())
                                && !order.Status.Equals(OrderStatus.Delivered.ToString())
                                && order.UserID.Equals(activeUser.UserID)
                                select new OrdersDisplay
                                {
                                    OrderNumber = order.OrderNumber,
                                    Price = order.Price,
                                    Date = order.Date,
                                    Status = order.Status
                                }).ToList();

            foreach (var order in activeOrders)
            {
                order.EstimatedDate = order.Date.AddMinutes(Constants.DeliveryTime);
            }
            return activeOrders;
        }

        public List<OrdersDisplay> GetAllOrders()
        {
            var allOrders = (from order in restaurantEntities.Order
                             where order.UserID.Equals(activeUser.UserID)
                             select new OrdersDisplay
                             {
                                 OrderNumber = order.OrderNumber,
                                 Price = order.Price,
                                 Date = order.Date,
                                 Status = order.Status
                             }).ToList();

            foreach (var order in allOrders)
            {
                order.EstimatedDate = order.Date.AddMinutes(Constants.DeliveryTime);
            }
            return allOrders;
        }
    }
}
