using System.ComponentModel.DataAnnotations;

namespace TheLions.Models
{
    [Serializable]
    public class Entity<T> 
    {
        [Key]
        public T Id { get; set; }
    }
}
