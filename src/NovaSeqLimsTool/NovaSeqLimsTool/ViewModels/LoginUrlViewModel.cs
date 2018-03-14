using NovaSeqLimsTool.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaSeqLimsTool.ViewModels
{
    public class LoginUrlViewModel : ViewModelBase
    {
        #region Fields

        private string _loginUrl;

        #endregion

        #region Properties

        public string LoginUrl
        {
            get => _loginUrl;
            set => SetProperty(ref _loginUrl, value);
        }

        #endregion

        #region ICommands
        public ICommand GetLoginUrl => new AsyncCommand(RetrieveLoginUrl, () => true);
        #endregion

        #region Constructor 
        public LoginUrlViewModel(LimsService state) : base(state)
        {
        }
        #endregion

        #region Methods
        private async Task RetrieveLoginUrl()
        {
            try
            {
                LoginUrl = string.Empty;
                LoginUrl = await _service.RetrieveLoginUrl();
            }
            catch (Exception e)
            {
                _service.ErrorMessage = e.Message;
            }
        }
        #endregion
    }
}
