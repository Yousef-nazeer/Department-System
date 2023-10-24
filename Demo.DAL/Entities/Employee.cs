using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(0)]
        public string Name { get; set; }
        public int? Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Range(4000,8000)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]

        public int PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]

        public DateTime HireDate { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

        //ForiegnKEy 
        //[ForeignKey("Department")]
        [Display(Name="Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public string ImageName { get; set; }
    }
}
