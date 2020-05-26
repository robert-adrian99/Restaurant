using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using Restaurant.Helps;
using Restaurant.Models;
using Restaurant.Models.BusinessLogicLayer;

namespace Restaurant.ViewModels
{
    public class MenuViewModel : NotifyPropertyChangedHelp
    {
        private CategoryBLL categoryBLL = new CategoryBLL();

        public MenuViewModel()
        {
            Categories = new ObservableCollection<string>(categoryBLL.GetCategories());
            for (int index = 0; index < Categories.Count; index++)
            {
                Regex regex = new Regex(@"\s");
                Categories[index] = regex.Replace(Categories[index], "");
                Categories[index] = Categories[index].ToUpper();
            }
            SelectedItem = Categories.First();
        }

        private string productName;
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
                NotifyPropertyChanged("ProductName");
            }
        }

        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                NotifyPropertyChanged("Quantity");
            }
        }

        public ObservableCollection<string> Categories { get; }

        private string selectedItem;
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }
    }
}
