using NovaSeqLimsTool.Model;
using System;
using System.ComponentModel;

namespace NovaSeqLimsTool.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields
        private string _limsToken;
        private string _limsUserName;
        #endregion

        #region Properties
        public Uri LimsLoginUri
        {
            get => _service.LimsLoginUri;
        }

        public string LimsToken
        {
            get => _limsToken;
            set
            {
                SetProperty(ref _limsToken, value);
                _service.LimsToken = _limsToken;
            }
        }

        public string LimsUserName
        {
            get => _limsUserName;
            set
            {
                SetProperty(ref _limsUserName, value);
                _service.LimsUserName = _limsUserName;
            }
        }
        #endregion

        #region Constructor
        public LoginPageViewModel(LimsService state) : base(state)
        {
            _service.PropertyChanged += ChangeHandler;
        }
        #endregion

        #region Methods
        private void ChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(LimsService.LimsLoginAddress)))
            {
                RaiseEvent(nameof(LimsLoginUri));
            }
        }
        #endregion
    }
}
