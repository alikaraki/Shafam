﻿namespace Shafam.Common.DataModel
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public int? UserId { get; set; }
        public bool Disabled { get; set; }
    }
}