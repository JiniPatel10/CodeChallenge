using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockfast.ViewModels;
using System.Numerics;

namespace Rockfast.ApiDatabase.DomainModels
{
    public class User
    {
        public User()
        {
        }

        public User(UserVM model)
        : this()
        {
            Id = model.Id;
            Name = model.Name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; }

    }
}
