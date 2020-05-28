using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Restaurant.Helps;
using Restaurant.Models.BusinessLogicLayer;
using Restaurant.Models.Entity;

namespace Restaurant.ViewModels
{
    public class DetailsViewModel : NotifyPropertyChangedHelp
    {
        private ProductsDisplay productsDisplay = new ProductsDisplay();

        public DetailsViewModel(string productName)
        {
            MealBLL mealBLL = new MealBLL();
            productsDisplay = mealBLL.GetProductDetails(productName);
        }

        public string ProductName
        {
            get
            {
                Regex regex = new Regex(@"\s*$");
                return regex.Replace(productsDisplay.Name.ToUpper(), "");
            }
        }

        public string ProductQuantity 
        {
            get
            {
                return productsDisplay.Quantity.ToUpper();
            }
        }

        public string ProductPrice
        {
            get
            {
                return productsDisplay.Price.ToUpper();
            }
        }

        public byte[] Image1
        {
            get
            {
                return productsDisplay.Image1;
            }
        }

        public byte[] Image2
        {
            get
            {
                return productsDisplay.Image2;
            }
        }

        public byte[] Image3
        {
            get
            {
                return productsDisplay.Image3;
            }
        }

        public ObservableCollection<string> Allergens
        {
            get
            {
                return new ObservableCollection<string>(productsDisplay.Allergens);
            }
        }
    }
}
