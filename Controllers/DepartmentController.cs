using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Buffers;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext context;

        public DepartmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var student = await context.Students.ToListAsync();
        //    return View(student);
        //}
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            // Define sorting parameters
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["IdSortParam"] = sortOrder == "id" ? "id_desc" : "id";
            ViewData["CollegeSortParam"] = sortOrder == "college" ? "college_desc" : "college";

            var students = context.Students
        .Include(s => s.Department) // Ensure department data is loaded
        .AsQueryable();


            // Apply search filter if search string is provided
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString)  || s.LastName.Contains(searchString) || s.Department.Name.Contains(searchString));
            }

            // Apply sorting based on the provided sort order
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "id":
                    students = students.OrderBy(s => s.StudentId);
                    break;
                case "id_desc":
                    students = students.OrderByDescending(s => s.StudentId);
                    break;
               
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }

            return View(await students.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Add(department);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Data;
//using WebApplication1.Models;
//using WebApplication1.netCore.Repositories;

//namespace WebApplication1.netCore.Controllers
//{
//    public class DepartmentController : Controller
//    {
//        private readonly IDepartmentRepository _context;
//        private readonly ApplicationDbContext context;

//        public DepartmentController(ApplicationDbContext context)
//        {
//            this.context = context;
//        }
//        public async Task<IActionResult> Index()
//        {
//            //Fetch the data from database
//            var departments = await _context.GetAllAsync();
//            return View(departments);
//        }
//        public IActionResult Add()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Add(DepartmentViewModel model)
//        {

//            if (!ModelState.IsValid)
//            {
//                return View(model); // Return to the form with validation errors
//            }

//            //Insert data to the database           
//            await _departmentRepository.AddAsync(model);

//            // Redirect to List all department page
//            return RedirectToAction("Index", "Department");
//        }

//        //GET: /Department/Edit
//        [HttpGet]
//        public async Task<IActionResult> Edit(int id)
//        {
//            var department = await _departmentRepository.GetByIdAsync(id);
//            return View(department);
//        }

//        //POST: /Department/Edit
//        [HttpPost]
//        public async Task<IActionResult> Edit(DepartmentViewModel department)
//        {

//            if (ModelState.IsValid)
//            {
//                //Update the database with modified details
//                await _departmentRepository.UpdateAsync(department);

//                // Redirect to List all department page
//                return RedirectToAction("Index", "Department");
//            }

//            // If the model is not valid, return the view with the validation errors
//            return View(department);
//        }

//        //GET: /Department/Delete
//        [HttpGet]
//        public async Task<IActionResult> Delete(int id)
//        {
//            //Delete the data from database
//            await _departmentRepository.DeleteAsync(id);

//            // Redirect to List all department page
//            return RedirectToAction("Index", "Department");
//        }
//    }
//}
