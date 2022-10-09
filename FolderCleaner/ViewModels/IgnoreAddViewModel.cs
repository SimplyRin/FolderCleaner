using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleaner.ViewModels
{
    public class IgnoreAddViewModel : ObservableObject
    {

        private string tName;

        public string TName { get => tName; set => SetProperty(ref tName, value); }
    }
}
