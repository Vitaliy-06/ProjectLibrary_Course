using System;
using System.Windows.Input;

namespace CourseLibrary.Infrastructure
{
	internal class RelayCommand : ICommand
	{
		Action<object> _execute;
		Predicate<object> _canExecute;

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public RelayCommand(Action<object> execute)
		{
			_execute= execute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute.Invoke(parameter);
		}

		public void Execute(object parameter)
		{
			_execute?.Invoke(parameter);
		}
	}
}
