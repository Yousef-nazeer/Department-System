using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepaatrmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(IEmployeeRepository employeeRepository,IDepaatrmentRepository depaatrmentRepository)
        {
            EmployeeRepository=employeeRepository;
            DepartmentRepository = depaatrmentRepository;

        }
    }
}
