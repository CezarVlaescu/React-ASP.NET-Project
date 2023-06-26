using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        
        public List<BasketItem> Items {get; set; } = new();

        public void addItem(Product product, int quanity)
        {
            if(Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem{Product = product, Quanity=quanity});
            }

            var existItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if(existItem != null) existItem.Quanity += quanity;
        }

        public void RemoveItem(int productId, int quanity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if(item==null) return;
            item.Quanity -= quanity;
            if(item.Quanity == 0) Items.Remove(item);   
        }
    }
}