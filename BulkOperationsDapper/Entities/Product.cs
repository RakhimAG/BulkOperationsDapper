using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkOperationsDapper.Entities.ExplicitAttributes;

namespace BulkOperationsDapper.Entities;

[Table("Products")]
public class Product
{
    [IsIdentity]
    public int Id { get; set; }
    public string Name { get ; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
