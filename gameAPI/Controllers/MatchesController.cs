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
using System.Security.Cryptography;
using System.Text;

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
        [Route("Matches/New")]
        public HttpResponseMessage PostNewMatch(MatchContract obj)
        {
            if (obj == null)
            {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
               
                var result = obj;
                string token = MD5Hash(obj.Match.Id.ToString() + obj.Match_User.UserId.ToString());
                obj.Match.Token = token.Substring(0,5).ToUpper(); 
                db.Matches.Add(obj.Match);
                db.SaveChanges();
                obj.Match_User.MatchId = obj.Match.Id;
                obj.Match_User.Token = obj.Match.Token;
                db.Match_Users.Add(obj.Match_User);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao criar nova Partida. n/"+e.Message);

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
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}