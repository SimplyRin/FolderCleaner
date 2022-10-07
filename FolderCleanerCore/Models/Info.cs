using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleanerCore.Models
{
    public class Info
    {

        // 名前（プロパティ）
        public String Name { get; set; }

        public Item _Item { get; set; }

        public Item GetItems()
        {
            return _Item;
        }

    }
}
