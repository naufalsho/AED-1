namespace Domain.Account
{
    public class AuthLoginDto
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordDto
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }

    public class UserSessionDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public UserRoleDto Role { get; set; }
    }

    public class UserRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserMenuGroupDto
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsDirectMenu { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<UserMenuDto> Menus { get; set; }
    }

    public class UserMenuDto
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public bool AllowView { get; set; } = false;
        public bool AllowCreate { get; set; } = false;
        public bool AllowEdit { get; set; } = false;
        public bool AllowDelete { get; set; } = false;
        public bool IsActive { get; set; }

    }
}
