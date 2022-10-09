using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCleaner.ViewModels {
    public class ErrorViewModel : ObservableObject {

        private object tDescription;

        public object TDescription { get => tDescription; set => SetProperty(ref tDescription, value); }
    }
}
