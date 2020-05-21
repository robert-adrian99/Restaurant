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
    public class SignInViewModel : NotifyPropertyChangedHelp
    {

        // UserBLL user = new UserBLL();


        #region DataMembers
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                // TODO: Validation
                email = value;
                NotifyPropertyChanged("Email");
            }
        }
        #endregion


        #region CommandMembers
        private ICommand signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                {
                    signInCommand = new RelayCommand(SignIn);
                }
                return signInCommand;
            }
        }

        public void SignIn(object param)
        {
            string password = (param as PasswordBox).Password;
            // call method from UserBLL to validate the email and password
        }
        #endregion

    }
}
