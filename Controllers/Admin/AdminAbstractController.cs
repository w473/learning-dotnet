using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Events.Models;
using System.Web.Http;
using System.Net;

namespace Events.Controllers
{
    abstract public class AdminAbstractController : ControllerBase
    {

        protected User? _loggedUser;

        protected void bootUser(EventsContext context, IHttpContextAccessor httpContextAccessor, ILogger<AdminEventsController> logger)
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User != null)
            {
                var userGlobalId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                logger.LogInformation(userGlobalId);
                try
                {
                    _loggedUser = context.User.Where(u => u.GlobalId == userGlobalId).First();
                    logger.LogInformation("user found");
                }
                catch (InvalidOperationException)
                {
                    _loggedUser = new User
                    {
                        GlobalId = userGlobalId,
                        Name = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name)
                    };

                    context.User.Add(_loggedUser);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

        }
    }
}