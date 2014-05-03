using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PeopleManagement.Domain.Model;
using PeopleManagement.Domain.Services;
using PeopleManagement.Models;

namespace PeopleManagement.Controllers
{
    public class PeopleApiController : ApiController
    {
        public IPeopleManagementService PeopleService { get; private set; }

        public PeopleApiController(IPeopleManagementService peopleManagementService)
        {
            PeopleService = peopleManagementService;
        }

        // GET api/PeopleApi
        public PeopleListResponse GetPeople()
        {
            // Initialize values for pageIndex and pageSize
            int pageIndex, pageSize;
            var httpContext = HttpContext.Current;
            if (!int.TryParse(httpContext.Request.QueryString["page"], out pageIndex)) pageIndex = 0;   // Default is the first page
            if (!int.TryParse(httpContext.Request.QueryString["size"], out pageSize)) pageSize = 10;    // Default page size is 10

            // Return people list in JSON
            return Json(new PeopleListResponse
            {
                People = PeopleService.List(pageIndex, pageSize).Select(PersonModel.FromPerson),
                PageCount = PeopleService.CountPage(pageSize)
            });
        }

        // GET api/PeopleApi/5
        public PersonModel GetPerson(int id)
        {
            // Get person data
            Person person = PeopleService.FindPerson(id);
            if (person == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            // Return data in JSON
            return Json(PersonModel.FromPerson(person));
        }

        // PUT api/PeopleApi/5
        public HttpResponseMessage PutPerson(int id, PersonUpdateModel person)
        {
            // Validate incoming model
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
            if (id != person.PersonId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }


            try
            {
                // Update person's information
                var updatedPerson = PeopleService.UpdatePerson(person.PersonId,
                                                               person.FirstName,
                                                               person.LastName,
                                                               person.Birthday,
                                                               person.Email,
                                                               person.Phone,
                                                               person.LastModified);
                // Return updated person back to client                
                return Request.CreateResponse(HttpStatusCode.OK, PersonModel.FromPerson(updatedPerson));
            }
            catch (DataException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }            
        }

        // POST api/PeopleApi
        public HttpResponseMessage PostPerson(PersonAddModel person)
        {
            if (ModelState.IsValid)
            {
                var newPerson = PeopleService.AddPerson(person.FirstName,
                                                        person.LastName,
                                                        person.Birthday,
                                                        person.Email,
                                                        person.Phone);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, PersonModel.FromPerson(newPerson));
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newPerson.PersonId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PeopleApi/5
        public HttpResponseMessage DeletePerson(int id, PersonRemoveModel person)
        {            
            if (person == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                PeopleService.DeletePerson(person.PersonId, person.LastModified);
            }
            catch (DataException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        private static TResult Json<TResult>(TResult result)
        {
            // Force return JSON
            HttpContext.Current.Response.ContentType = "application/json";
            return result;
        }             

    }
}