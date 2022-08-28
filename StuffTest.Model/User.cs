using System;
using System.ComponentModel.DataAnnotations;

namespace StuffTest.Model
{
    public class User : IEntityBase
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }
        [MaxLength(60)]
        public string MiddleName { get; set; }
        [Required]   
        public Guid PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}