using System.ComponentModel;
using DataAccessLayer;


namespace ConsoleApp.Commands;

public class AddObjectCommand : ICommand
{
    private static readonly Dictionary<string, string[]> FieldSetByClassName = new Dictionary<string, string[]> ()
    {
        {"User", new []{ "FullName" }},
        {"Category", new []{"Name"}},
        {"Product", new []{"Name", "OwnerId", "CategoryId", "Price"}} 
    };

    private object CreateNewObject(Dictionary<string,string> objectData, string className)
    {
        var type = Type.GetType($"DataAccessLayer.Entities.{className}, DataAccessLayer");
        
        if (type is null || !FieldSetByClassName.ContainsKey(className))
        {
            throw new Exception($"Non existing type specified, valid types are: {String.Join(", ", FieldSetByClassName.Keys)}");
        }

        var instance = Activator.CreateInstance(type);
        var properties = instance.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (objectData.ContainsKey(property.Name))
            {
                var value = objectData[property.Name];
                if (property.PropertyType.Name == "String")
                {
                    property.SetValue(instance, value);
                }
                var converter = TypeDescriptor.GetConverter(property.PropertyType);
                
                if (!converter.CanConvertFrom(typeof(string))) continue;
                var convertedValue = converter.ConvertFromString(value);
                property.SetValue(instance, convertedValue);
            }
        }

        return instance;
    }


    private string PromptForFieldValue(string fieldName)
    {
        Console.WriteLine($"Enter {fieldName}");
        var name = Console.ReadLine();
        if (name is null) throw new Exception($"Invalid {fieldName}");
        return name;
    }
    
    public async void Execute()
    {
        Console.WriteLine("Enter the name of a class you want to add an entity of: ");
        var className = Console.ReadLine();
        var fieldset = FieldSetByClassName[className];
        var fieldValues = new Dictionary<string, string>();
        foreach (var field in fieldset)
        {
            var value = PromptForFieldValue(field);
            fieldValues.Add(field, value);
        }
        var obj = CreateNewObject(fieldValues, className);
        using (var applicationContext = new ApplicationContext())
        {
            applicationContext.Add(obj);
            await applicationContext.SaveChangesAsync();    
        }
    }
}