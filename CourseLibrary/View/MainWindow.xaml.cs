using CourseLibrary.Infrastructure;
using CourseLibrary.ViewModel;
using Library.Data;
using Library.Model;
using System;
using System.Windows;

namespace Library
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//AppDbContext appDbContext;
		public MainWindow()
		{
			//appDbContext = new AppDbContext();
			//CreateTestData();
			InitializeComponent();
		}

		//public void CreateTestData()
		//{
		//	appDbContext.Books.Add(new Book
		//	{
		//		Title = "Test",
		//		Description = "Test",
		//		Gengre = Gengres.Romance,
		//		Author = "Test",
		//		Price = 10,
		//		ReleaseDate = DateTime.Now,
		//		ImageData = Utils.ConvertBitmapSourceToByteArray("D:\\Users\\Користувач\\Downloads\\Знімок екрана 2023-10-22 230253.png")
		//	});
		//	appDbContext.SaveChanges();
		//}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
