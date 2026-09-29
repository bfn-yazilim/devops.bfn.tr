using System.ComponentModel.DataAnnotations;

namespace Bfn.DevOps.Models;

public sealed class LoginViewModel
{
    [Required, Display(Name = "Kullanıcı adı")] public string UserName { get; set; } = "";
    [Required, DataType(DataType.Password), Display(Name = "Parola")] public string Password { get; set; } = "";
}

public sealed class ChangePasswordViewModel
{
    [Required, DataType(DataType.Password), Display(Name = "Mevcut parola")] public string CurrentPassword { get; set; } = "";
    [Required, DataType(DataType.Password), Display(Name = "Yeni parola")] public string NewPassword { get; set; } = "";
    [Required, DataType(DataType.Password), Compare(nameof(NewPassword)), Display(Name = "Yeni parola (tekrar)")] public string ConfirmPassword { get; set; } = "";
}

public sealed class CreateUserViewModel
{
    [Required, Display(Name = "Kullanıcı adı")] public string UserName { get; set; } = "";
    [Required, DataType(DataType.Password), Display(Name = "Geçici parola")] public string Password { get; set; } = "";
    [Display(Name = "Yönetici")] public bool IsAdministrator { get; set; }
}
