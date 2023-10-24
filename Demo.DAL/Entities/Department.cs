using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Department
    { 
        public int Id { get; set; }
        [Required(ErrorMessage ="Code is Required !")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Required !")]
        [MaxLength(50 ,ErrorMessage ="Max length is 50")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        
        public ICollection<Employee> Employees { get; set; }

    }
}
