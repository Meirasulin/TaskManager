using System;


namespace TaskManager
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public User(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
