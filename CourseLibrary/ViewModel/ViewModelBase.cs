using System;
using System.ComponentModel;

namespace CourseLibrary.ViewModel
{
	abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void Dispose()
		{
			this.OnDispose();
		}

		public virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnDispose() { }

	}
}
