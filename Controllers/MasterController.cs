using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MasterController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext context = context;

        [HttpGet]
        [Authorize(Roles = "Admin")]

        

        public async Task<IActionResult> Collage(string searchString)
        {
            var collages = await context.Students.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                collages = (List<Student>)collages.Where(n => n.FirstName.Contains(searchString) || n.LastName.Contains(searchString) || n.College.Contains(searchString)).ToList();
            }
            return View(collages);
        }
    }
}
