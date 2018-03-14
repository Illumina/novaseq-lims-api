using NovaSeqLimsTool.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NovaSeqLimsTool.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Event used to notify a property has changed
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected LimsService _service;

        #endregion

        #region Constructor
        protected ViewModelBase(LimsService state)
        {
            _service = state;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the PropertyChanged event for a given property
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the backing field for a property and raise the PropertyChanged event
        /// </summary>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="backingVariable">The backing field</param>
        /// <param name="value">The incident value</param>
        /// <param name="propertyName">The name to use when firing the PropertyChanged Event</param>
        /// <returns></returns>
        protected virtual bool SetProperty<T>(ref T backingVariable, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(backingVariable, value))
            {
                return false;
            }

            backingVariable = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected virtual void RaiseEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
