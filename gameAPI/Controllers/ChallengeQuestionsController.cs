﻿using System;
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
    public class ChallengeQuestionsController : ApiController
    {
        private GameDataContext db = new GameDataContext();

        [Route("ChallengeQuestions")]
        public HttpResponseMessage GetChallengeQuestions()
        {
            var result = db.ChallengeQuestions.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("ChallengeQuestions/{Id}")]
        public HttpResponseMessage GetChallengeQuestionsById(int Id)
        {
            var result = db.ChallengeQuestions.Where(x=> x.Id==Id).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("ChallengeQuestions/category/{categoryId}")]
        public HttpResponseMessage GetProductByCategories(int categoryId)
        {
            var result = db.ChallengeQuestions.Include("Category").Where(x => x.CategoryId == categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [Route("ChallengeQuestions")]
        public HttpResponseMessage PostChallengeQuestion(ChallengeQuestion obj)
        {
            if (obj == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.ChallengeQuestions.Add(obj);
                db.SaveChanges();
                var result = obj;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pergunta-Desafio.");

            }
        }


        [HttpPatch]
        [Route("ChallengeQuestions")]
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

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao Alterar Pergunta-Desafio.");

            }
        }

        [HttpPut]
        [Route("ChallengeQuestions")]
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar Pergunta-Desafio");
            }
        }

        [HttpDelete]
        [Route("ChallengeQuestions")]
        public HttpResponseMessage DeleteChallengeQuestion(int Id)
        {
            if (Id <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.ChallengeQuestions.Remove(db.ChallengeQuestions.Find(Id));
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Pergunta-Desafio excluida");
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