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
        //public IEnumerable<EMPLOYEE> Get()
        //{
        //    using (STEPHENEntities entities = new STEPHENEntities())
        //    {
        //        return entities.EMPLOYEES.ToList();
        //    }
        //}


        public HttpResponseMessage Get(string gender = "All")
        {
            using (STEPHENEntities entities = new STEPHENEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.EMPLOYEES.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.EMPLOYEES.Where(e => e.GENDER.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.EMPLOYEES.Where(e => e.GENDER.ToLower() == "female").ToList());

                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "value for gender must be male or female. " + gender + " is invalid");
                }

            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (STEPHENEntities entities = new STEPHENEntities())
            {
                var entity = entities.EMPLOYEES.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with id =" + id.ToString() + " not found");
                }
            }
        }

        public HttpResponseMessage post([FromBody] EMPLOYEE employee)
        {
            try
            {
                using (STEPHENEntities entities = new STEPHENEntities())
                {
                    entities.EMPLOYEES.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (STEPHENEntities entities = new STEPHENEntities())
                {
                    var entity = entities.EMPLOYEES.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id.ToString() + "not found to delete");
                    }
                    else
                    {
                        entities.EMPLOYEES.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        public HttpResponseMessage put(int id, [FromBody] EMPLOYEE employee)
        {
            try {
            using (STEPHENEntities entities = new STEPHENEntities())
            {

                var entity = entities.EMPLOYEES.FirstOrDefault(e => e.ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with id = " + id.ToString() + " not found");
                }
                else
                {
                    entity.FIRSTNAME = employee.FIRSTNAME;
                    entity.LASTNAME = employee.LASTNAME;
                    entity.GENDER = employee.GENDER;
                    entity.SALARY = employee.SALARY;

                    entities.SaveChanges(); 
                    return Request.CreateResponse(HttpStatusCode.OK,entity);
                }

            }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
