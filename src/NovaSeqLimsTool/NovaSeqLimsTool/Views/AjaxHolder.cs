using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace NovaSeqLimsTool.Views
{
    /// <summary>
    /// The webbrowser's javascript can interact with this object by talking 
    /// to 'window.external'
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class AjaxHolder
    {
        public string Token { get; private set; }

        public string UserName { get; private set; }

        /// <summary>
        /// Javascript can set the username here.
        /// We filter out 'undefined' because javascript sets that
        /// if there is no username
        /// </summary>
        /// <param name="username"></param>
        public void SetUserName(string username)
        {
            //Clear out previous username to trigger event if it is the same
            UserName = string.Empty;
            if (!username.ToLower().Equals("undefined"))
            {
                UserName = username;
            }
        }

        /// <summary>
        /// Javascript can set the username here.
        /// We filter out 'undefined' because javascript sets that
        /// if there is no token
        /// </summary>
        /// <param name="token"></param>
        public void SetToken(string token)
        {
            //Clear out previous token to trigger event if it is the same
            Token = string.Empty;
            if (!token.ToLower().Equals("undefined"))
                Token = token;
        }

        public void LoginComplete()
        {
            LoggedIn?.Invoke(this, new EventArgs());
        }

        public event EventHandler LoggedIn;
    }

}
