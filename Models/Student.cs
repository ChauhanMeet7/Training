using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace WebApplication1.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Range(1, 10)]

        public int StudentId { get; set; }
        [Required]
        public string? Course { get; set; }

        public required string? Branch { get; set; }
        public required string? Semester { get; set; }
        [Range(1, 5)]
        public required int VT_Sr_No { get; set; }
        [Range(1, 12)]
        public required int Training_Period { get; set; }
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string College { get; set; }
        [Range(1, 20)]
        public required int EnrollmentNumber { get; set; }
        public required string? StartDate { get; set; }
        public required string? EndDate { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        

        [ForeignKey("DepartmentId")]
      
        public Department? Department { get; set; }
        public required string? AddressCollege { get; set; }
        public required string? Birth_Date { get; set; }
        public required string? Blood_group { get; set; }
        public required string? Permanent_Address { get; set; }

        public required string PhoneNumber { get; set; }

        public required string ParentsContact { get; set; }
        public required string? Married { get; set; }
        public required string? Examination { get; set; }
        public required string? Board { get; set; }
        [Range(1, 4)]
        public required int PassingYear { get; set; }
        [Range(1, 2)]
        public required int Percentage { get; set; }
        public required string? FatherName { get; set; }
        public required string? MotherName { get; set; }

    }
}
