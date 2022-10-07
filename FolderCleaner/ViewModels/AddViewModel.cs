using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleaner
{
    public class AddViewModel : ObservableObject
    {
        private string tName;

        public string TName { get => tName; set => SetProperty(ref tName, value); }

        private string tScanPath;

        public string TScanPath { get => tScanPath; set => SetProperty(ref tScanPath, value); }

        private string tDay;

        public string TDay { get => tDay; set => SetProperty(ref tDay, value); }

        private string tTargetPath;

        public string TTargetPath { get => tTargetPath; set => SetProperty(ref tTargetPath, value); }

    }
}
