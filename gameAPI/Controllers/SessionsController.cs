using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Game.Domain;
using Game.Infra.DataContext;
using System.Web.Http.Cors;

namespace gameAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("game")]
    public class SessionsController : ApiController
    {
        private GameDataContext db = new GameDataContext();

        [Route("sessions")]
        public HttpResponseMessage GetSessions()
        {
            var result = db.Sessions.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("sessions/{sessionId}")]
        public HttpResponseMessage GetSessionsById(int sessionId)
        {
            var result = db.Sessions.Where(x => x.Id == sessionId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SessionExists(int id)
        {
            return db.Sessions.Count(e => e.Id == id) > 0;
        }
    }
}