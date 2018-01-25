using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Game.Domain;
using Game.Infra;
using Game.Infra.DataContext;

namespace gameAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("game")]
    public class UsersController : ApiController
    {
        private GameDataContext db = new GameDataContext();

        [Route("Users")]
        public HttpResponseMessage GetUsers()
        {
            var result = db.Users.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
