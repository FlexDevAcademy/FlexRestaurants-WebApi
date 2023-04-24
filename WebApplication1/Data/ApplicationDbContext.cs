using FlexRestaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexRestaurants.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Restaurant> Restaurants { get; set; }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
			if (!Restaurants.Any())
			{
				Restaurants.AddRange(_initialMockedRestaurants);
				SaveChanges();
			}
		}

		private static readonly List<Restaurant> _initialMockedRestaurants = new()
		{
			new () {
				Id = 1,
				Name = "BaraBoo",
				Description = "Dobre jedzenie przy Malcie",
				ImageUrl = "https://restauracje.baraboo.pl/web/uploaded_images/gallery/1654423564_4f168077cb97e51c5884b6b0056d0c89.JPG",
				TelephoneNumber = "516 894 836",
				Rating = 4.84
			},
			new () {
				Id = 2,
				Name = "Forni Rossi",
				Description = "Pizza Italiana",
				ImageUrl = "https://lh3.googleusercontent.com/p/AF1QipMQ01W2GmcXKP-zc2dQ2yOHi8uba5WsnyMA0uc=s1280-p-no-v1",
				TelephoneNumber = "513 296 866",
				Rating = 3.75
			},
			new () {
				Id = 3,
				Name = "Impresja",
				Description = "Domowe obiadki",
				ImageUrl = "https://impresjapoznan.pl/wp-content/uploads/2018/11/lokal-z-zewnatrz-2048x0.jpg",
				TelephoneNumber = "512 893 875",
				Rating = 2.01
			},
		};
	}
}
