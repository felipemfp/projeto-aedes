using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Aedes.Models;

namespace Aedes.Filters
{
    public class AuthFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Dictionary<string, string> query = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            if (query.ContainsKey("key"))
            {
                string key = query["key"];
                using (var db = new AedesDBContext())
                {
                    if (db.Users.FirstOrDefault(u=> u.Key == key) != null)
                    {
                        base.OnActionExecuting(actionContext);
                        return;
                    }
                }
            }
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}