﻿namespace re_do_music.MVC.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string[] Roles { get; set; }
    }
}
