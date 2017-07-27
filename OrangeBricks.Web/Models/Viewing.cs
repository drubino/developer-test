using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeBricks.Web.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Index("UX_Username_PropertyId", 1, IsUnique = true)]
        public string Username { get; set; }

        [Index("UX_Username_PropertyId", 2, IsUnique = true)]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public DateTime Date { get; set; }

        public ViewingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}