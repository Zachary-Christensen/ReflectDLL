# ReflectDLL

This project is to be used as a command line tool. 
By giving the path to a DLL built using .NET, each classes dependencies are saved in a CSV file.
Dependencies are only saved when the type of the dependency can be found within the same DLL.

Example usage


  --Open terminal and change directory to the folder with the solution
  
  
  --Run the command "dotnet build"
  
  
  --Change directory to "./ReflectDLL/bin/Debug/net6.0" * the number after /net may vary *
  
  
  --Run the command "dotnet ./ReflectDLL.dll [pathToDLL]"
  
  
  --Move the generated CSV files "Dependencies.csv" and "TypeNames.csv" to the folder with the GraphDependencies.py file
  
  
  --Now, running ./GraphDependencies.py will output a digraph of the dependencies

Notes:
  Very large graphs may not be readable
  
  
