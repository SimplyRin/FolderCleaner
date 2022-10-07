using FolderCleanerCore.Models;

var items = ItemFactory.GetItems();

foreach (var item in items)
{
    var name = item.Name;
    var scanPath = item.ScanPath;
    var day = item.Day;
    var targetPath = item.TargetPath;

    Console.WriteLine("処理開始:" +
        "\n\tName:" + name +
        "\n\tScanPath: " + scanPath +
        "\n\tDay: " + day + 
        "\n\tTargetPath: " + targetPath);

    EachDirectory(scanPath);
}

static void EachDirectory(string directory)
{
    if (!Directory.Exists(directory))
    {
        Console.WriteLine(directory + " が見つかりませんでした。");
        return;
    }

    var info = new DirectoryInfo(directory);
    var files = info.GetFiles();

    foreach (var file in files)
    {
        var lastAccess = File.GetLastAccessTime(file.FullName);
        var now = DateTime.Now;

        int days = (int) (now - lastAccess).TotalDays;

        Console.WriteLine(days + "日: " + file.FullName);
    }
}
