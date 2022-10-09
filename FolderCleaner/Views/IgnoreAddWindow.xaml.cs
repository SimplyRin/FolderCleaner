using FolderCleaner.ViewModels;
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

namespace FolderCleaner.Views {
    /// <summary>
    /// IgnoreAddWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class IgnoreAddWindow : Window {
        public IgnoreAddWindow() {
            InitializeComponent();
        }

        private void OnDoneButton_Click(object sender, RoutedEventArgs e) {
            var uniqueId = this.XUniqueId.Content;
            if (uniqueId == null) {
                uniqueId = Guid.NewGuid().ToString();
            }

            var viewModel = ViewModel.GetInstance();
            var items = viewModel.IgnoreItems;

            if (this.XName.Text.Length == 0) {
                var error = new ErrorWindow();
                error.XDescription.Content = "フィールドに値を正しく入力してください。";
                error.Show();
                return;
            }

            var changed = false;

            foreach (var item in items) {
                if (item.UniqueId.Equals(uniqueId)) {
                    changed = true;

                    item.Name = this.XName.Text;
                }
            }

            if (!changed) {
                items.Add(new Item() {
                    UniqueId = (string)uniqueId,
                    Name = this.XName.Text,
                });
            }

            viewModel.IgnoreItems = items;

            this.Close();
        }

        private void OnCancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
