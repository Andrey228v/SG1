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

        FoldersCreator.CreateDefault(defaultFolderPath, "Animations", "Materials", "Prefabs", "Scripts", "ScriptableObjects", "Plugins");
        FoldersCreator.CreateDefault(defaultFolderPath + "Animations", "Animations Clips", "Animator Cotrollers");
        FoldersCreator.CreateDefault(defaultFolderPath + "Materials", "Materials", "Models", "Textures");
        FoldersCreator.CreateDefault(defaultFolderPath + "Scripts", "Input");

        SctiptCreator.CreateDefault(defaultFolderPath + "Scripts", "Test", "Test2");

        Refresh();

    }

    [MenuItem("Tools/Setup/Import My Favorite Assets")]
    public static void ImportAssets()
    {
        AssetsCreator.ImportAsset("DOTween HOTween v2.unitypackage", "Asset Store-5.x\\Demigiant");
        AssetsCreator.ImportAsset("Selection History.unitypackage", "Asset Store-5.x\\Staggart Creations");
        AssetsCreator.ImportAsset("Replace Selected.unitypackage", "Asset Store-5.x\\Staggart Creations");
        AssetsCreator.ImportAsset("Odin Inspector and Serializer v3.3.1.5 (03 Jul 2024).unitypackage", "Asset Store-5.x\\Odin Inspector and Serializer v3.3.1.5 (03 Jul 2024)");
        AssetsCreator.ImportAsset("Odin Validator v3.3.1.11.unitypackage", "Asset Store-5.x\\Odin Validator v3.3.1.11");
        AssetsCreator.ImportAsset("Editor Console Pro v3.974.unitypackage", "Asset Store-5.x\\Unity Asset - Editor Console Pro v3.974");
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

public static class AssetsCreator
{
    public static void ImportAsset(string asset, string subfolder, string folder = "C:\\Users\\gysev\\AppData\\Roaming\\Unity")
    {
        string assetFolderName = Application.dataPath;
        Debug.Log(assetFolderName);
        string assetPathOnPC = Path.Combine(folder, subfolder, asset);

        AssetDatabase.ImportPackage(assetPathOnPC, false);

        //if (AssetDatabase.AssetPathExists(assetPathOnPC))
        //{
        //    AssetDatabase.ImportPackage(assetPathOnPC, false);
        //}
    }
}