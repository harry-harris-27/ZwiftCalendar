using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Zwift.Calendar.Mobile
{
    public class RelayCommand : ICommand
    {

        protected static readonly Func<object, bool> defaultCanExecute = new Func<object, bool>(obj => true);

        private readonly Action<object> execute;
        private readonly Func<object, bool> canExceute;


        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Func<bool> canExecute) : this(obj => execute.Invoke(), obj => canExecute?.Invoke() ?? true) { }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExceute)
        {
            this.execute = execute ?? throw new ArgumentException(nameof(execute));
            this.canExceute = canExceute ?? defaultCanExecute;
        }


        public event EventHandler CanExecuteChanged;


        public virtual bool CanExecute(object parameter)
        {
            bool result = canExceute(parameter);
            return result;
        }

        public virtual void Execute(object parameter) => execute(parameter);


        public virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand<TParameter> : RelayCommand
    {

        public RelayCommand(Action<TParameter> execute) : this(execute, null) { }

        public RelayCommand(Action<TParameter> execute, Func<TParameter, bool> canExecute)
            : base(TryExecute(execute), TryCanExecute(canExecute)) { }


        private static Action<object> TryExecute(Action<TParameter> execute) => new Action<object>(obj =>
        {
            if (obj is TParameter parameter)
            {
                execute.Invoke(parameter);
            }
        });

        private static Func<object, bool> TryCanExecute(Func<TParameter, bool> canExecute) => new Func<object, bool>(obj =>
        {
            if (obj is TParameter parameter)
            {
                canExecute.Invoke(parameter);
            }
            return false;
        });
    }
}
