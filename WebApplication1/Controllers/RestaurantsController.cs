using FlexRestaurants.Data;
using FlexRestaurants.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlexRestaurants.Controllers
{
	[ApiController]
	[Route("")]
	public class RestaurantsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public RestaurantsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAllRestaurants()
		{
			try
			{
				var restaurants = _context.Restaurants.ToList();

				return Ok(restaurants);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpGet]
		[Route("/{id:int}")]
		public IActionResult GetRestaurantById(int id)
		{
			try
			{
				var restaurant = _context.Restaurants.FirstOrDefault(res => res.Id == id);

				if (restaurant == null)
				{
					return NotFound($"Found no restaurant having id: {id}");
				}

				return Ok(restaurant);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpGet]
		[Route("/{name}")]
		public IActionResult GetRestaurantByName(string name)
		{
			try
			{
				var restaurant = _context.Restaurants.FirstOrDefault(res => res.Name == name);

				if (restaurant == null)
				{
					return NotFound($"Found no restaurant having name: {name}");
				}

				return Ok(restaurant);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpGet]
		[Route("search")]
		public IActionResult GetRestaurantsByRating(double rating = 4.5)
		{
			try
			{
				var restaurants = _context.Restaurants.Where(res => res.Rating > rating);

				if (!restaurants.Any())
				{
					return NotFound($"Found no restaurants with rating above {rating}");
				}

				return Ok(restaurants);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpPost]
		public IActionResult CreateRestaurant([FromBody] Restaurant newRestaurant)
		{
			try
			{
				var existing = _context.Restaurants.FirstOrDefault(res => res.Id == newRestaurant.Id);

				if (existing == null)
				{
					_context.Restaurants.Add(newRestaurant);
					_context.SaveChanges();

					return CreatedAtAction("CreateRestaurant", newRestaurant);
				}

				return BadRequest($"Database already contains restaurant having id: {newRestaurant.Id}");
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpPut]
		public IActionResult UpdateRestaurant([FromBody] Restaurant update)
		{
			try
			{
				var existing = _context.Restaurants.FirstOrDefault(res => res.Id == update.Id);

				if (existing == null)
				{
					return NotFound($"Found no restaurant having id: {update.Id}");
				}

				existing.Name = update.Name;
				existing.Description = update.Description;
				existing.Rating = update.Rating;

				_context.Update(existing);
				_context.SaveChanges();

				return Ok(new { message = $"Successfully updated restaurant having id: {update.Id}" });
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpPatch]
		[Route("/{id:int}/{name}")]
		public IActionResult RenameRestaurant(int id, string name)
		{
			try
			{
				var existing = _context.Restaurants.FirstOrDefault(res => res.Id == id);

				if (existing == null)
				{
					return NotFound($"Found no restaurant having id: {id}");
				}

				string oldName = existing.Name;
				existing.Name = name;

				_context.Update(existing);
				_context.SaveChanges();

				return Ok(new { message = $"Successfully renamed from '{oldName}' to '{name}' (restaurant id: {id})" });
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpDelete]
		[Route("/{id:int}")]
		public IActionResult DeleteRestaurant(int id)
		{
			try
			{
				var existing = _context.Restaurants.FirstOrDefault(res => res.Id == id);

				if (existing == null)
				{
					return NotFound($"Found no restaurant having id: {id}");
				}

				_context.Restaurants.Remove(existing);
				_context.SaveChanges();

				return Ok(new { message = $"Restaurant having id: {id} has been successfully removed" });
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}
	}
}