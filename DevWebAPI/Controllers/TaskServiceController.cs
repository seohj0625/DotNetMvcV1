using DevWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevWebAPI.Controllers
{
    public class TaskServiceController : ApiController
    {
        TaskRepository repo = new TaskRepository();

        public IEnumerable<Task> Get()
        {
            return repo.GetTasks().AsEnumerable();
        }

        public Task Get(int id)
        {
            Task task = repo.GetTask(id);
            if(task == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return task;
        }

        public HttpResponseMessage Post([FromBody] Task task)
        {
            if(ModelState.IsValid)
            {
                repo.AddTask(task);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, task);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = task.Id }));
                return response;
            } else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if(id != task.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            repo.UpdateTask(task);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            Task task = repo.GetTask(id);
            if(task == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            repo.RemoveTask(id);

            return Request.CreateResponse(HttpStatusCode.OK, task);
        }
    }
}
