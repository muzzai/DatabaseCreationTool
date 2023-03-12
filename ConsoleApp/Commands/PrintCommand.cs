using System.Text;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace ConsoleApp.Commands;

public class PrintCommand : ICommand
{

    private string CreateTableRow(List<int> columnWidth, List<string> values)
    {
        var result = new StringBuilder();
        for (int i = 0; i < values.Count; i++)
        {
            result.Append("| ");
            var record = values[i].PadRight(columnWidth[i]);
            result.Append(record);
            result.Append(" |");
        }

        return result.ToString();
    }
    
    private string RenderTable<T>( List<T> entities) where T : IPrintable
    {
        var tableData = new List<List<string>>();
        var columnNames = T.GetColumnsNames();
        tableData.Add(columnNames);
        var tableRows = entities.Select(x => x.GetColumnsValues());
        tableData.AddRange(tableRows);
        
        var columnsWidths = new List<int>() { };
        for (int i = 0; i < columnNames.Count; i++)
        {
            var column = tableData.Select(x => x[i]).ToList();
            var maxLength = column
                .Select(x => x)
                .MaxBy(x => x.Length)!
                .Length;
            columnsWidths.Add(maxLength);
        }
        
        return String.Join('\n', tableData.Select(x => CreateTableRow(columnsWidths, x)));
    }
    public async void Execute()
    {
        using (var applicationContext = new ApplicationContext())
        {
            var users = await applicationContext.Users.ToListAsync();
            var categories = await applicationContext.Categories.ToListAsync();
            var products = await applicationContext.Products.ToListAsync();
            var usersTable = RenderTable(users);
            var categoryTable = RenderTable(categories);
            var productTable = RenderTable(products);
            Console.WriteLine(usersTable + '\n');
            Console.WriteLine(categoryTable + '\n');
            Console.WriteLine(productTable + '\n');
        }
        
    }
}