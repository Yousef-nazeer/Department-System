using Demo.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max lengtg is 50 Chars")]
        [MinLength(0, ErrorMessage = "Min lengtg is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 30, ErrorMessage = "Age Must Be Between 22 and 30")]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}", ErrorMessage = "Adress must be like this 123-Distrct-citty-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Range(4000, 8000)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]

        public int PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]

       public DateTime HireDate { get; set; }

        //ForiegnKEy 
        //[ForeignKey("Department")]
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public IFormFile Image { get; set;}

        public string ImageName { get; set; }

    }
}
