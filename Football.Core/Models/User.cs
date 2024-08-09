﻿

namespace Football.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public required RefreshToken RefreshToken { get; set; } = null!;
}