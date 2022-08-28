using System;
using System.ComponentModel.DataAnnotations;

namespace StuffTest.Model
{
    public class Position : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
    }
}