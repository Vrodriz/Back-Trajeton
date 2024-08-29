using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services; // Certifique-se de que este using está incluído

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            var user = UserRepository.Get(model.Email, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("alterar-senha")]
        public async Task<ActionResult<dynamic>> ChangePasswordAsync([FromBody] ChangePasswordModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.NewPassword))
                return BadRequest(new { message = "Email e nova senha são obrigatórios." });

            var userExists = UserRepository.GetByEmail(model.Email) != null;

            if (!userExists)
                return NotFound(new { message = "Usuário não encontrado" });

            if (model.NewPassword != model.ConfirmPassword)
                return BadRequest(new { message = "As novas senhas não coincidem" });

            var success = UserRepository.UpdatePassword(model.Email, model.NewPassword);

            if (!success)
                return StatusCode(500, new { message = "Erro ao atualizar a senha" });

            return Ok(new { message = "Senha alterada com sucesso" });
        }

    }
}
