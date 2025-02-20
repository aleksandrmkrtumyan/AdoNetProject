using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.AuthOption;

public static class BackofficeAuthOptions
{
    /// <summary>
    /// Key
    /// </summary>
    public const string Key = "AdoNetProject_3erddj394sf89vsnl866/9*s57";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}