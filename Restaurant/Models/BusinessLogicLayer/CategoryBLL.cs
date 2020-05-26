using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models.BusinessLogicLayer
{
    class CategoryBLL
    {
        private readonly RestaurantEntities restaurantEntities = new RestaurantEntities();

        public List<string> GetCategories()
        {
            var query = (from category in restaurantEntities.Categories
                         select category.Name)?.ToList();
            return query;
        }
    }
}
