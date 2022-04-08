namespace WebApiAuth.Controllers
{
    using WebApiAuth.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Net.Http.Headers;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        //[Authorize]
        [HttpGet]
        [Route("tokeninfo")]
        public async Task<IActionResult> TokenInfo()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return BadRequest("!User.Identity.IsAuthenticated");
            }


            //var accessToken = Request.Headers[HeaderNames.Authorization]
            //var userName = User.Identity.Name;
            //var expClaim = User.Claims.First(x => x.Type == "exp");
            //var expTime = new TimeSpan(long.Parse(expClaim.Value));
            //var expiration = DateTime.Now + expTime;

            //var jwtExpValue = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "exp").Value);
            //DateTime expirationTime = DateTimeOffset.FromUnixTimeSeconds(jwtExpValue).DateTime;

            var expClaim = User.FindFirstValue("exp");
            var jwtExpValue = long.Parse(expClaim);
            DateTimeOffset expirationTime = DateTimeOffset.FromUnixTimeSeconds(jwtExpValue).DateTime;

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userRoles = await userManager.GetRolesAsync(user);
            return Ok(new { Status = "Success", Message = "Access token is active!", Expires = expirationTime, Roles = string.Join(", ", userRoles) });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Поиск пользователя по полю UserName
            var user = await userManager.FindByNameAsync(model.Username);

            // Если пользователь не найден или не верный пароль,
            // возвращаем результат, что пользователь не авторизован
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            // Права пользователя
            var userRoles = await userManager.GetRolesAsync(user);

            // Идентификационные поля записываемые в токен
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            //{
            //  "access_token": "1/fFAGRNJru1FTz70BzhT3Zg",
            //  "expires_in": 3920,
            //  "token_type": "Bearer",
            //  "scope": "https://www.googleapis.com/auth/youtube.force-ssl",
            //  "refresh_token": "1//xEoDL4iW3cxlI7yDbSRFYNG01kVKM2C-259HOF2aQbI"
            //}
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo.ToLocalTime()
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
