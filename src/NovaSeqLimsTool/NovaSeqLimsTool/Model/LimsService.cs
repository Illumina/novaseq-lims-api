using NovaSeqLimsTool.HttpExtensions;
using NovaSeqLimsTool.POCOs;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NovaSeqLimsTool.Model
{
    public class LimsService : INotifyPropertyChanged
    {
        #region Fields
        private string _errorMessage;
        private string _limsUrlAddress;
        private string _limsLoginAddress;
        private string _limsToken;
        private string _limsUserName;
        public static string LoginUrl = "Illumina/Sequencer/v2/sequencing-run/login";
        public static string RecipeUrl = "Illumina/Sequencer/v2/sequencing-run/recipe/novaseq";
        public static string RunProgressUrl = "Illumina/Sequencer/v2/sequencing-run/update";
        public static string RunMetricUrl = "Illumina/Sequencer/v2/sequencing-run/metrics";
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public string LimsUrlAddress
        {
            get => _limsUrlAddress;
            set
            {
                _limsUrlAddress = value;
                OnPropertyChanged();
            }
        }

        public Uri LimsUri { get => LimsUrlAddress == null ? null : new Uri(LimsUrlAddress); }

        public string LimsLoginAddress
        {
            get => _limsLoginAddress;
            set
            {
                _limsLoginAddress = value;
                OnPropertyChanged();
            }
        }

        public Uri LimsLoginUri { get => LimsLoginAddress == null ? null : new Uri(LimsLoginAddress); }

        public string LimsToken
        {
            get => _limsToken;
            set
            {
                _limsToken = value;
                OnPropertyChanged();
            }
        }

        public string LimsUserName
        {
            get => _limsUserName;
            set
            {
                _limsUserName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods
        public async Task<string> RetrieveLoginUrl()
        {
            if (LimsUri == null)
            {
                return string.Empty;
            }
            var client = GetNoAuthClient();
            LimsLoginAddress = await client?.GetAsync<string>(new Uri(LimsUri, LoginUrl));
            return LimsLoginAddress;
        }

        public async Task<string> GetRecipeDto(string flowCellId, string librarySerialNumber, CancellationToken token = default(CancellationToken))
        {
            var client = GetAuthClient();
            var response = await client?.PostAsync<RecipeIdentifier, RecipeDto>(new Uri(LimsUri, RecipeUrl),
                    new RecipeIdentifier
                    {
                        FlowCellId = flowCellId,
                        LibraryContainerId = librarySerialNumber
                    }, token);
            return response?.ToString() ?? string.Empty;
        }

        public async Task SubmitMetrics(SequencingRunMetrics srm, CancellationToken token = default(CancellationToken))
        {
            var client = GetAuthClient();
            var response = await client.PostAsync<SequencingRunMetrics, HttpResponseMessage>(new Uri(LimsUri, RunMetricUrl), srm, token);
            response?.EnsureSuccessStatusCode();
        }

        public async Task SendRunProgress(SequencingRunStatusDto srs, CancellationToken token = default(CancellationToken))
        {
            var client = GetAuthClient();
            var response = await client.PostAsync<SequencingRunStatusDto, HttpResponseMessage>(new Uri(LimsUri, RunProgressUrl), srs, token);
            response?.EnsureSuccessStatusCode();
        }

        private HttpClient GetNoAuthClient()
        {
            return HttpClientExtensions.CreateJson();
        }

        private void VerifyAuthorized()
        {
            if (string.IsNullOrWhiteSpace(LimsToken))
            {
                throw new ArgumentException("Not logged in or authorized");
            }
        }

        private HttpClient GetAuthClient()
        {
            VerifyAuthorized();
            return HttpClientExtensions.GetAuthenticatedClient(LimsToken);
        }

        /// <summary>
        /// Raises the PropertyChanged event for a given property
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
