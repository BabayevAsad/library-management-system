using Api.Base;
using Api.Roles;

namespace Api.User;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
}