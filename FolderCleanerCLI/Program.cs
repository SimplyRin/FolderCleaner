using FolderCleanerCore.Models;
using System.Text.RegularExpressions;

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

    EachDirectory(item);
}

static void EachDirectory(Item item)
{
    var directory = item.ScanPath;

    if (!Directory.Exists(directory))
    {
        Console.WriteLine(directory + " が見つかりませんでした。");
        return;
    }

    ForDirectory(item, directory);
}

static void ForDirectory(Item item, String directory)
{
    var info = new DirectoryInfo(directory);
    var files = info.GetFiles();

    foreach (var file in files)
    {
        if (File.GetAttributes(file.FullName).HasFlag(FileAttributes.Directory))
        {
            ForDirectory(item, file.FullName);
            return;
        }

        var lastAccess = File.GetLastAccessTime(file.FullName);
        var now = DateTime.Now;

        int days = (int)(now - lastAccess).TotalDays;

        // Console.WriteLine(days + "日: " + file.Directory?.FullName + " _ " + file.Name);

        var _continue = false;
        foreach (var ignore in GetIgnoreFiles())
        {
            // Console.WriteLine(file.Name + ", " + ignore);

            var regexPattern = Regex.Replace(ignore, ".", m =>
            {
                String s = m.Value;
                if (s.Equals("?"))
                {
                    return ".";
                }
                else if (s.Equals("*"))
                {
                    return ".*";
                }
                else
                {
                    //上記以外はエスケープする
                    return Regex.Escape(s);
                }
            });

            if (new Regex(regexPattern).IsMatch(file.Name))
            {
                // Console.WriteLine("[Regex] True: " + file.Name + ", " + ignore);
                _continue = true;
            }
        }

        if (_continue)
        {
            continue;
        }

        // GUI でしたいした日数を超えている場合
        if (days >= item.Day)
        {
            var filename = file.Name;

            // 移動先のフォルダに同じ名前のファイルがある場合 _Guid を付け足して移動
            if (File.Exists(item.TargetPath + Path.DirectorySeparatorChar + filename))
            {
                var basename = Path.GetFileNameWithoutExtension(filename);
                var extension = Path.GetExtension(filename);

                basename += "_" + Guid.NewGuid().ToString().Split("-")[0];

                filename = basename + extension;
            }

            Directory.CreateDirectory(item.TargetPath);

            try
            {
                Console.WriteLine(days + "日: " + file.Directory?.FullName + " _ " + file.Name);
                Console.WriteLine(filename + " を移動します。");
                // file.MoveTo(item.TargetPath + Path.DirectorySeparatorChar + filename);
            }
            catch (Exception e)
            {

            }
        }
    }
}

static List<String> GetIgnoreFiles()
{
    var list = new List<String>();

    foreach (var filename in IgnoreItemFactory.GetInfos())
    {
        list.Add(filename.Name);
    }

    return list;
}
