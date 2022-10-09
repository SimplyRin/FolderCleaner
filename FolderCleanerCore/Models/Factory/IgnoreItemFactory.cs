using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FolderCleanerCore.Models
{
    public class IgnoreItemFactory
    {

        public static List<Info> GetInfos()
        {
            var infos = GetItems();

            var list = new List<Info>();

            foreach (var info in infos)
            {
                list.Add(new Info()
                {
                    Name = info.Name,
                });
            }

            return list;
        }

        // データを取得する
        public static List<Item> GetItems()
        {
            if (!File.Exists(GetConfigFilePath()))
            {
                var list = new List<Item>
                {
                    new Item()
                    {
                        UniqueId = Guid.NewGuid().ToString(),
                        Name = "*.lnk",
                    },
                    new Item()
                    {
                        UniqueId = Guid.NewGuid().ToString(),
                        Name = "*.exe",
                    },

                    // WMP で生成される不要なファイルは無視
                    new Item()
                    {
                        UniqueId = Guid.NewGuid().ToString(),
                        Name = "folder.jpg",
                    },
                    new Item()
                    {
                        UniqueId= Guid.NewGuid().ToString(),
                        Name = "albumartsmall.jpg",
                    },
                };

                IgnoreItemFactory.SaveItems(list);
            }

            // item.json から Item インスタンスを生成する
            using var reader = File.OpenText(GetConfigFilePath());

            return JsonSerializer.Deserialize<List<Item>>(reader.ReadToEnd(), new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public static void SaveItems(List<Item> items)
        {
            if (items == null)
            {
                return;
            }

            // ファイルへ書き込み
            using var writer = File.Open(GetConfigFilePath(), FileMode.Create);
            using var jsonWriter = new Utf8JsonWriter(writer, new JsonWriterOptions() { Indented = true, });

            // シリアライズ
            JsonSerializer.Serialize(jsonWriter, items);
        }

        public static String GetConfigFilePath()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + Path.DirectorySeparatorChar + ".foldercleaner";
            Directory.CreateDirectory(directory);

            return directory + Path.DirectorySeparatorChar + "ignore-items.json";
        }

    }
}
