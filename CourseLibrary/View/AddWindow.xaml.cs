using Library.Data;
using Library.Model;
using System;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;

using CourseLibrary.Infrastructure;
using System.Globalization;

namespace CourseLibrary.View
{
	/// <summary>
	/// Interaction logic for AddWindow.xaml
	/// </summary>
	public partial class AddWindow : Window
	{
		

		public AddWindow()
		{
			
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}
	}
}
