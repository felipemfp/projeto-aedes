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