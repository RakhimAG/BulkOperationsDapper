using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using BulkOperationsDapper.Entities.ExplicitAttributes;

namespace BulkOperationsDapper.DapperExtensions;

static class BulkOperations
{
    // THIS BULK INSERT ONLY SUPPORTS STRING AND INTEGER TYPED PROPERTIES :(
    public static async Task BulkInsertAsync<T>(this IDbConnection connection, IEnumerable<T> values)
    {
        // DETERMINING TABLE NAME
        // before
        //string tableName = $"{typeof(T).Name}s";
        // after
        string tableName = GetTableName<T>();

        // THE PART THAT COMES AFTER VALUES KEYWORD IN SQL
        StringBuilder valuesInSql = new("");
        for (int i = 0; i < values.Count(); i++)
        {
            var obj = values.ElementAt(i);

            valuesInSql.Append('(');
            valuesInSql.Append
            (
                string.Join
                (
                    ',',
                    obj!.GetType()
                       .GetProperties()
                       .Where( p => (
                                p.PropertyType.IsPrimitive ||
                                p.PropertyType == typeof(string) ||
                                p.PropertyType == typeof(decimal) ||
                                p.PropertyType == typeof(DateTime) ||
                                p.PropertyType == typeof(Guid) ||
                                p.PropertyType == typeof(DateOnly)
                            ) &&
                            p.GetCustomAttribute<IsIdentity>() is null
                        )
                       .Select( p => 
                       {
                           if (p.PropertyType == typeof(string))
                               return $"\'{p.GetValue(obj)}\'" ?? "NULL";
                           else
                               return p.GetValue(obj)?.ToString() ?? "NULL";
                       })
                )
            );
            valuesInSql.Append(')');

            if (i !=  values.Count() - 1)
                valuesInSql.Append(',');
        }

        // FINALLY

        var query = @$"
        INSERT INTO {tableName}
        VALUES {valuesInSql.ToString()};
        ";

        await connection.ExecuteAsync(query);
    }

    public static async Task BulkUpdateAsync<T>(this IDbConnection connection, IEnumerable<T> values)
    {
        string tableName = GetTableName<T>();

        StringBuilder valuesInSql = new("");

        var query = $@"
        UPDATE {tableName}        
        SET {3}
        FROM {tableName}
        JOIN (
            VALUES
            {1}
        ) v(Id, {2} )
        ON v.Id = {tableName}.Id;
        "; // 1 => (obj1.prp1, obj1.prp2), (obj2.prp1,...)... 
        // 2 => (prp1, prp2,....)
        // 3 tableName.prp1 = v.prp1..... with other prps too that are not null
    }

    // ============= HELPER METHODS ==============
    private static string GetTableName<T>()
    {
        var tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();

        return tableAttribute!.Name ??
            throw new InvalidOperationException
            (
                $"Type {typeof(T).Name} doesn't have a Table attribute."
            );
    }
}
