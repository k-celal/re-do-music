using System.ComponentModel.DataAnnotations;

namespace re_do_music.MVC.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Rol ismi boş bırakılamaz.")]
        [Display(Name = "Rol ismi")]
        public string Name { get; set; }
    }
}
