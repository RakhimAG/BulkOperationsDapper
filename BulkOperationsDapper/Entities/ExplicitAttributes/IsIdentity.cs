using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkOperationsDapper.Entities.ExplicitAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class IsIdentity : Attribute
{
}
