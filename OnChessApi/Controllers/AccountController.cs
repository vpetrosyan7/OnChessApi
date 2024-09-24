using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using OnChessApi.Models;
using OnChessApi.Repository;
using OnChessApi.Services;

namespace OnChessApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly MySqlRepository _mySqlRepository;

        public AccountController(MySqlRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        [HttpPost]
        public async Task<IResult> Register()
        {
            using (StreamReader reader = new(this.HttpContext.Request.Body))
            {
                string postData = await reader.ReadToEndAsync();

                UserModel? model = JsonConvert.DeserializeObject<UserModel>(postData);

                if (model != null && _mySqlRepository.AddUser(model))
                {
                    return Results.Json(new { access_token = new JwtService(model).GetToken() });
                }
            }

            return Results.NoContent();
        }
    }
}
