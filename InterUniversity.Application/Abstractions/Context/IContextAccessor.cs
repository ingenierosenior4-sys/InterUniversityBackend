namespace InterUniversity.Application.Abstractions.Context;

public interface IContextAccessor
{
    string UserId { get; }
    string UserName { get; }
    string UserMail { get; }
    string ClientIP { get; }
    string Headers { get; }
    string SessionId { get; }
}
