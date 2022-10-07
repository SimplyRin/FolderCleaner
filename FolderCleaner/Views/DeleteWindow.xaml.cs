using FolderCleanerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FolderCleaner
{
    /// <summary>
    /// DeleteWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public DeleteWindow()
        {
            InitializeComponent();
        }

        private void OnDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var uniqueId = this.XUniqueId.Content;
            if (uniqueId == null)
            {
                uniqueId = Guid.NewGuid().ToString();
            }

            var viewModel = ViewModel.GetInstance();
            var items = viewModel.Items;

            var newList = new List<Item>();

            foreach (var item in items)
            {
                if (!item.UniqueId.Equals(uniqueId))
                {
                    newList.Add(item);
                }
            }

            viewModel.Items = newList;

            this.Close();
        }

        private void OnCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
