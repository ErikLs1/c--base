namespace App.DAL.EF.DataSeeding;

public static class InitialData
{
    public static readonly (string roleName, Guid? id)[]
        Roles =
        [
            ("admin", null),
            ("manager", null),
        ];


    public static readonly (string name, string firstName, string lastName, string password, Guid? id, string[] roles)[]
        Users =
        [
            ("admin@test.ee", "admin", "taltech", "Abc123-", null, ["admin"]),
            ("manager@test.ee", "manager", "taltech", "Abc123-", null, ["manager"]),
            ("user@test.ee", "user", "taltech", "Abc123-", null, []),
        ];
}  