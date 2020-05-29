using Restaurant.Helps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models.Entity
{
    public class ProductsInCart : NotifyPropertyChangedHelp
    {
        public ProductTypeEnum ProductType { get; set; }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
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

        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                NotifyPropertyChanged("Price");
            }
        }

        private double totalPrice;
        public double TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                NotifyPropertyChanged("TotalPrice");
            }
        }
    }
}
