public class ChangePasswordDto
{
    public int CustomerId { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordConfirm { get; set; } = null!;
}