using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing activity resources.
    /// </summary>
    /// <remarks>Inherits from <see cref="BaseApiController"/>, enabling standard API controller functionality
    /// such as routing and model binding. Use this controller to perform operations related to activities within the
    /// application.</remarks>
    public class ActivitiesController(AppDbContext context) : BaseApiController
    {
        private readonly AppDbContext _context = context;

        /// <summary>
        /// Retrieves a list of all activities from the data store.
        /// </summary>
        /// <remarks>This method performs an asynchronous database query to obtain all activities. The
        /// result is returned with an HTTP 200 OK status on success.</remarks>
        /// <returns>An <see cref="ActionResult{T}"/> containing a list of <see cref="Activity"/> objects. Returns an empty list
        /// if no activities are found.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return Ok(await _context.Activities.AsNoTracking().ToListAsync());
        }

        /// <summary>
        /// Retrieves the details of a specific activity by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the activity to retrieve.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the <see cref="Activity"/> object if found; otherwise, a 404 Not Found response.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityDetail(string id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity is null) return NotFound();

            return activity;
        }
    }
}
