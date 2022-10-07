using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleanerCore.Models
{
    public class Item
    {
        // UUID
        public String UniqueId
        {
            get; set;
        }

        // 名前
        public String Name
        {
            get; set;
        }

        // 検索フォルダのパス
        public String ScanPath
        {
            get; set;
        }

        // 最後のアクセスから何日経ったタイミングでファイルを移動するか日単位で指定
        public int Day
        {
            get; set;
        }

        // ファイルの移動先フォルダ
        public String TargetPath
        {
            get; set;
        }

    }
}
