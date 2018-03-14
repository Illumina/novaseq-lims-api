using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaSeqLimsTool.ViewModels
{
    public class AsyncCommand : ICommand
    {
        #region Fields
        private Func<bool> _canExecute;
        private Func<Task> _executeAsync;
        #endregion

        #region Properties
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Constructor
        public AsyncCommand(Func<Task> executeMethod, Func<bool> canExecute)
        {
            _canExecute = canExecute;
            _executeAsync = executeMethod;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        public async Task ExecuteAsync(object parameter)
        {
            await _executeAsync();
        }
        #endregion
    }
}
