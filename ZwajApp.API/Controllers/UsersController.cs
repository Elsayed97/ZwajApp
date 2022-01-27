using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwajApp.Api.Data;
using ZwajApp.Api.ViewModels;
using ZwajApp.API.Models;

namespace ZwajApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IZwajRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IZwajRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersToReturn = await _repo.GetUsers();
            var users = _mapper.Map<IEnumerable<UserForListVM>>(usersToReturn);
            return Ok(users);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userToReturn = await _repo.GetUser(id);
            var user = _mapper.Map<UserForDetailsVM>(userToReturn);
            return Ok(user);
        }

    }
}
