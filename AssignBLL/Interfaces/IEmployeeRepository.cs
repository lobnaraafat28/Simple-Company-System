using AssignDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignBLL.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeebyAddress(string address);
        //IQueryable<Employee> SearchEmployeebyName(string name);


    }
}
