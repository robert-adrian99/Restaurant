using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Restaurant.Helps;
using Restaurant.Models;
using Restaurant.Models.BusinessLogicLayer;
using Restaurant.Models.Entity;
using Restaurant.Views;

namespace Restaurant.ViewModels
{
    public class MenuViewModel : NotifyPropertyChangedHelp
    {
        private CategoryBLL categoryBLL = new CategoryBLL();
        private MealBLL mealBLL = new MealBLL();

        public MenuViewModel()
        {
            Categories = new ObservableCollection<string>(categoryBLL.GetCategories());
            for (int index = 0; index < Categories.Count; index++)
            {
                Regex regex = new Regex(@"\s*$");
                Categories[index] = regex.Replace(Categories[index], "");
                Categories[index] = Categories[index].ToUpper();
            }
            SelectedItemCombobox = Categories.First();

            ProductsDisplay = new ObservableCollection<ProductsDisplay>(mealBLL.GetProductsByCategory(SelectedItemCombobox));
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

        private string selectedItemCombobox;
        public string SelectedItemCombobox
        {
            get
            {
                return selectedItemCombobox;
            }
            set
            {
                selectedItemCombobox = value;
                NotifyPropertyChanged("SelectedItemCombobox");
                ProductsDisplay = new ObservableCollection<ProductsDisplay>(mealBLL.GetProductsByCategory(SelectedItemCombobox));
            }
        }

        private ObservableCollection<ProductsDisplay> productsDisplay;
        public ObservableCollection<ProductsDisplay> ProductsDisplay
        {
            get
            {
                return productsDisplay;
            }
            set
            {
                productsDisplay = value;
                NotifyPropertyChanged("ProductsDisplay");
            }
        }

        private ProductsDisplay selectedItemList;
        public ProductsDisplay SelectedItemList
        {
            get
            {
                return selectedItemList;
            }
            set
            {
                selectedItemList = value;
                CanExecuteCommand = true;
                NotifyPropertyChanged("SelectedItemList");
            }
        }

        private bool CanExecuteCommand { get; set; } = false;
        private ICommand seeDetailsCommand;
        public ICommand SeeDetailsCommand
        {
            get
            {
                if (seeDetailsCommand == null)
                {
                    seeDetailsCommand = new RelayCommand(SeeDetails, param => CanExecuteCommand);
                }
                return seeDetailsCommand;
            }
        }

        public void SeeDetails(object param)
        {
            DetailsWindow detailsWindow = new DetailsWindow();
            detailsWindow.ShowDialog();
        }

    }
}
