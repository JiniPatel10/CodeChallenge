using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rockfast.ViewModels;

namespace Rockfast.ApiDatabase.DomainModels
{
    public class Todo
    {
        public Todo()
        {
            DateCreated = DateTime.UtcNow;
        }

        public Todo(TodoVM model)
          : this()
        {
            Id = model.Id;
            Name = model.Name;
            Complete = model.Complete;
            DateCompleted = model.DateCompleted;
            UserId = model.UserId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Complete { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int UserId { get; set; } // Foreign key for User
        public User User { get; set; } // Navigation property


    }
}
