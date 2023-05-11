using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public record User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long User_ID { get; init; }
        public string Email { get; init; }
        public string Username { get; init; }
        public string PasswordHash { get; init; }


        public long? UserType_ID { get; init; }
        public virtual UserType? UserType { get; init; }
    }
}
