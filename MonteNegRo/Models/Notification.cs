using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MonteNegRo.Models
{
    public record Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Notification_ID { get; init; }
        public string? Description { get; init; }
        public DateTimeOffset ActivationDate { get; init; }
        public bool IsActive { get; init; }


        public virtual Property Property { get; init; }
        public long Property_ID { get; init; }
    }
}
