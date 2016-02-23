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
using Aedes.Filters;
using Aedes.Models;

namespace Aedes.Controllers
{
    [AuthFilter]
    public class OccurrencesController : ApiController
    {
        private AedesContext db = new AedesContext();
        private string key => Request.GetQueryNameValuePairs().First(q => q.Key == "key").Value;
        private User user => db.Users.FirstOrDefault(u => u.Key == key);

        // GET: api/Occurrences
        public IQueryable<Occurrence> GetOccurrences()
        {
            return db.Occurrences.Where(o => o.UserTask.Username == user.Username);
        }

        // GET: api/Occurrences/5
        [ResponseType(typeof(Occurrence))]
        public IHttpActionResult GetOccurrence(int id)
        {
            if (!OccurrenceExists(id))
            {
                return BadRequest();
            }

            Occurrence occurrence = db.Occurrences.Find(id);
            if (occurrence == null)
            {
                return NotFound();
            }

            return Ok(occurrence);
        }

        // PUT: api/Occurrences/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOccurrence(int id, Occurrence occurrence)
        {
            if (!OccurrenceExists(id))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != occurrence.Id)
            {
                return BadRequest();
            }

            db.Entry(occurrence).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OccurrenceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Occurrences
        [ResponseType(typeof(Occurrence))]
        public IHttpActionResult PostOccurrence(Occurrence occurrence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.UserTasks.Find(occurrence.UserTaskId).Username != user.Username)
            {
                return BadRequest();
            }

            db.Occurrences.Add(occurrence);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = occurrence.Id }, occurrence);
        }

        // DELETE: api/Occurrences/5
        [ResponseType(typeof(Occurrence))]
        public IHttpActionResult DeleteOccurrence(int id)
        {
            if (!OccurrenceExists(id))
            {
                return BadRequest();
            }

            Occurrence occurrence = db.Occurrences.Find(id);
            if (occurrence == null)
            {
                return NotFound();
            }

            db.Occurrences.Remove(occurrence);
            db.SaveChanges();

            return Ok(occurrence);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OccurrenceExists(int id)
        {
            return db.Occurrences.Where(o=>o.UserTask.Username == user.Username).Count(e => e.Id == id) > 0;
        }
    }
}