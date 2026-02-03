using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InterUniversity.Application.Abstractions.Context;

public class ContextAccessor(IHttpContextAccessor httpContextAccessor) : IContextAccessor
{
    public string UserId { get => httpContextAccessor.HttpContext!.User.Identity!.Name!; }
    public string UserName { get => httpContextAccessor.HttpContext!.User!.Identity!.Name!; }
    public string UserMail { get => throw new NotImplementedException(); }
    public string ClientIP { get => $"{httpContextAccessor.HttpContext!.Connection.RemoteIpAddress}"; }
    public string Headers { get => JsonConvert.SerializeObject(httpContextAccessor.HttpContext!.Request.Headers); }
    public string SessionId { get => httpContextAccessor.HttpContext!.User.Claims.First(claim => claim.Type == "jti").Value; }
}