﻿using System;


namespace TaskManager
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public User(string email)
        {
            Id = Guid.NewGuid();
            Email = email;
        }
    }
}
