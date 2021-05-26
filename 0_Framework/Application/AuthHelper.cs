﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void SignIn(AuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

             _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Claims.Any();
        }

        public string GetCurrentUserRole()
        {
            if (IsAuthenticated())
            {
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            }

            return null;
        }

        public AuthViewModel GetCurrentUserInfo()
        {
            var authViewModel = new AuthViewModel();
            if (IsAuthenticated())
            {
                var claims = _contextAccessor.HttpContext.User.Claims.ToList();

                authViewModel.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                authViewModel.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
                authViewModel.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
                authViewModel.Username = claims.FirstOrDefault(x => x.Type == "Username").Value;
                authViewModel.Role = Roles.GetRoleBy(authViewModel.RoleId);
            }

            return authViewModel;

        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
