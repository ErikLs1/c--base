using App.BLL.Services;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiController.Identity;


/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly AccountMapper _accountMapper;

    /// <inheritdoc />
    public AccountController(AccountService accountService, AccountMapper accountMapper)
    {
        _accountService = accountService;
        _accountMapper = accountMapper;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT access token and refresh token.
    /// </summary>
    /// <param name="loginInfo">The users login credentials.</param>
    /// <param name="jwtExpiresInSeconds">(Optional) Custom expiration time for JWT in seconds.</param>
    /// <param name="refreshTokenExpiresInSeconds">(Optional) Custom expiration time for the issued refresh token, in seconds.</param>
    /// <returns>
    /// 200 OK containing the access token, refresh token and role;
    /// 404 Not Found if provided credentials are invalid.
    /// </returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JwtResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<JwtResponseDto>> Login(
        [FromBody]
        LoginDto loginInfo,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {

        var res = await _accountService.LoginAsync(_accountMapper.Map(loginInfo)!, jwtExpiresInSeconds,
            refreshTokenExpiresInSeconds);
        return Ok(_accountMapper.Map(res));
    }
    
    /// <summary>
    /// Registers a new user and issues a JWT access token, refresh token and role of the user.
    /// </summary>
    /// <param name="registerModel">The users registration details.</param>
    /// <param name="jwtExpiresInSeconds">(Optional) Custom expiration time for JWT in seconds.</param>
    /// <param name="refreshTokenExpiresInSeconds">(Optional) Custom expiration time for the issued refresh token, in seconds.</param>
    /// <returns>
    /// 200 OK containing the access token, refresh token and role;
    /// 404 Not Found if registration data is invalid or user already exists.
    /// </returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JwtResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<JwtResponseDto>> Register(
        [FromBody]
        RegisterDto registerModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds)
    {
        var res = await _accountService.RegisterAsync(_accountMapper.Map(registerModel)!, jwtExpiresInSeconds,
            refreshTokenExpiresInSeconds);
        return Ok(_accountMapper.Map(res));
    }
    
    /// <summary>
    /// Renews expired refresh token and issues new tokens.
    /// </summary>
    /// <param name="refreshTokenModel">The current jwt and refresh token.</param>
    /// <param name="jwtExpiresInSeconds">(Optional) Custom expiration time for JWT in seconds.</param>
    /// <param name="refreshTokenExpiresInSeconds">(Optional) Custom expiration time for the issued refresh token, in seconds.</param>
    /// <returns>
    /// 200 OK containing the access token, refresh token and role;
    /// 400 Bad Request if refresh token is invalid.
    /// 500 Internal Server Error for unexpected errors.
    /// </returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JwtResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<JwtResponseDto>> RenewRefreshToken(
        [FromBody]
        RefreshTokenDto refreshTokenModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        var bllDto = _accountMapper.Map(refreshTokenModel)!;
        var res = await _accountService.RenewTokenAsync(bllDto, jwtExpiresInSeconds, refreshTokenExpiresInSeconds);
        return Ok(_accountMapper.Map(res));
    }
    
    /// <summary>
    /// Kicks out the user and deletes the refresh token.
    /// </summary>
    /// <param name="logout">The refresh token to delete.</param>
    /// <returns>
    /// 200 OK if logout was successful;
    /// 404 Not Found if given token was not found.
    /// </returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutDto logout)
    {
        var userId = User.GetUserId();
        await _accountService.LogoutAsync(userId, logout.RefreshToken);
        return Ok();
    }
}