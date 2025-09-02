using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LinkedIdSelector.Commands;
using LinkedIdSelector.ExternalEventsModeless;
using LinkedIdSelector.Model;
using LinkedIdSelector.Stores;

namespace LinkedIdSelector.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly RevitExternalEvents _revitExternalEvent;

        public ICommand SelectLinkedElementCommand { get; }
        public ICommand CopyElementIdCommand { get; }

        public ItemStore ItemStore { get; }

        public ObservableCollection<LinkedElementInfo> LinkedElements { get; }

        public MainViewModel(RevitExternalEvents externalEvent, ItemStore itemStore)
        {
            _revitExternalEvent = externalEvent;
            ItemStore = itemStore;
            LinkedElements = ItemStore.LinkedElementInfos;

            SelectLinkedElementCommand = new RelayCommand(_ => _revitExternalEvent.MakeRequest(RevitRequestId.SelectLinkedElement));
            CopyElementIdCommand = new RelayCommand(id => CopyElementId(id));
        }

        private void CopyElementId(object id)
        {
            if (id != null)
            {
                Clipboard.SetText(id.ToString());
            }
        }
    }
}

