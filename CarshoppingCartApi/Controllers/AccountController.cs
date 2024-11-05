using CarshoppingCartApi.Const;
using CarshoppingCartApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarshoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = registerDto.UserName;
                user.Email = registerDto.Email;
                IdentityResult identityResult = await _userManager.CreateAsync(user,registerDto.Password);
                if (identityResult.Succeeded) 
                {
                    await _userManager.AddToRoleAsync(user,Role.User.ToString());

                    return Ok("User Add Succeeded");
                }
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            if (ModelState.IsValid) 
            {
                IdentityUser user = await _userManager.FindByNameAsync(loginDto.UserName);
                if (user != null) 
                {
                    bool check = await _userManager.CheckPasswordAsync(user,loginDto.Password);
                    if (check)
                    {
                        // add claims 
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, loginDto.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        //get role 
                        var role = await _userManager.GetRolesAsync(user);
                        foreach (var roleClaim in role) 
                        {
                            claims.Add(new Claim (ClaimTypes.Role,roleClaim));
                        }

                        // Jwt
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        SigningCredentials credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                       JwtSecurityToken securityToken = new JwtSecurityToken
                            (    
                                 issuer:_configuration["JWT:ValidIssuer"],
                                 audience:_configuration["JWT:ValidAudience"],
                                 claims: claims,
                                 expires:DateTime.Now.AddDays(1),
                                 signingCredentials: credentials

                            );
                        string TokenValue =new JwtSecurityTokenHandler().WriteToken(securityToken);
                        return Ok(new
                        {   
                            Token = TokenValue,
                            User =user,
                            //expires = securityToken.ValidTo
                        }
                            
                            
                        );
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();

        }
      
       
        

        
    }
   
}
