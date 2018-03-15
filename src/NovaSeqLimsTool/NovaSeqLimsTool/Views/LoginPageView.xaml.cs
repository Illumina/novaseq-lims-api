using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovaSeqLimsTool.Views
{
    /// <summary>
    /// Interaction logic for LoginPageView.xaml
    /// </summary>
    public partial class LoginPageView : UserControl
    {
        #region Fields
        public static readonly DependencyProperty LimsUriProperty = DependencyProperty.Register(nameof(LimsUri), typeof(Uri), typeof(LoginPageView));
        public static readonly DependencyProperty LimsUserNameProperty = DependencyProperty.Register(nameof(LimsUserName), typeof(string), typeof(LoginPageView));
        public static readonly DependencyProperty LimsTokenProperty = DependencyProperty.Register(nameof(LimsToken), typeof(string), typeof(LoginPageView));
        #endregion

        #region Properties
        public Uri LimsUri
        {
            get { return (Uri)GetValue(LimsUriProperty); }
            set { SetValue(LimsUriProperty, value); }
        }
        
        public string LimsUserName
        {
            get { return (string)GetValue(LimsUserNameProperty); }
            set { SetValue(LimsUserNameProperty, value); }
        }
                
        public string LimsToken
        {
            get { return (string)GetValue(LimsTokenProperty); }
            set { SetValue(LimsTokenProperty, value); }
        }
        #endregion

        #region Constructor
        public LoginPageView()
        {
            SetBrowserEmulationMode();
            InitializeComponent();
            var a = new AjaxHolder();
            a.LoggedIn += (o, e) =>
            {
                LimsToken = a.Token;
                LimsUserName = a.UserName;
            };
            LimsBrowser.ObjectForScripting = a;
        }
        #endregion

        #region Methods

        /// <summary>
        /// This method configures the web browser to not use IE 7 by default,
        /// which breaks scripting and modern website support
        /// </summary>
        public void SetBrowserEmulationMode()
        {
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            if (String.Compare(fileName, "devenv.exe", true) == 0 ||
                String.Compare(fileName, "XDesProc.exe", true) == 0)
                return;
            UInt32 mode = 10000;
            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, mode);
        }
        
        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == LimsUriProperty && LimsUri != null)
            {
                var uriString = LimsUri.ToString() + "?refreshToken=" + Guid.NewGuid().ToString();
                var uri = new Uri(uriString);
                LimsBrowser.Navigate(uri);
            }
        }
        #endregion
    }
}
