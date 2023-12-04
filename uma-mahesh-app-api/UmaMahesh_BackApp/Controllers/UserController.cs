using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;

using UmaMahesh_BackApp.Models.User;

namespace UmaMahesh_BackApp.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DataRepositoryContext _dataRepositoryContext;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _configuration;

    public static UserRegistration user = new UserRegistration();

    public UserController(DataRepositoryContext dataRepositoryContext , ILogger<UserController> logger,IConfiguration configuration)
    {
        _dataRepositoryContext = dataRepositoryContext;
        _logger = logger;
        _configuration = configuration;
    }


    [HttpPost("register")]
    public async Task<ActionResult<UserRegistration>> Register(UserDto request)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Bad Request recieved having {ModelState}");
                return BadRequest(ModelState);
            }

            var userName = await _dataRepositoryContext.UserRegistration.FirstOrDefaultAsync(us => us.UserName == request.UserName);
            if (userName != null)
            {
                return BadRequest($"UserName '{request.UserName.ToString()}' has already taken, Please try with another");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dataRepositoryContext.UserRegistration.AddAsync(user);
            _dataRepositoryContext.SaveChanges();

            return Ok();
        }
        catch (Exception)
        {
            throw;
        }
        }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto request)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Bad Request recieved having {ModelState}");
                return BadRequest(ModelState);
            }
            var userName = await _dataRepositoryContext.UserRegistration
                                 .FirstOrDefaultAsync(us => us.UserName == request.UserName);

            if (userName == null)
            {
                return BadRequest("User Not Found, Please try with registered Username");
            }

            if (!VerifyPassword(request.Password, userName.PasswordHash!, userName.PasswordSalt!))
            {
                return BadRequest("Wrong Password, Please try with correct password");
            }

            string token = CreateToken(userName);
            return Ok(token);
        }
        catch (Exception)
        {
            throw;
        }
        }

    private string CreateToken(UserRegistration user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("JwtSettings:Key").Value!));

        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash , out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }
    }


    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using ( var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }







}
