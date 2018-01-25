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


        [HttpPost]
        [Route("Users/Login")]
        public HttpResponseMessage PostLogin(Game.Domain.User userlogin)
        {
            string responseMessage = string.Empty;
            if (userlogin.Password == string.Empty || userlogin.Email == string.Empty )
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Preencha os campos de login!");
            }
            try
            {

                User user = new Game.Domain.User();
                user = (from u in db.Users
                        where userlogin.Email == u.Email && 
                        userlogin.Password == u.Password
                        select  u)
                        .FirstOrDefault<User>();

                if (user != null)
                {

                    //VALIDA SE USUARIO JÁ POSSUI SESSAO ATIVA
                    Session session = db.Sessions.Where(x =>x.UserId == user.Id && x.IsActive == true).FirstOrDefault<Session>();
                    if (session == null)
                    {
                        // SE NÃO TEM, CRIA UMA NOVA
                        db.Sessions.Add(new Session { IsActive = true, UserId = user.Id });
                        db.SaveChanges();
                        session = db.Sessions.Where(x => x.UserId == user.Id && x.IsActive == true).FirstOrDefault<Session>();
                    } 
                    return Request.CreateResponse(HttpStatusCode.OK, session);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Usuário não encontrado!");
                }
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao fazer login.");

            }
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
