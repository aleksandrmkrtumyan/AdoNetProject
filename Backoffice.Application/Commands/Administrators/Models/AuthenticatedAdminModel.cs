namespace Backoffice.Application.Commands.Administrators.Models;

public class AuthenticatedAdminModel
{
    /// <summary>
    /// Токен
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
}