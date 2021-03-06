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
using Aedes.Filters;
using Aedes.Models;

namespace Aedes.Controllers
{
    public class UsersController : ApiController
    {
        private AedesDBContext db = new AedesDBContext();
        private string key => Request.GetQueryNameValuePairs().First(q => q.Key == "key").Value;
        private User user => db.Users.FirstOrDefault(u => u.Key == key);

        // GET: api/Users
        [AuthFilter]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser()
        {
            if (this.user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string username, string password)
        {
            User user = db.Users.Find(username);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Password == Helpers.HashIt.SHA256($"salt{username}{password}salt"))
            {
                return Ok(user);
            }

            return BadRequest();
        }

        // PUT: api/Users/5
        [AuthFilter]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!UserExists(id))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (this.user.Username != user.Username)
            {
                return BadRequest();
            }

            user.Password = Helpers.HashIt.SHA256($"salt{user.Username}{user.Password}salt");
            user.Key = db.Users.Find(id).Key;

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user.DateRegister == null)
            {
                user.DateRegister = DateTime.Now;
            }
            user.Password = Helpers.HashIt.SHA256($"salt{user.Username}{user.Password}salt");
            user.Key = Helpers.HashIt.SHA256($"salt{user.Username}{user.DateRegister.ToString("yyyy-MM-dd HH:mm:ss")}salt");

            db.Users.Add(user);

            try
            {
                db.SaveChanges();

                user.UserTasks = new List<UserTask>();

                foreach (Task task in db.Tasks)
                {
                    user.UserTasks.Add(new UserTask()
                    {
                        Task = task,
                        IsEnabled = true,
                        IsReminder = false,
                        TimeOfReminder = new TimeSpan(9, 30, 0)
                    });
                }

                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Username }, user);
        }

        // DELETE: api/Users/5
        [AuthFilter]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            if (!UserExists(id))
            {
                return BadRequest();
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return this.user.Username == id;
        }
    }
}