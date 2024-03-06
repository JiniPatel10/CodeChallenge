using System.ComponentModel.DataAnnotations;

namespace Rockfast.ViewModels
{
    public class TodoVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Complete { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int UserId { get; set; } 

    }
}