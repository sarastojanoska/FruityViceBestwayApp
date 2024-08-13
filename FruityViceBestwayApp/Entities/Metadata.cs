using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruityViceBestwayApp.Entities
{
    public class Metadata
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FruitId")]
        public int FruitId { get; set; }
        public virtual Fruit Fruit { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

    }
}
