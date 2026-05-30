using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using System.Security.Claims;

namespace ExercicioJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTestController : ControllerBase
    {


        [HttpGet("publico")]
      
        public string RotaPublica()
        {
            return "Parabens rota é Publica";
        }

        [HttpGet("protegido")]
        [Authorize]
        public string RotaProtegida()
        {
            return "Parabens rota é protegida";
        }



        [HttpPost("Login")]

        public string Login(string nome, string senha) 
        {
            if (nome == "david" && senha =="12345")
            {
                var chave = Encoding.ASCII.GetBytes("MinhaChaveSuperSecreta2027SenacTDS02");
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, nome),
                        new Claim(ClaimTypes.GivenName, "Black"),
                        new Claim(ClaimTypes.Country, "Mato Grosso do norte"),
                        new Claim(ClaimTypes.Role, "Dono da alemanha")
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescription);

                return "Autenticado com sucesso, seu token é " + tokenHandler.WriteToken(token);

                
            }
            return "Erro de Login. Usuário ou senha incorretos";
        }



    }
}
