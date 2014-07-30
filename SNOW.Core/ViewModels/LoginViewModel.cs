using Cirrious.MvvmCross.ViewModels;
using SNOW.Core.Services;
using System.Windows.Input;

namespace SNOW.Core.ViewModels
{
    public class LoginViewModel
        : MvxViewModel
    {
        private readonly ILoginService _loginservice;

        public LoginViewModel(ILoginService loginservice)
        {
            _loginservice = loginservice;
        }

        private string _domain;
        public string Domain
        {
            get { return _domain; }
            set { _domain = value; RaisePropertyChanged(() => Domain); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(() => UserName); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(() => Password); }
        }

        private MvxCommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                _loginCommand = _loginCommand ?? new MvxCommand(ExecuteLogin);
                return _loginCommand;
            }
        }

        private void ExecuteLogin()
        {
//            throw new System.NotImplementedException();
            _loginservice.AttemptLogin(Domain, UserName, Password, successs => { }, error => { });

                
        }

    }
}
