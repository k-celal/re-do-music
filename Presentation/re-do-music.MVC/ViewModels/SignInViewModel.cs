using System.ComponentModel.DataAnnotations;

namespace re_do_music.MVC.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
        }
        public SignInViewModel(string email, string password = null)
        {
            Email = email;
            Password = password;
        }
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        public string Email {  get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        public string Password { get; set; }

        public bool RememberMe {  get; set; }

    }
}
