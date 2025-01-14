using System.IO.Compression;
class Program
{
    static void Main()
    {
        Console.WriteLine("введите путь для поиска файла");
        string searchingPath = Console.ReadLine();
        Console.WriteLine("введите название файла");
        string fileName = Console.ReadLine();
        string[] files = Directory.GetFiles(searchingPath, fileName, SearchOption.AllDirectories);
        if (files.Length > 0)
        {
            Console.WriteLine($"файл найден : {files[0]}");
            using (FileStream fileStream = new FileStream(files[0], FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    string fileContent = sr.ReadToEnd();
                    Console.WriteLine($"содержимое файла : \n\t{fileContent}");
                }
            }
        }
        else
        {
            Console.WriteLine("файл не найден");
        }
        Console.WriteLine("хотите сжать найденный файл? (y/n)");
        string response = Console.ReadLine();
        if (response == "y")
        {
            string compressedPath = $"{files[0]}.zip";
            using (FileStream fileStream_1 = new FileStream(files[0], FileMode.Open))
            {
                using (FileStream fileStream_2 = new FileStream(compressedPath, FileMode.Create))
                {
                    using (GZipStream compress_stream = new GZipStream(fileStream_2, CompressionMode.Compress))
                    {
                        fileStream_1.CopyTo(compress_stream);
                        Console.WriteLine("файл сжат и сохранен в ту же директорию");
                    }
                }
            }
        }
    }
}