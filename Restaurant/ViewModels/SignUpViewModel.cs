using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Restaurant.Helps;

namespace Restaurant.ViewModels
{
    public class SignUpViewModel : NotifyPropertyChangedHelp
    {

        // UserBLL user = new UserBLL();


        #region DataMembers
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                // validation
                firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                // validation
                lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                // validation
                email = value;
                NotifyPropertyChanged("Email");
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                // validation
                phoneNumber = value;
                NotifyPropertyChanged("PhoneNumber");
            }
        }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                NotifyPropertyChanged("Address");
            }
        }
        #endregion


        #region CommandMembers
        private ICommand signUpCommand;
        public ICommand SignUpCommand
        {
            get
            {
                if (signUpCommand == null)
                {
                    signUpCommand = new RelayCommand(SignUp);
                }
                return signUpCommand;
            }
        }

        public void SignUp(object param)
        {
            string password = (param as PasswordBox).Password;
            // call method from UserBLL to add the user
        }
        #endregion
    }
}
