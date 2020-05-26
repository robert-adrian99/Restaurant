using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models.BusinessLogicLayer
{
    class UserBLL
    {
        private RestaurantEntities restaurantEntities = new RestaurantEntities();

        public bool SignIn(string email, string password)
        {
            try
            {
                var userQuery = (from user in restaurantEntities.Users
                                 where user.Email.Equals(email) && user.Password.Equals(password)
                                 select user).First();

                var query = (from user in restaurantEntities.Users
                             select user)?.ToList();

                foreach (var userInList in query)
                {
                    userInList.Active = false;

                    restaurantEntities.Users.Attach(userInList);
                    restaurantEntities.Entry(userInList).Property(x => x.Active).IsModified = true;
                    restaurantEntities.SaveChanges();
                }

                userQuery.Active = true;

                restaurantEntities.Users.Attach(userQuery);
                restaurantEntities.Entry(userQuery).Property(x => x.Active).IsModified = true;
                restaurantEntities.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
            return true;

            //var query = restaurantEntities.GetAllUsers(); // procedura stocata
        }

        public bool SignUp(string firstName, string lastName, string email, string phone, string address, string password)
        {
            try
            {
                var query = (from user in restaurantEntities.Users
                             select user)?.ToList();

                foreach (var userInList in query)
                {
                    if (userInList.Email == email)
                    {
                        return false;
                    }
                }

                foreach (var userInList in query)
                {
                    userInList.Active = false;

                    restaurantEntities.Users.Attach(userInList);
                    restaurantEntities.Entry(userInList).Property(x => x.Active).IsModified = true;
                    restaurantEntities.SaveChanges();
                }
            }
            finally
            {
                User newUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    Password = password,
                    Active = true
                };

                restaurantEntities.Users.Add(newUser);
                restaurantEntities.SaveChanges();
            }
            return true;
        }
    }
}
