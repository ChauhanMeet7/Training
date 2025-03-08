using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

using System.Linq;
using System.Threading.Tasks;


namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext context;
        public StudentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add()
        {
            ViewData["Departments"] = new SelectList(await context.Departments.ToListAsync(), "Id", "Name");

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddViewModel viewModel)
        {
            var student = new Student
            {
                StudentId = viewModel.StudentId,
                Course = viewModel.Course,
                Branch = viewModel.Branch,
                Semester = viewModel.Semester,
                VT_Sr_No = viewModel.VT_Sr_No,
                Training_Period = viewModel.Training_Period,
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName,
                LastName = viewModel.LastName,
                College = viewModel.College,
                EnrollmentNumber = viewModel.EnrollmentNumber,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                DepartmentId = viewModel.DepartmentId,
                AddressCollege = viewModel.AddressCollege,
                Birth_Date = viewModel.Birth_Date,
                Blood_group = viewModel.Blood_group,
                Permanent_Address = viewModel.Permanent_Address,
                PhoneNumber = viewModel.PhoneNumber,
                ParentsContact = viewModel.ParentsContact,
                Married = viewModel.Married,
                Examination = viewModel.Examination,
                Board = viewModel.Board,
                PassingYear = viewModel.PassingYear,
                Percentage = viewModel.Percentage,
                FatherName = viewModel.FatherName,
                MotherName = viewModel.MotherName
            };
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return RedirectToAction("List", "Student");
            //// If there is an error, reload departments
            //ViewData["Departments"] = new SelectList(await context.Departments.ToListAsync(), "Id", "Name", student.DepartmentId);
            //return View(student);
        }
        //[HttpGet]
        [Authorize(Roles = "Admin")]

        //public async Task<IActionResult> List(string searchString)
        //{
    //    var students = await context.Students.ToListAsync();
    //        if(!String.IsNullOrEmpty(searchString))
    //        {
    //            students = (List<Student>) students.Where(n => n.FirstName.Contains(searchString) || n.LastName.Contains(searchString)).ToList();
    //}
    //        return View(students);
    //}
    public async Task<IActionResult> List(string searchString, string sortOrder)
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
                students = students.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString) || s.College.Contains(searchString));
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
                case "college":
                    students = students.OrderBy(s => s.College);
                    break;
                case "college_desc":
                    students = students.OrderByDescending(s => s.College);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }
           
            return View(await students.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(Guid id)
        {
            //var student = await context.Students.FindAsync(id);
            //return View(student);
            var student = await context.Students
        .Include(s => s.Department) // Ensure Department data is included
        .FirstOrDefaultAsync(s => s.Id == id);

           
            // Populate department dropdown
            ViewData["Departments"] = new SelectList(await context.Departments.ToListAsync(), "Id", "Name", student.DepartmentId);

            return View(student);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(Student viewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    // Reload dropdown list if validation fails
            //    ViewData["Departments"] = new SelectList(await context.Departments.ToListAsync(), "Id", "Name", viewModel.DepartmentId);
            //    return View(viewModel);
            //}

           

            var student = await context.Students.FindAsync(viewModel.Id);
            if (student is not null)
            {
                student.StudentId = viewModel.StudentId;
                student.Course = viewModel.Course;
                student.Branch = viewModel.Branch;
                student.Semester = viewModel.Semester;
                student.VT_Sr_No = viewModel.VT_Sr_No;
                student.Training_Period = viewModel.Training_Period;
                student.FirstName = viewModel.FirstName;
                student.MiddleName = viewModel.MiddleName;
                student.LastName = viewModel.LastName;
                student.College = viewModel.College;
                student.EnrollmentNumber = viewModel.EnrollmentNumber;
                student.StartDate = viewModel.StartDate;
                student.EndDate = viewModel.EndDate;
                student.DepartmentId = viewModel.DepartmentId;
                student.AddressCollege = viewModel.AddressCollege;
                student.Birth_Date = viewModel.Birth_Date;
                student.Blood_group = viewModel.Blood_group;
                student.Permanent_Address = viewModel.Permanent_Address;
                student.PhoneNumber = viewModel.PhoneNumber;
                student.ParentsContact = viewModel.ParentsContact;
                student.Married = viewModel.Married;
                student.Examination = viewModel.Examination;
                student.Board = viewModel.Board;
                student.PassingYear = viewModel.PassingYear;
                student.Percentage = viewModel.Percentage;
                student.FatherName = viewModel.FatherName;
                student.MotherName = viewModel.MotherName;


            }
            await context.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }


    }
}


