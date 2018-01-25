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
    public class BonusQuestionsController : ApiController
    {
        private GameDataContext db = new GameDataContext();

        [Route("BonusQuestions")]
        public HttpResponseMessage GetBonusQuestions()
        {
            var result = db.BonusQuestions.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("BonusQuestions/{Id}")]
        public HttpResponseMessage GetBonusQuestionsById(int Id)
        {
            var result = db.BonusQuestions.Where(x => x.Id == Id).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("BonusQuestions/category/{categoryId}")]
        public HttpResponseMessage GetProductByCategories(int categoryId)
        {
            var result = db.BonusQuestions.Include("Category").Where(x => x.CategoryId == categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [Route("BonusQuestions")]
        public HttpResponseMessage PostChallengeQuestion(BonusQuestion obj)
        {
            if (obj == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.BonusQuestions.Add(obj);
                db.SaveChanges();
                var result = obj;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pergunta-Bonus.");

            }
        }


        [HttpPatch]
        [Route("BonusQuestions")]
        public HttpResponseMessage PatchChallengeQuestion(ChallengeQuestion challengeQuestion)
        {
            if (challengeQuestion == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<ChallengeQuestion>(challengeQuestion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var result = challengeQuestion;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao Alterar Pergunta-Bonus.");

            }
        }

        [HttpPut]
        [Route("BonusQuestions")]
        public HttpResponseMessage PutChallengeQuestion(ChallengeQuestion challengeQuestion)
        {
            if (challengeQuestion == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Entry<ChallengeQuestion>(challengeQuestion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var result = challengeQuestion;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar Pergunta-Bonus");
            }
        }

        [HttpDelete]
        [Route("BonusQuestions")]
        public HttpResponseMessage DeleteChallengeQuestion(int Id)
        {
            if (Id <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.BonusQuestions.Remove(db.BonusQuestions.Find(Id));
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Pergunta-Bonus excluida");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir Pergunta-Bonus");
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
            return db.BonusQuestions.Count(e => e.Id == id) > 0;
        }
    }
}
