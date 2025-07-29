using System.IO;
using UnityEngine;
using UnityEditor;
using static UnityEditor.AssetDatabase;

public static class Setup
{
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders()
    {
        string defaultFolderPath = "";

        FoldersCreator.CreateDefault(defaultFolderPath, "Animations", "Materials", "Prefabs", "Scripts", "ScriptableObjects");
        FoldersCreator.CreateDefault(defaultFolderPath + "Animations", "Animations Clips", "Animator Cotrollers");
        FoldersCreator.CreateDefault(defaultFolderPath + "Materials", "Materials", "Models", "Textures");
        FoldersCreator.CreateDefault(defaultFolderPath + "Scripts", "Input");

        SctiptCreator.CreateDefault(defaultFolderPath + "Scripts", "Test", "Test2");

        Refresh();

    }
}

public static class FoldersCreator
{
    public static void CreateDefault(string root, params string[] folders)
    {

        // Application.dataPath - path curent progect
        // Path.Combine - combines strings into a path
        //The Directory.Exists() method in C# allows you to check if a directory exists on a given path.
        string fullPath = Path.Combine(Application.dataPath, root);
        
        foreach (string folder in folders) 
        {
            string path = Path.Combine(fullPath, folder);

            if (Directory.Exists(path) == false) 
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}

public static class SctiptCreator
{
    public static void CreateDefault(string root, params string[] names)
    {
        string fullPath = Path.Combine(Application.dataPath, root);

        foreach(string name in names)
        {
            string filePath = Path.Combine(fullPath, name + ".txt");

            if (File.Exists(filePath) == false)
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("using UnityEngine;\n");
                    sw.WriteLine($"public class {name}: MonoBehaviour");
                    sw.WriteLine("{");
                    sw.WriteLine("}");
                }

                string extension = Path.GetExtension(filePath);
                string newFilePath = Path.ChangeExtension(filePath, ".cs");
                File.Move(filePath, newFilePath);
            }
        }
    }
}