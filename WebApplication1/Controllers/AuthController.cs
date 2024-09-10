using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using domain_and_repo.DTO;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data;
using System.Linq;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration configuration;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.configuration = configuration;
        }


        // Registration Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                return Ok("User registered and logged in successfully!");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }




        // Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] login model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser? user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Invalid login attempt.");
            }

            var passwordCheckResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordCheckResult)
            {
                return Unauthorized("Invalid login attempt.");
            }

            var claims = new List<Claim>();
            //  claims.Add(new Claim("tokenNO", "75"));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var roles = await _userManager.GetRolesAsync(user);

            var allClaims = roles.Select(a => new Claim(ClaimTypes.Role, a.ToString()));
            claims.AddRange(allClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: sc
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenValue);
        }

        // Logout Endpoint
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out successfully!");
        }

        // Check if user is authenticated
        [HttpGet("authenticated")]
        public IActionResult IsAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
                return Ok("User is authenticated.");
            else
                return Unauthorized("User is not authenticated.");
        }
    }
}
