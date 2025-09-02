using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using LinkedIdSelector.Commands;
using LinkedIdSelector.ExternalEventsModeless;
using LinkedIdSelector.Model;
using LinkedIdSelector.Stores;

namespace LinkedIdSelector.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Document _doc { get; set; }
        private RevitExternalEvents _revitExternalEvent;
        public ICommand SelectLinkedElementCommand { get; }
        public ICommand CopyElementIdCommand { get; }
        public ItemStore ItemStore { get; private set; }

        public ObservableCollection<LinkedElementInfo> LinkedElements { get; }

        public MainViewModel(Document doc, RevitExternalEvents externalEvent, ItemStore itemStore)
        {
            _revitExternalEvent = externalEvent;
            ItemStore = itemStore;
            _doc = doc;

            LinkedElements = ItemStore.LinkedElementInfos;
            SelectLinkedElementCommand = new RelayCommand(x => _revitExternalEvent.MakeRequest(RevitRequestId.SelectLinkedElement));
            CopyElementIdCommand = new RelayCommand(param =>
            {
                if (param is ElementId id)
                {
                    Clipboard.SetText(id.IntegerValue.ToString());
                }
            });
        }

        public void GetElementsInCurrentView()
        {
            ItemStore.ElementIdsInCurrentView.Clear();
            if (_doc?.ActiveView == null) return;

            var collector = new FilteredElementCollector(_doc, _doc.ActiveView.Id);
            ItemStore.ElementIdsInCurrentView.AddRange(collector.ToElementIds());
        }
    }
}
