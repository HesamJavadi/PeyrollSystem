namespace PayrollSystem.Domain.Contracts.Request.UserRoleAssignment
{
    public class UsersRoleAssignmentRequest
    {
        public string[] Usernames { get; set; }
        public string RoleName { get; set; }
    }
}
