using FolderCleaner.ViewModels;
using FolderCleanerCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// AddWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void OnDoneButton_Click(object sender, RoutedEventArgs e)
        {
            var uniqueId = this.XUniqueId.Content;
            if (uniqueId == null)
            {
                uniqueId = Guid.NewGuid().ToString();
            }

            var viewModel = ViewModel.GetInstance();
            var items = viewModel.Items;

            if (this.XName.Text.Length == 0 || this.XScanPath.Text.Length == 0 || this.XDay.Text.Length == 0 || this.XTargetPath.Text.Length == 0)
            {
                var error = new ErrorWindow();
                error.XDescription.Content = "フィールドに値を正しく入力してください。";
                error.Show();
                return;
            }

            if (!Directory.Exists(this.XScanPath.Text))
            {
                var error = new ErrorWindow();
                error.XDescription.Content = "検索フォルダパスへのフォルダが見つかりませんでした。";
                error.Show();
                return;
            }

            var changed = false;

            foreach (var item in items)
            {
                if (item.UniqueId.Equals(uniqueId))
                {
                    changed = true;

                    item.Name = this.XName.Text;
                    item.ScanPath = this.XScanPath.Text;
                    item.Day = Int32.Parse(this.XDay.Text);
                    item.TargetPath = this.XTargetPath.Text;
                }
            }

            if (!changed)
            {
                items.Add(new Item()
                {
                    UniqueId = (string) uniqueId,
                    Name = this.XName.Text,
                    ScanPath = this.XScanPath.Text,
                    Day = Int32.Parse(this.XDay.Text),
                    TargetPath = this.XTargetPath.Text
                });
            }

            viewModel.Items = items;

            this.Close();
        }

        private void OnCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void XDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

    }
}
