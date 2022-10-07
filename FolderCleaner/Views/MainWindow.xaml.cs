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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FolderCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _MainWindow { get; set; }

        public static MainWindow GetInstance()
        {
            return _MainWindow;
        }

        public MainWindow()
        {
            InitializeComponent();

            _MainWindow = this;
        }

        // アイテム一覧の ListBox の選択が変化したときに呼ばれる
        private void ItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemListBox.ScrollIntoView(itemListBox.SelectedItem);
        }

        public void UpdateListBox()
        {
            itemListBox.Items.Refresh();
        }

        public void UpdateInfoBox()
        {
            infoListBox.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GetInstance().ShowIgnoreList();
        }
    }
}
