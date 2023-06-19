using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            if(context.Products.Any()) return;

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Product name",
                    Description = "Very nice product",
                    Price = 300,
                    PictureUrl = "",
                    Brand = "NetCore",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Product name1",
                    Description = "Very nice product",
                    Price = 1300,
                    PictureUrl = "",
                    Brand = "NetCore",
                    Type = "Boards",
                    QuantityInStock = 10
                },
                new Product
                {
                    Name = "Product name2",
                    Description = "Very nice product",
                    Price = 2300,
                    PictureUrl = "",
                    Brand = "NetCore",
                    Type = "Boards",
                    QuantityInStock = 90
                },
                new Product
                {
                    Name = "Product name3",
                    Description = "Very nice product",
                    Price = 3300,
                    PictureUrl = "",
                    Brand = "NetCore",
                    Type = "Boots",
                    QuantityInStock = 2100
                },new Product
                {
                    Name = "Product name4",
                    Description = "Very nice product",
                    Price = 4300,
                    PictureUrl = "",
                    Brand = "Angular",
                    Type = "Boots",
                    QuantityInStock = 3100
                },

            };

            foreach(var product in products){
                context.Products.Add(product);
            }

            context.SaveChanges();

        }
    }
}