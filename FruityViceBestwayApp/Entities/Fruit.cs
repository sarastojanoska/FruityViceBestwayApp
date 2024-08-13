using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruityViceBestwayApp.Entities
{
    public class Fruit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Genus { get; set; }
        public string Order { get; set; }
        public virtual Nutrition Nutrition { get; set; }
        public virtual ICollection<Metadata> Metadata { get; set; }
    }
}
