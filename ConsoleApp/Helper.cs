using DataAccessLayer;
using DataAccessLayer.Entities;

namespace ConsoleApp;

public abstract class Helper
{
    public static async Task AddEntities(int rowsNumber)
    {
        string[] categoriesNames;
        categoriesNames = new[] {
            "Sports",
            "Home",
            "Pets",
            "Arts",
            "Garden"
        };

        var rnd = new Random();
        using (var context = new ApplicationContext())
        {
            foreach (var categoryName in categoriesNames)
            {
                var catgory = new Category()
                {
                    Name = categoryName
                };
                context.Add(catgory);
            }
            await context.SaveChangesAsync();
        
            for (var i = 0; i < rowsNumber; i++)
            {

                var user = new User()
                {
                    FullName = Faker.Name.FullName()
                };
                context.Add(user);
                await context.SaveChangesAsync();
                var category = await context.Categories.FindAsync(rnd.Next(1, categoriesNames.Length));
                var product = new Product()
            
                {
                    CategoryId = category!.CategoryId,
                    OwnerId = user.UserId,
                    Name = string.Join(' ', Faker.Lorem.Words(3))
                };
                context.Add(product);
                await context.SaveChangesAsync();
            }    
        }
        
    }
}