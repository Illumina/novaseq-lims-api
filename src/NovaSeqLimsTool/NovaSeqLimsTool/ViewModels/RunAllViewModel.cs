using NovaSeqLimsTool.HttpExtensions;
using NovaSeqLimsTool.Model;
using NovaSeqLimsTool.POCOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaSeqLimsTool.ViewModels
{
    public class RunAllViewModel : ViewModelBase
    {
        #region Fields
        private CancellationTokenSource _cts;
        private string _currentDto;
        private string _flowCellId;
        private string _librarySerialNumber;
        private int _currentCycle;
        private SequencingRunStatus _endingStatus;
        private bool _running;
        #endregion

        #region Properties
        public string CurrentDto
        {
            get => _currentDto;
            set => SetProperty(ref _currentDto, value);
        }

        public string FlowCellId
        {
            get => _flowCellId;
            set => SetProperty(ref _flowCellId, value);
        }

        public string LibrarySerialNumber
        {
            get => _librarySerialNumber;
            set => SetProperty(ref _librarySerialNumber, value);
        }

        public SequencingRunStatus EndingStatus
        {
            get => _endingStatus;
            set => SetProperty(ref _endingStatus, value);
        }

        public bool Running
        {
            get => _running;
            set => SetProperty(ref _running, value);
        }
        #endregion

        #region ICommands
        public ICommand StartRunningCommand => new AsyncCommand(StartOperation, () => Running == false);
        public ICommand StopRunningCommand => new AsyncCommand(StopOperation, () => Running == true);
        #endregion

        #region Constructor
        public RunAllViewModel(LimsService state) : base(state)
        {
            _cts = new CancellationTokenSource();
        }
        #endregion

        #region Methods
        private async Task StopOperation()
        {
            Running = false;
            _cts.Cancel();
        }

        private async Task StartOperation()
        {
            try
            {
                Running = true;
                var token = _cts.Token;
                _currentCycle = 0;

                var client = HttpClientExtensions.GetAuthenticatedClient(_service.LimsToken);

                token.ThrowIfCancellationRequested();

                var recipeResponse = await _service.GetRecipeDto(FlowCellId, LibrarySerialNumber, token);

                CurrentDto = recipeResponse.ToString();

                token.ThrowIfCancellationRequested();

                await Task.Delay(1000, token);

                token.ThrowIfCancellationRequested();

                await NotifyLims(client, SequencingRunStatus.RunStarted, token);

                token.ThrowIfCancellationRequested();

                await Task.Delay(2000, token);

                var end = EndingStatus == SequencingRunStatus.RunStarted ? SequencingRunStatus.RunCompletedSuccessfully : EndingStatus;
                await NotifyLims(client, end, token);
            }
            catch (OperationCanceledException)
            {
                var client = HttpClientExtensions.GetAuthenticatedClient(_service.LimsToken);
                await NotifyLims(client, SequencingRunStatus.RunErroredOut, CancellationToken.None);
            }
            catch (Exception e)
            {
                _service.ErrorMessage = e.Message;
            }
            finally
            {
                Running = false;
            }
        }

        private async Task NotifyLims(HttpClient client, SequencingRunStatus status, CancellationToken token)
        {

            var fcdate = DateTime.Now;
            var sbsdate = DateTime.Now;
            var ltdate = DateTime.Now;
            var cdate = DateTime.Now;
            var bdate = DateTime.Now;
            var sbcycles = 151;
            var ccycles = 151;
            var bcycles = 151;
            var runInfo = new RunInfo
            {
                RunId = "Run 1",
                FlowCellId = FlowCellId,
                LibraryTubeId = LibrarySerialNumber,
                InstrumentId = "NovaSeq1",
                InstrumentType = SequencingInstrumentType.NovaSeq6000,
                FlowCellSide = "Left",
                DateTime = DateTime.Now,
                OutputFolder = "C:\\",
                UserName = "User1"
            };

            var reagents = new List<Reagent>
                {
                    //Flow Cell
                    new Reagent
                    {
                        Name = "Flow Cell",
                        ExpirationDate = fcdate,
                        LotNumber = "1234",
                        SerialNumber = "5764",
                        PartNumber = "12341344",
                        Mode = "Sequencing"
                    },
                    new Reagent
                    {
                        Name = "SBS",
                        ExpirationDate = sbsdate,
                        LotNumber = "346345",
                        SerialNumber = "46758736",
                        PartNumber = "12342456",
                        Mode = "Sequencing",
                        Cycles = sbcycles
                    },
                    new Reagent
                    {
                        Name = "Library Tube",
                        ExpirationDate = ltdate,
                        LotNumber = "45782724",
                        SerialNumber = "1345156",
                        PartNumber = "13461346",
                        Mode = "13461457"
                    },
                    new Reagent
                    {
                        Name = "Cluster",
                        ExpirationDate = cdate,
                        LotNumber = "32478217",
                        SerialNumber = "10971",
                        PartNumber = "209726789",
                        Mode = "21769820",
                        Cycles = ccycles
                    },
                    new Reagent
                    {
                        Name = "Buffer",
                        ExpirationDate = bdate,
                        LotNumber = "6549851",
                        SerialNumber = "75637",
                        PartNumber = "6454",
                        Mode = "156753",
                        Cycles = bcycles
                    }
                };

            var srsDto = new SequencingRunStatusDto
            {
                RunInfo = runInfo,
                Status = status,
                Reagents = reagents,
                CurrentCycle = ++_currentCycle,
                CurrentRead = 1,
                InstrumentControlSoftwareVersion = "1.2.3",
                RtaVersion = "0.0.2",
                FirmwareVersion = "6.0.4"
            };
            try
            {
                token.ThrowIfCancellationRequested();
                CurrentDto = srsDto.ToString();
                await _service.SendRunProgress(srsDto, token);
                token.ThrowIfCancellationRequested();
                if (status == SequencingRunStatus.RunCompletedSuccessfully)
                {
                    token.ThrowIfCancellationRequested();
                    var srm = CreateRunMetrics(runInfo);
                    await _service.SubmitMetrics(srm, token);
                    token.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                _service.ErrorMessage = e.Message;
            }
        }

        private SequencingRunMetrics CreateRunMetrics(RunInfo runInfo)
        {
            var srm = new SequencingRunMetrics
            {
                RunInfo = runInfo
            };
            return srm;
        }
        #endregion
    }
}
