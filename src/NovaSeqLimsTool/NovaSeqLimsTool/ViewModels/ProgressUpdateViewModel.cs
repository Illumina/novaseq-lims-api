using NovaSeqLimsTool.Model;
using NovaSeqLimsTool.POCOs;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaSeqLimsTool.ViewModels
{
    public class ProgressUpdateViewModel : ViewModelBase
    {
        #region Fields
        private string _progress;
        private SequencingRunStatus _endingStatus;
        #endregion

        #region Properties
        public string Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }
        public SequencingRunStatus EndingStatus
        {
            get => _endingStatus;
            set => SetProperty(ref _endingStatus, value);
        }
        #endregion

        #region ICommands
        public ICommand SubmitUpdateCommand => new AsyncCommand(SendRunProgress, () => true);
        #endregion

        #region Constructor
        public ProgressUpdateViewModel(LimsService state) : base(state)
        {
        }
        #endregion

        #region Methods
        private async Task SendRunProgress()
        {
            try
            {
                var srs = new SequencingRunStatusDto
                {
                    Status = EndingStatus
                };
                await _service.SendRunProgress(srs);
                Progress = srs.ToString();
            }
            catch (Exception e)
            {
                _service.ErrorMessage = e.Message;
            }
            #endregion
        }
    }
}
