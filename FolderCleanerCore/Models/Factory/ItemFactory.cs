using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FolderCleanerCore.Models {
    public class ItemFactory {

        // データを取得する
        public static List<Item> GetItems() {
            if (!File.Exists(GetConfigFilePath())) {
                var list = new List<Item>
                {
                    new Item()
                    {
                        UniqueId = Guid.NewGuid().ToString(),
                        Name = "ダウンロードフォルダ",
                        ScanPath = "C:\\Users\\User\\Downloads",
                        Day = 14,
                        TargetPath = "D:\\dump-files\\Downloads"
                    },
                    new Item()
                    {
                        UniqueId = Guid.NewGuid().ToString(),
                        Name = "デスクトップフォルダ",
                        ScanPath = "C:\\Users\\User\\Desktop",
                        Day = 14,
                        TargetPath = "D:\\dump-files\\Desktop"
                    }
                };

                ItemFactory.SaveItems(list);
            }

            // item.json から Item インスタンスを生成する
            using var reader = File.OpenText(GetConfigFilePath());

            return JsonSerializer.Deserialize<List<Item>>(reader.ReadToEnd(), new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            });
        }

        public static void SaveItems(List<Item> items) {
            if (items == null) {
                return;
            }

            // ファイルへ書き込み
            using var writer = File.Open(GetConfigFilePath(), FileMode.Create);
            using var jsonWriter = new Utf8JsonWriter(writer, new JsonWriterOptions() { Indented = true, });

            // シリアライズ
            JsonSerializer.Serialize(jsonWriter, items);
        }

        public static String GetConfigFilePath() {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + Path.DirectorySeparatorChar + ".foldercleaner";
            Directory.CreateDirectory(directory);

            return directory + Path.DirectorySeparatorChar + "items.json";
        }

    }
}
