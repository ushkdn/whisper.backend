﻿using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Dtos.User;
using Whisper.Data.Utils;
using Whisper.Services.UserService;

namespace Whisper.User.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService userService) : Controller
{
    [HttpPost("/register")]
    public async Task<ServiceResponse<string>> Register(UserRegisterDto user)
    {
        return await userService.Register(user);
    }
    
    [HttpPost("/login")]
    public async Task<ServiceResponse<string>> ForgotPassword(UserForgotPasswordDto user)
    {
        return await userService.ForgotPassword(user);
    }
    
    [HttpPost("/reset-password")]
    public async Task<ServiceResponse<string>> ResetPassword(UserResetPasswordDto user)
    {
        return await userService.ResetPassword(user);
    }
    
    [HttpPost("/log-in")]
    public async Task<ServiceResponse<string>> LogIn(UserLogInDto user)
    {
        return await userService.LogIn(user);
    }
}