using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Helps;
using Restaurant.Models.Entity;

namespace Restaurant.ViewModels
{
    public class SeeCartViewModel : NotifyPropertyChangedHelp
    {

        public SeeCartViewModel(List<ProductsDisplay> productsDisplays)
        {
            foreach (var product in productsDisplays)
            {
                bool ok = true;
                foreach (var product1 in ProductsInCarts)
                {
                    if (product.Name == product1.Name)
                    {
                        product1.Quantity = (int.Parse(product1.Quantity) + 1).ToString();
                        product1.Price = (double.Parse(product.Price) + double.Parse(product.Price)).ToString();
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    ProductsInCarts.Add(new ProductsInCart()
                    {
                        Name = product.Name,
                        Quantity = "1",
                        Price = product.Price
                    });

                }
            }
        }

        #region DataMembers
        private ObservableCollection<ProductsInCart> productsInCarts = new ObservableCollection<ProductsInCart>();
        public ObservableCollection<ProductsInCart> ProductsInCarts
        {
            get
            {
                return productsInCarts;
            }
            set
            {
                productsInCarts = value;
                NotifyPropertyChanged("ProductsInCarts");
            }
        }
        #endregion
    }
}
