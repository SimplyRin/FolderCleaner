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

namespace FolderCleaner {
    /// <summary>
    /// IgnoreListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class IgnoreListWindow : Window {
        private static IgnoreListWindow _IgnoreListWindow { get; set; }

        public static IgnoreListWindow GetInstance() {
            return _IgnoreListWindow;
        }

        public IgnoreListWindow() {
            InitializeComponent();

            _IgnoreListWindow = this;
        }

        private void ItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            this.ignoreItemListBox.ScrollIntoView(this.ignoreItemListBox.SelectedItem);
        }

        // 閉じるボタンが押された時
        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        public void UpdateIgnoreListBox() {
            this.ignoreItemListBox.Items.Refresh();
        }
    }
}
