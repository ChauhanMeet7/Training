using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class AddViewModel
    {
        public required int StudentId { get; set; }
        public required string Course { get; set; }
        public required string Branch { get; set; }
        public required string Semester { get; set; }
        public required int VT_Sr_No { get; set; }
        public required int Training_Period { get; set; }
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string College { get; set; }

        public required int EnrollmentNumber { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        public required string AddressCollege { get; set; }
        public required string Birth_Date { get; set; }
        public required string Blood_group { get; set; }
        public required string Permanent_Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string ParentsContact { get; set; }
        public required string Married { get; set; }
        public required string Examination { get; set; }
        public required string Board { get; set; }
        public required int PassingYear { get; set; }
        public required int Percentage { get; set; }
        public required string FatherName { get; set; }
        public required string MotherName { get; set; }
        
    }
}

