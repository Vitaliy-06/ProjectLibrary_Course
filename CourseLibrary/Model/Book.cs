using System;

namespace Library.Model
{
	public enum Gengres
	{
		Horror,
		Adventure,
		Mystery,
		ScienceFiction,
		Romance,
		Fiction,
		Thrillers,
		All
	}

	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Gengres Gengre { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public DateTime ReleaseDate { get; set; }
		public double Price { get; set; }
		public byte[] ImageData { get; set; }

	}
}
