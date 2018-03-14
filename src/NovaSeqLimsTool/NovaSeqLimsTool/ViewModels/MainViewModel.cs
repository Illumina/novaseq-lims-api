using NovaSeqLimsTool.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaSeqLimsTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private string _error;
        #endregion

        #region Properties
        public string LimsUrlAddress
        {
            get => _service.LimsUrlAddress;
            set
            {
                _service.LimsUrlAddress = value;
                OnPropertyChanged();
            }
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        #endregion

        #region ICommands
        public ICommand VerifyUrlCommand => new AsyncCommand(Verify, CanVerify);
        #endregion

        #region Constructor
        public MainViewModel(LimsService state) : base(state)
        {
            _service.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(LimsService.ErrorMessage))
                {
                    Error = _service.ErrorMessage;
                }
            };
        }
        #endregion

        #region Methods
        public async Task Verify()
        {
            try
            {
                Error = string.Empty;
                var s = _service.LimsUri;
                await _service.RetrieveLoginUrl();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        public bool CanVerify()
        {
            return !string.IsNullOrWhiteSpace(LimsUrlAddress);
        }
        #endregion
    }
}
