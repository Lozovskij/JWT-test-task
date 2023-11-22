﻿using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class UserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}