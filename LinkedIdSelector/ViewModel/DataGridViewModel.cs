using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LinkedIdSelector.Model;
using LinkedIdSelector.Stores;

namespace LinkedIdSelector.ViewModel
{
    public class DataGridViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        private ItemStore _itemStore;

        public ICollectionView FilteredItems { get; set; }

        private bool _filterOne = false;
        public bool FilterOne
        {
            get => _filterOne;
            set { _filterOne = value; OnPropertyChanged(nameof(FilterOne)); FilteredItems.Refresh(); }
        }

        private bool _filterTwo = false;
        public bool FilterTwo
        {
            get => _filterTwo;
            set { _filterTwo = value; OnPropertyChanged(nameof(FilterTwo)); FilteredItems.Refresh(); }
        }
        private bool _filterByView = false;
        public bool FilterByView
        {
            get => _filterByView;
            set
            {
                if (_filterByView == value) return;
                _filterByView = value;
                OnPropertyChanged(nameof(FilterByView));

                // Example filter
                //if (value == true) _mainViewModel.GetElementsInCurrentView();
                FilteredItems.Refresh();
            }
        }

        private DataGridItem _selectedRow;
        public DataGridItem SelectedRow
        {
            get => _selectedRow;

            set
            {
                if (_selectedRow == value) return;
                _selectedRow = value;
                if (value != null)
                {
                    // Example to hightlight element in revit model when row is selected
                    //_mainViewModel.HighlightElement(value);
                }
                OnPropertyChanged(nameof(SelectedRow));
            }
        }

        private ObservableCollection<DataGridItem> _dataGridItems;
        public ObservableCollection<DataGridItem> DataGridItems
        {
            get => _dataGridItems;
            set
            {
                _dataGridItems = value;
                OnPropertyChanged(nameof(DataGridItems));
            }
        }


        public DataGridViewModel(MainViewModel mvm)
        {
            _mainViewModel = mvm;
            _itemStore = mvm.ItemStore;
            DataGridItems = new ObservableCollection<DataGridItem>();

            FilteredItems = CollectionViewSource.GetDefaultView(DataGridItems);
            FilteredItems.Filter = FilterLogic;

        }




        private bool FilterLogic(object item)
        {
            if (item is DataGridItem myItem)
            {
                if (FilterByView)
                {
                    bool filterByView = _itemStore.ElementIdsInCurrentView.Contains(myItem.ElementId);
                    if (filterByView == false) { return false; }
                }

                if (FilterTwo == false && FilterOne == false) return true;


                // Filtering conditions based on checkboxes
                // Example Filter
                //bool filterOne = FilterOne == true && myItem.InterfaceStatus == Enums.interfaceStatus.ZeroChildren;
                //bool filterTwo = FilterTwo == true && myItem.InterfaceStatus == Enums.interfaceStatus.Error;

                bool filterOne = true;
                bool filterTwo = true;

                return filterOne || filterTwo;
            }

            return false;
        }
    }
}
