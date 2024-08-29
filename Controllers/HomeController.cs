using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ApiAuth.Models;
using System.IdentityModel.Tokens.Jwt; 
using Microsoft.IdentityModel.Tokens; 
using Microsoft.AspNetCore.Authorization;

namespace ApiAuth.Controllers
{   
    [ApiController]
    public class HomeController : ControllerBase
    {   
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado - {User.Identity.Name}";
    }
}
