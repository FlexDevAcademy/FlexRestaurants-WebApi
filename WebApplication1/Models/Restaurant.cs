using System.ComponentModel.DataAnnotations;

namespace FlexRestaurants.Models
{
	public class Restaurant
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }

		[Required]
		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

        public string? TelephoneNumber { get; set; }

        public double Rating { get; set; }
	}
}
