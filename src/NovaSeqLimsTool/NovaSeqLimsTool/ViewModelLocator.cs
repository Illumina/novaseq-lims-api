using NovaSeqLimsTool.Model;
using NovaSeqLimsTool.ViewModels;

namespace NovaSeqLimsTool
{
    public class ViewModelLocator
    {
        #region Properties
        public MainViewModel Main { get; private set; }
        public LoginUrlViewModel LoginUrl { get; private set; }
        public GetRecipeViewModel Recipe { get; private set; }
        public LoginPageViewModel LoginPage { get; private set; }
        public MetricsViewModel Metrics { get; private set; }
        public ProgressUpdateViewModel ProgressUpdate { get; private set; }
        public RunAllViewModel RunAll { get; private set; }
        #endregion

        #region Constructor
        public ViewModelLocator()
        {
            var state = new LimsService();
            Main = new MainViewModel(state);
            LoginUrl = new LoginUrlViewModel(state);
            Recipe = new GetRecipeViewModel(state);
            LoginPage = new LoginPageViewModel(state);
            Metrics = new MetricsViewModel(state);
            ProgressUpdate = new ProgressUpdateViewModel(state);
            RunAll = new RunAllViewModel(state);
        }
        #endregion
    }
}
