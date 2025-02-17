namespace Api.Roles;

public enum Role
{
    StoreAdmin=1,
    Admin=2,
    Guest=3
}

public static class RoleHelper
{
    public static Role GetById(int roleId)
    {
        return (Role)roleId;
    }
}
