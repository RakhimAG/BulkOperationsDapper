using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkOperationsDapper.Entities.ExplicitAttributes;

namespace BulkOperationsDapper.Entities;

[Table("Categories")]
public class Category
{
    [IsIdentity]
    public int Id { get; set; }
    public string Name { get; set; }
}
