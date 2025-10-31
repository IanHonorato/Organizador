class Program
{
    //61.5 GB
    static void Main(string[] args)
    {
        Console.WriteLine("=== Organizar imagens e vídeos ===");

        Console.Write("Informe o diretório raiz: ");
        string rootPath = Console.ReadLine()!;

        if (!Directory.Exists(rootPath))
        {
            Console.WriteLine("❌ Diretório não encontrado!");
            return;
        }

        Console.Write("Informe o diretório destino para imagens: ");
        string imagesDest = Console.ReadLine()!;
        Directory.CreateDirectory(imagesDest);

        Console.Write("Informe o diretório destino para vídeos: ");
        string videosDest = Console.ReadLine()!;
        Directory.CreateDirectory(videosDest);

        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp", ".heic", ".heif" };
        string[] videoExtensions = { ".mp4", ".mov", ".avi", ".mkv", ".wmv", ".flv", ".webm", ".m4v", ".3gp" };

        var allFiles = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);

        int movedImages = 0;
        int movedVideos = 0;

        foreach (var file in allFiles)
        {
            try
            {
                string ext = Path.GetExtension(file).ToLowerInvariant();
                string fileName = Path.GetFileName(file);

                if (imageExtensions.Contains(ext))
                {
                    string destPath = Path.Combine(imagesDest, fileName);
                    destPath = GetUniquePath(destPath);
                    File.Move(file, destPath);
                    movedImages++;
                }
                else if (videoExtensions.Contains(ext))
                {
                    string destPath = Path.Combine(videosDest, fileName);
                    destPath = GetUniquePath(destPath);
                    File.Move(file, destPath);
                    movedVideos++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Erro ao mover {file}: {ex.Message}");
            }
        }

        Console.WriteLine($"\n✅ Concluído!");
        Console.WriteLine($"   Imagens movidas: {movedImages}");
        Console.WriteLine($"   Vídeos movidos: {movedVideos}");
    }

    // Garante que arquivos com o mesmo nome não sobrescrevam
    static string GetUniquePath(string path)
    {
        string dir = Path.GetDirectoryName(path)!;
        string name = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);
        int count = 1;

        while (File.Exists(path))
        {
            path = Path.Combine(dir, $"{name}_{count}{ext}");
            count++;
        }

        return path;
    }
}
