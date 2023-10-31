using CourseLibrary.Infrastructure;
using CourseLibrary.View;
using Library.Data;
using Library.Model;
using Microsoft.Win32;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseLibrary.ViewModel
{
	internal class MainWindowViewModel : ViewModelBase
	{
		private AppDbContext dbContext;

		private GenericUnitOfWork work;
		private IGenericRepository<Book> bookRepository;

		public string[] gengres => Enum.GetNames(typeof(Gengres));

		public MainWindowViewModel()
		{
			InitializeDB();
		}

		private void InitializeDB()
		{
			dbContext = new AppDbContext();
			work = new GenericUnitOfWork(dbContext);
			bookRepository = work.Repository<Book>();
		}

		public IEnumerable<Book> Books
		{
			get
			{
				var books = bookRepository.GetAll();

				if (_name == null)
					return books;

				if (_name == string.Empty)
				{
					switch (SelectedGenre)
					{
						case Gengres.Horror:
							return books.Where(x => x.Gengre == Gengres.Horror);
						case Gengres.Adventure:
							return books.Where(x => x.Gengre == Gengres.Adventure);
						case Gengres.Mystery:
							return books.Where(x => x.Gengre == Gengres.Mystery);
						case Gengres.ScienceFiction:
							return books.Where(x => x.Gengre == Gengres.ScienceFiction);
						case Gengres.Romance:
							return books.Where(x => x.Gengre == Gengres.Romance);
						case Gengres.Fiction:
							return books.Where(x => x.Gengre == Gengres.Fiction);
						case Gengres.Thrillers:
							return books.Where(x => x.Gengre == Gengres.Thrillers);
						case Gengres.All:
							return books;
					}
				}
				else
				{
					switch (SelectedGenre)
					{
						case Gengres.Horror:
							return books.Where(x => x.Gengre == Gengres.Horror && x.Title.ToLower() == SelectedName.ToLower());
						case Gengres.Adventure:
							return books.Where(x => x.Gengre == Gengres.Adventure && x.Title.ToLower() == SelectedName.ToLower());
						case Gengres.Mystery:
							return books.Where(x => x.Gengre == Gengres.Mystery && x.Title.ToLower() == SelectedName.ToLower());
						case Gengres.ScienceFiction:
							return books.Where(x => x.Gengre == Gengres.ScienceFiction && x.Title.ToLower() == SelectedName.ToLower());
						case Gengres.Romance:
							return books.Where(x => x.Gengre == Gengres.Romance && x.Title.ToLower() == SelectedName.ToLower());
						case Gengres.Fiction:
							return books.Where(x => x.Gengre == Gengres.Fiction && x.Title.ToLower() == SelectedName.ToLower());
						case Gengres.Thrillers:
							return books.Where(x => x.Gengre == Gengres.Thrillers && x.Title.ToLower() == SelectedName.ToLower());
						case Gengres.All:
							return books.Where(x => x.Title.ToLower() == SelectedName.ToLower());
					}
				}
				return books;
			}
		}

		/*Selected Genre*/
		Gengres _gengre;
		public Gengres SelectedGenre
		{
			get
			{
				if (_gengre == null)
					_gengre = new Gengres();
				return _gengre;
			}
			set
			{
				_gengre = value;
				OnPropertyChanged(nameof(Books));
			}
		}

		/*Selected Name*/
		string _name;
		public string SelectedName
		{
			get
			{
				if (_name == null)
					_name = string.Empty;
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Books));
			}
		}

		/*Current Book*/
		Book? _currentBook;
		public Book? CurrentBook
		{
			get
			{
				if (_currentBook == null)
					_currentBook = new Book();
				return _currentBook;
			}
			set
			{
				_currentBook = value;
				OnPropertyChanged(nameof(CurrentBook));
			}
		}

		/*Selected Book*/
		Book? _selectedBook;
		public Book SelectedBook
		{
			get
			{
				if (_selectedBook == null)
					_selectedBook = new Book();

				return _selectedBook;
			}
			set
			{
				_selectedBook = value;
				OnPropertyChanged(nameof(SelectedBook));
			}
		}

		/*Select Image*/
		private async Task<byte[]> SelectImageAsync()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.png";
			openFileDialog.Title = "Select an Image";

			if (openFileDialog.ShowDialog() == true)
			{
				string selectedImagePath = openFileDialog.FileName;
				return Utils.ConvertBitmapSourceToByteArray(selectedImagePath);
			}
			return null;
		}

		// COMMANDS
		/*Select Image Command*/
		RelayCommand? _selectImageCommand;
		public ICommand AddSelectImageCommand
		{
			get
			{
				return _selectImageCommand ?? (_selectImageCommand = new RelayCommand(
				async (object parameter) =>
				{
					CurrentBook.ImageData = await SelectImageAsync();
				}));
			}
		}

		RelayCommand? _editSelectImageCommand;
		public ICommand EditSelectImageCommand
		{
			get
			{
				return _editSelectImageCommand ?? (_editSelectImageCommand = new RelayCommand(
				async (object parameter) =>
				{
					SelectedBook.ImageData = await SelectImageAsync();
				}));
			}
		}

		/*Open Forms*/
		RelayCommand? _openAddWindow;
		public ICommand OpenAddWindow
		{
			get
			{
				return _openAddWindow ?? (_openAddWindow = new RelayCommand(
					new Action<object>((object parameter) =>
					{
						AddWindow form = new AddWindow();
						form.ShowDialog();
						OnPropertyChanged(nameof(Books));
					})
					));
			}
		}

		RelayCommand? _openEditWindow;
		public ICommand OpenEditWindow
		{
			get
			{
				return _openEditWindow ?? (_openEditWindow = new RelayCommand(
					new Action<object>((object parameter) =>
					{
						EditWindow form = new EditWindow();
						form.ShowDialog();
						OnPropertyChanged(nameof(Books));
					}),
					parameter => bookRepository.GetAll().Contains(_selectedBook) &&
								 bookRepository.GetAll().Count() != 0));
			}
		}

		/*Add*/
		RelayCommand? _addBookCommand;
		public ICommand AddBookCommand
		{
			get
			{
				return _addBookCommand ?? (_addBookCommand = new RelayCommand(
					new Action<object>((object parameter) =>
					{
						bookRepository.Add(CurrentBook);
						CurrentBook = null;
						//OnPropertyChanged(nameof(Books));
					}),
					parameter => (!string.IsNullOrEmpty(CurrentBook.Title) &&
								 !string.IsNullOrEmpty(CurrentBook.Description) &&
								 !string.IsNullOrEmpty(CurrentBook.Author) &&
								 CurrentBook.ImageData != null)
					));
			}
		}

		/*Remove*/
		RelayCommand? _removeBookCommand;
		public ICommand RemoveBookCommand
		{
			get
			{
				return _removeBookCommand ?? (_removeBookCommand = new RelayCommand(
						new Action<object>((object parameter) =>
						{
							bookRepository.Remove((Book)parameter);
							OnPropertyChanged(nameof(Books));
						}),
						parameter => bookRepository.GetAll().Contains(_selectedBook) &&
						bookRepository.GetAll().Count() != 0)
					);
			}
		}

		/*Edit*/
		RelayCommand? _editBookCommand;
		public ICommand EditBookCommand
		{
			get
			{
				return _editBookCommand ?? (_editBookCommand = new RelayCommand(
					new Action<object>((object parameter) =>
					{
						bookRepository.Update((Book) parameter);
						SelectedBook = null;
						//OnPropertyChanged(nameof(Books));
					}),
					parameter => (!string.IsNullOrEmpty(SelectedBook.Title) &&
								 !string.IsNullOrEmpty(SelectedBook.Description) &&
								 !string.IsNullOrEmpty(SelectedBook.Author) &&
								 SelectedBook.ImageData != null)
					));
			}
		}

	}
}
