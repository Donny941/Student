using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StudentDashboard.Models.Entities
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public bool IsDeleted { get; set; }
    }
}
