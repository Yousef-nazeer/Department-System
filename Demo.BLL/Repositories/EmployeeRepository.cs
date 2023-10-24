using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppDbContext _dbContext;
        public EmployeeRepository(MVCAppDbContext dbContext) : base(dbContext)
        {
            _dbContext= dbContext;
        }

        public object SearchByName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            throw new NotImplementedException();
        }

        
        public IQueryable<Employee> SearchEmployeesByName(string SearchValue) {
            return _dbContext.Employees.Where(E => E.Name.Contains(SearchValue));
        }
    }
}
