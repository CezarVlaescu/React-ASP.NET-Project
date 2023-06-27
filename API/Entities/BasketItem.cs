using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("BasketItems")]
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quanity {get; set; }

        // navigation prop
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int basketId { get; set; } // specify the name of entity

        public basket Basket {get; set;} 
    }
}