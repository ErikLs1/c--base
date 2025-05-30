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
    
    
    public static readonly (string schoolName, string schoolAddress, Guid? id)[]
        Schools =
        [
            ("Tallinn High University",  "Väike-Ameerika 1, Tallinn",  null),
            ("Tartu Science University", "Riia 25, Tartu",            null),
            ("Viljandi University",   "Pargi 12, Viljandi",        null),
        ];
    
    public static readonly (string subjectName, string subjectCode, int eap,
        string schoolName, Guid? id)[]
        Subjects =
        [
            ("Mathematics I", "MTH101", 6, "Tallinn High University",  null),
            ("Physics I", "PHY101", 6, "Tallinn High University",  null),
            ("Estonian B1", "EST101", 4, "Tallinn High University",  null),
            ("Mathematics I","MTH101", 6, "Tartu Science University", null),
            ("Chemistry I", "CHE101", 6, "Tartu Science University", null),
            ("Biology I",  "BIO101", 6, "Tartu Science University", null),
            ("English A2", "ENG101", 4, "Viljandi University",   null),
            ("History I", "HIS101", 5, "Viljandi University",   null),
            ("Geography I", "GEO101", 5, "Viljandi University",   null),
        ];
    
    
    public static readonly (string semesterName, int semesterYear,  Guid? id)[]
        Semesters =
        [
            ("Spring", 2025, null),
            ("Autumn", 2025, null),
        ];
}  