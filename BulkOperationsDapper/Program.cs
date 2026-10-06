using BulkOperationsDapper.DapperExtensions;
using BulkOperationsDapper.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

var connectionString =
    "SERVER=localhost;Database=BulkInsertTestingDB;Trusted_Connection=true;TrustServerCertificate=true;";
var connection = new SqlConnection(connectionString);

#region BULK INSERT OF CATEGORIES AND PRODUCTS
if (false)
{
    var categories = new List<Category>()
    {
        new Category
        {
            Name = "Electronics"
        },
        new Category
        {
            Name = "Exclusive Items"
        }
    };
    await connection.BulkInsertAsync<Category>(categories);

    var products = new List<Product>
    {
        new Product
        {
            Name = "Iphone 99",
            CategoryId = 1,
            Price = 999m,
            Stock = 10,
        },
        new Product
        {
            Name = "RTX 7090",
            CategoryId = 1,
            Price = 399m,
            Stock = 20,
        },
        new Product
        {
            Name = "Ketchup",
            CategoryId = 2,
            Price = 99999.99m,
            Stock = 1,
        },
        new Product
        {
            Name = "Wireless Mouse",
            CategoryId = 1,
            Price = 29.99m,
            Stock = 45
        },
        new Product
        {
            Name = "Mechanical Keyboard",
            CategoryId = 1,
            Price = 89.50m,
            Stock = 20
        },
        new Product
        {
            Name = "USB-C Hub",
            CategoryId = 1,
            Price = 34.99m,
            Stock = 60
        },
        new Product
        {
            Name = "Gaming Headset",
            CategoryId = 1,
            Price = 74.99m,
            Stock = 15
        },
        new Product
        {
            Name = "Office Chair",
            CategoryId = 2,
            Price = 159.99m,
            Stock = 8
        },
        new Product
        {
            Name = "Desk Lamp",
            CategoryId = 2,
            Price = 39.95m,
            Stock = 32
        },
        new Product
        {
            Name = "Monitor Stand",
            CategoryId = 2,
            Price = 54.90m,
            Stock = 18
        }
    };

    await connection.BulkInsertAsync<Product>(products);
}
#endregion


