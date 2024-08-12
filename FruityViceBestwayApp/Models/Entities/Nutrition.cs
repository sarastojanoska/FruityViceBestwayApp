using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FruityViceBestwayApp.Models.Entities
{
    public class Nutrition
    {
        [Key, ForeignKey("Fruit")]
        public int FruitId { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Calories { get; set; }
        public double Sugar { get; set; }
        public virtual Fruit Fruit { get; set; }
    }
}
