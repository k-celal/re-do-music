﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ReDoMusic.Domain.Entites;
using System.Text;

namespace re_do_music.MVC.TagHelpers
{
    public class UserRoleNamesTagHelper : TagHelper
    {
        public string UserId { get; set; }
        private readonly UserManager<AppUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var userRoles = await _userManager.GetRolesAsync(user);

            var stringBuilder = new StringBuilder();

            userRoles.ToList().ForEach(x =>
            {
                stringBuilder.Append(@$"<span class='badge bg-secondary m-lg-1' >{x.ToLower()}</span>");
            });
            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}
