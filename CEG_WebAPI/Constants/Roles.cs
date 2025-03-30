namespace CEG_WebAPI.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Parent = "Parent";
        public const string Student = "Student";

        public static string GetSelectedRoles(string[] roles)
        {
            string roleList = "";
            foreach (var role in roles)
            {
                if(!roleList.Contains(role))
                    roleList += !roleList.Contains(',') ? role : "," + role;
            }
            return roleList;
        }
    }
}
