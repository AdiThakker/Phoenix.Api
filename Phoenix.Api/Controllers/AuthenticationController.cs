using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Phoenix.Manager.Registration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Api.Controllers
{
    [Authorize]
    [Route("api/signin")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // return .ToList();
            Uri uri = new Uri("fabric:/Phoenix.Microservice.Registration/Phoenix.Manager.Registration.Service");
            IRegistrationManager registrationProxy = ServiceProxy.Create<IRegistrationManager>(uri);
            var user = registrationProxy.CreateNewUser(Guid.NewGuid(), "Sylvester", "Stallone").Result;
            var list = HttpContext.User.Claims.Select(claim => string.Concat(claim.Type, " : ", claim.Value)).ToList();
            list.Add($"New user: {user.UserObjectId} {user.FirstName} {user.LastName}");
            return list;
        }
    }
}