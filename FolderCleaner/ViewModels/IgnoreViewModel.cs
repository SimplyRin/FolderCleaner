using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FolderCleaner.Views;
using FolderCleanerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FolderCleaner.ViewModels {
    public class IgnoreViewModel : ObservableObject {
        private static IgnoreViewModel _IgnoreViewModel;

        public static IgnoreViewModel GetInstance() {
            return _IgnoreViewModel;
        }

        public IgnoreViewModel() {
            _IgnoreViewModel = this;
        }

        // データリスト（プロパティ）
        public List<Item> IgnoreItems {
            get {
                return ViewModel.GetInstance().ignoreItems;
            }
            set {
                SetProperty(ref ViewModel.GetInstance().ignoreItems, value);
                IgnoreListWindow.GetInstance().UpdateIgnoreListBox();

                IgnoreItemFactory.SaveItems(ViewModel.GetInstance().ignoreItems);
            }
        }

        // ユーザーが選択したアイテム
        private Item _selectedItem;
        public Item SelectedItem {
            get {
                return this._selectedItem;
            }
            set {
                this._selectedItem = value;
            }
        }

        private RelayCommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(Add);

        private void Add() {
            var add = new IgnoreAddWindow();
            add.XUniqueId.Content = Guid.NewGuid().ToString();
            add.Show();
        }

        private RelayCommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ??= new RelayCommand(Delete);

        private void Delete() {
            if (this.SelectedItem == null) {
                return;
            }

            var delete = new IgnoreDeleteWindow();

            delete.XUniqueId.Content = this.SelectedItem.UniqueId;

            delete.XName.Content = this.SelectedItem.Name;

            delete.Show();
        }
    }
}
