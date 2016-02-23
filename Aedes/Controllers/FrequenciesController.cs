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
using Aedes.Models;

namespace Aedes.Controllers
{
    public class FrequenciesController : ApiController
    {
        private AedesContext db = new AedesContext();

        // GET: api/Frequencies
        public IQueryable<Frequency> GetFrequencies()
        {
            return db.Frequencies;
        }

        // GET: api/Frequencies/5
        [ResponseType(typeof(Frequency))]
        public IHttpActionResult GetFrequency(int id)
        {
            Frequency frequency = db.Frequencies.Find(id);
            if (frequency == null)
            {
                return NotFound();
            }

            return Ok(frequency);
        }

        // PUT: api/Frequencies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFrequency(int id, Frequency frequency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != frequency.Id)
            {
                return BadRequest();
            }

            db.Entry(frequency).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrequencyExists(id))
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

        // POST: api/Frequencies
        [ResponseType(typeof(Frequency))]
        public IHttpActionResult PostFrequency(Frequency frequency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Frequencies.Add(frequency);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = frequency.Id }, frequency);
        }

        // DELETE: api/Frequencies/5
        [ResponseType(typeof(Frequency))]
        public IHttpActionResult DeleteFrequency(int id)
        {
            Frequency frequency = db.Frequencies.Find(id);
            if (frequency == null)
            {
                return NotFound();
            }

            db.Frequencies.Remove(frequency);
            db.SaveChanges();

            return Ok(frequency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FrequencyExists(int id)
        {
            return db.Frequencies.Count(e => e.Id == id) > 0;
        }
    }
}