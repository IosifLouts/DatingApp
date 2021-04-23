using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize] //This tribute requires the user to be authenticated
        [HttpGet("Auth")]
        public ActionResult<string> GetSecret() //test 401 Unauthorized responses
        {
            return "Secret Text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1); //We look for something that we know for sure is not gonna exist.

            if(thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1); //We look for something that we know for sure is not gonna exist.
            var thingToReturn = thing.ToString(); //Thing is surely null, because we do not have a user with a primary thing of -1. If you try to implement ToString method
            //to a null variable, then you get a null reference exception.
            return thingToReturn ;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}