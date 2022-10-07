using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleaner
{
    public class DeleteViewModel : ObservableObject
    {

        private object tName;

        public object TName { get => tName; set => SetProperty(ref tName, value); }



    }
}
