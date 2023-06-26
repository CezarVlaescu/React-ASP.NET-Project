namespace API.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quanity {get; set; }

        // navigation prop
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}