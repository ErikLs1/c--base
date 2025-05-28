using System.Security.Claims;
using App.BLL.Contracts;
using App.DTO.V1.DTO;
using App.DTO.V1.Mappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiController;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PersonsController : ControllerBase
{

    private readonly IAppBll _bll;
    private readonly ProfileInfoMapper _mapper = new ProfileInfoMapper();
    
    /// <inheritdoc />
    public PersonsController(IAppBll bll)
    {
        _bll = bll;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType( typeof( ProfileInfoDto ), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<ProfileInfoDto>> GetProfileInfo()
    {
        var userId = User.GetUserId();
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        var bll = await _bll.PersonService.GetProfileAsync(userId);
        var res = _mapper.Map(bll, userId, email)
                  ?? throw new InvalidOperationException("Profile not found");
        return res;
    }
}