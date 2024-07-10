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
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(MVCDBContext dbContext):base(dbContext)
        {

        }

    }
}
