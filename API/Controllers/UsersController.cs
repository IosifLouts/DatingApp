using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context) //Dependency injection in order to gain access to our DB
        {
            _context = context; //With this parameter, this class has access to our database
        }
        //Actions
        [HttpGet]
        [AllowAnonymous]
        public async  Task<ActionResult<IEnumerable<AppUser>>> GetUsers()  //First Endpoint that returns all the users from the DB
        {
               return await  _context.Users.ToListAsync();
        }

        //api/users/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id)  //Second Endpoint that returns a specific user from the DB
        {
               return await _context.Users.FindAsync(id);   
        }
    }
}