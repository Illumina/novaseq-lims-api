using NovaSeqLimsTool.HttpExtensions;
using NovaSeqLimsTool.Model;
using NovaSeqLimsTool.POCOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaSeqLimsTool.ViewModels
{
    public class MetricsViewModel : ViewModelBase
    {
        #region Fields
        private string _metrics;
        #endregion

        #region Properties
        public string Metrics
        {
            get => _metrics;
            set => SetProperty(ref _metrics, value);
        }
        #endregion

        #region ICommands
        public ICommand SendRunMetricsCommand => new AsyncCommand(SubmitMetrics, () => true);
        #endregion

        #region Constructor
        public MetricsViewModel(LimsService state) : base(state)
        {
        }
        #endregion

        #region Method
        private async Task SubmitMetrics()
        {
            try
            {
                var srm = new SequencingRunMetrics();
                await _service.SubmitMetrics(srm);
                Metrics = srm.ToString();
            }
            catch (Exception e)
            {
                _service.ErrorMessage = e.Message;
            }
            #endregion
        }
    }
}
