using AssignBLL.Interfaces;
using AssignDAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignBLL.Rebositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        private readonly MVCDBContext _dbContext;

        public UnitOfWork(MVCDBContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        => await _dbContext.SaveChangesAsync();

        public void Dispose()
        => _dbContext.Dispose();
    }
}
