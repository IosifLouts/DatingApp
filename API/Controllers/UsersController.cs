using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        //Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()  //First Endpoint that returns all the users from the DB
        {
          var users = await _userRepository.GetMembersAsync();
          return Ok(users);
        }

        //api/users/{id}

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUsers(string username)  //Second Endpoint that returns a specific user from the DB
        {
            return await _userRepository.GetMemberAsync(username);
                
        }
    }
}