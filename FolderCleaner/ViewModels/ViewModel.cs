using CommunityToolkit.Mvvm.ComponentModel;
using FolderCleanerCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace FolderCleaner.ViewModels
{
    public class ViewModel : ObservableObject
    {
        private static ViewModel _rootViewModel;

        public static ViewModel GetInstance()
        {
            return _rootViewModel;
        }

        private Boolean loaded = false;

        // 「追加」コマンド
        public ICommand AddCommand { get; }

        // 「編集」コマンド
        public ICommand EditCommand { get; }

        // 「削除」コマンド
        public ICommand DeleteCommand { get; }

        public ViewModel()
        {
            if (this.loaded)
            {
                return;
            }

            _rootViewModel = this;

            AddCommand = new RelayCommand(() => this.Add());
            EditCommand = new RelayCommand(() => this.Edit());
            DeleteCommand = new RelayCommand(() => this.Delete());

            Infos = InfoFactory.GetInfos(this.SelectedItem);

            this.loaded = true;
        }

        // データリスト
        private List<Item> items = ItemFactory.GetItems();

        // データリスト
        public List<Item> ignoreItems = IgnoreItemFactory.GetItems();

        // データリスト（プロパティ）
        public List<Item> Items {
            get {
                return this.items;
            }
            set
            {
                SetProperty(ref this.items, value);
                MainWindow.GetInstance().UpdateListBox();
                MainWindow.GetInstance().UpdateInfoBox();

                ItemFactory.SaveItems(this.items);
            }
        }

        // データリスト（プロパティ）
        public List<Item> IgnoreItems
        {
            get
            {
                return IgnoreViewModel.GetInstance().IgnoreItems;
            }
            set
            {
                IgnoreViewModel.GetInstance().IgnoreItems = value;
            }
        }

        // ユーザーが選択したアイテム
        private Item _selectedItem;
        public Item SelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                this._selectedItem = value;

                // 選択された都道府県の地域リストを取得する
                Infos = InfoFactory.GetInfos(this.SelectedItem);

                // 選択された都道府県を保存する
                // PrefInfoFactory.SavePref(this.SelectedPref);
            }
        }

        public bool IsClickable
        {
            get
            {
                return this.SelectedItem != null;
            }
        }

        // 名前、日、移動先などのデータ情報
        private List<Info> _infos;
        public List<Info> Infos
        {
            get
            {
                return this._infos;
            }
            set
            {
                SetProperty(ref this._infos, value);
            }
        }

        public void ShowIgnoreList()
        {
            // Infos = IgnoreItemFactory.GetInfos();
            var ignore = new IgnoreListWindow();
            ignore.Show();
        }

        public void Add()
        {
            var add = new AddWindow();
            add.XUniqueId.Content = Guid.NewGuid().ToString();
            add.Show();
        }

        public void Edit()
        {
            if (this.SelectedItem == null)
            {
                return;
            }

            var add = new AddWindow();

            add.Title = "編集: " + this.SelectedItem.Name;

            add.XUniqueId.Content = this.SelectedItem.UniqueId;

            add.XName.Text = this.SelectedItem.Name;
            add.XScanPath.Text = this.SelectedItem.ScanPath;
            add.XDay.Text = this.SelectedItem.Day.ToString();
            add.XTargetPath.Text = this.SelectedItem.TargetPath;

            add.Show();
        }

        public void Delete()
        {
            if (this.SelectedItem == null)
            {
                return;
            }

            var delete = new DeleteWindow();

            delete.XUniqueId.Content = this.SelectedItem.UniqueId;

            delete.XName.Content = this.SelectedItem.Name;

            delete.Show();
        }

    }
}
