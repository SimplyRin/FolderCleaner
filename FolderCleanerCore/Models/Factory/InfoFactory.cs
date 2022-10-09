using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleanerCore.Models {
    public class InfoFactory {

        public static List<Info> GetInfos(Item item) {
            if (item == null) {
                return new List<Info>();
            }

            return new List<Info>()
            {
                new Info()
                {
                    Name = item.Name,
                    _Item = item,
                }
            };
        }

    }
}
