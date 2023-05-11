using System;

namespace AuthorizationApi.Models
{
    public class Account
    {
        public long ID { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string UserType { get; init; }
    }
}
