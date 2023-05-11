using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonteNegRo.Models
{
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserType_ID { get; init; }
        public string UserTypeName { get; init; }
        public virtual IEnumerable<User>? Users { get; init; }
    }
}
