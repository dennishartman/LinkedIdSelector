using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autodesk.Revit.DB;
using LinkedIdSelector.Model;

namespace LinkedIdSelector.Stores
{
    public class ItemStore
    {
        public List<ElementId> ElementIdsInCurrentView { get; } = new List<ElementId>();
        public ObservableCollection<LinkedElementInfo> LinkedElementInfos { get; } = new ObservableCollection<LinkedElementInfo>();
        public ItemStore() { }
    }
}
