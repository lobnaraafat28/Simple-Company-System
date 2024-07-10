using AssignBLL.Interfaces;
using AssignDAL.Contexts;
using AssignDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignBLL.Rebositories
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCDBContext _dbContext;

        public EmployeeRepository(MVCDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeebyAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
