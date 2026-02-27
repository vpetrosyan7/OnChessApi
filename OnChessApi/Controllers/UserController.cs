using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnChessApi.Models;
using OnChessApi.Repository;

namespace OnChessApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MySqlRepository _mySqlRepository;

        public UserController(MySqlRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IResult> GetAsync()
        {
            List<UserModel> users = await Task.Run(() => _mySqlRepository.GetUsers());

            return Results.Json(users);
        }
    }
}
