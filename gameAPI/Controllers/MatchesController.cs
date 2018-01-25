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
    public class MatchesController : ApiController
    {
        private GameDataContext db = new GameDataContext();


        [Route("Matches")]
        public HttpResponseMessage GetMatches()
        {
            var result = db.Matches.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [Route("Matches/{Id}")]
        public HttpResponseMessage GetMatchesById(int Id)
        {
            var result = (from m in db.Matches
                          join mu in db.Match_Users
                          on m.Id equals mu.MatchId
                          where mu.MatchId.Equals(Id) select mu ).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Matches/category/{categoryId}")]
        public HttpResponseMessage GetMatchestByCategories(int categoryId)
        {
            var result = db.ChallengeQuestions.Include("Category").Where(x => x.CategoryId == categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [Route("Matches")]
        public HttpResponseMessage PostMatch(Match obj)
        {
            if (obj == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Matches.Add(obj);
                db.SaveChanges();
                var result = obj;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao criar nova Partida.");

            }
        }


        [HttpPatch]
        [Route("Matches")]
        public HttpResponseMessage PatchMatches(Match match)
        {
            if (match == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Match>(match).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var result = match;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao Alterar Partida.");

            }
        }

        [HttpPut]
        [Route("Matches")]
        public HttpResponseMessage PutMacth(Match match)
        {
            if (match == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Entry<Match>(match).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var result = match;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar Partida");
            }
        }

        [HttpDelete]
        [Route("Matches")]
        public HttpResponseMessage DeleteMacth(string token)
        {
            if ( token == string.Empty)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                int id = int.Parse((from match in db.Matches
                                     where match.Token == token
                                     select match.Id).ToString());
                db.Matches.Remove(db.Matches.Find(id));
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Partida excluida");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir Pergunta-Desafio");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        private bool ChallengeQuestionExists(int id)
        {
            return db.ChallengeQuestions.Count(e => e.Id == id) > 0;
        }
    }
}