using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(String.Join(',', args));
        if (args.Length < 1)
        {
            Console.WriteLine("Must enter path to DLL as argument. example usage ./ReflectDLL.exe [pathToDLL]");
            return;
        }

        string filename = args[0];
        Assembly assem;
        try
        {
            assem = Assembly.LoadFrom(filename);

            List<string> typeNames = new();
            foreach (var type in assem.GetTypes())
            {
                if (type.IsEnum) continue;

                typeNames.Add(type.Name);
            }

            PrintDependencies(GetDependencies(assem, typeNames));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.Exit(-1);
        }

        
    }

    private static void PrintDependencies(Dictionary<string, List<string>> dependencies)
    {
        Console.WriteLine("DEPENDENCIES");
        foreach (var key in dependencies.Keys)
        {
            if (dependencies[key].Count > 0)
            {
                Console.WriteLine($"{key}");
                dependencies[key].ForEach(x => Console.WriteLine($"\t{x}"));
            }
        }
    }

    private static Dictionary<string, List<string>> GetDependencies(Assembly assem, List<string> typeNames)
    {
        Dictionary<string, List<string>> dependencies = new();

        foreach (var type in assem.GetTypes())
        {
            if (dependencies.ContainsKey(type.Name))
            {
                Console.WriteLine($"Dictionary already contains {type.Name}");
            }
            else
            {
                dependencies.Add(type.Name, new());
                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (typeNames.Contains(field.FieldType.Name))
                    {
                        dependencies[type.Name].Add(field.FieldType.Name);
                    }
                }
                foreach (var property in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (typeNames.Contains(property.PropertyType.Name))
                    {
                        dependencies[type.Name].Add(property.PropertyType.Name);
                    }
                }
            }
        }
        return dependencies;
    }
}

