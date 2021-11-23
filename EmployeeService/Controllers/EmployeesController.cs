using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<EMPLOYEE> Get()
        {
            using (STEPHENEntities entities = new STEPHENEntities())
            {
                return entities.EMPLOYEES.ToList();
            }
        }

        public EMPLOYEE Get(int id)
        {
            using (STEPHENEntities entities = new STEPHENEntities())
            {
                return entities.EMPLOYEES.FirstOrDefault(e => e.ID == id);
            }
        }

    }
}
