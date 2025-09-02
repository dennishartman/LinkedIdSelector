using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autodesk.Revit.DB;
using LinkedIdSelector.Model;

namespace LinkedIdSelector.Stores
{
    public class ItemStore
    {
        private string _logMessageForInterfaceItemStore;

        public string LogMessageForInterfaceItemStore
        {
            get => _logMessageForInterfaceItemStore;
            set
            {
                if (_logMessageForInterfaceItemStore != value)
                {
                    _logMessageForInterfaceItemStore = value;
                    LogMessageChanged?.Invoke(value);
                }
            }
        }

        public List<ElementId> ElementIdsInCurrentView { get; } = new List<ElementId>();
        public ObservableCollection<LinkedElementInfo> LinkedElementInfos { get; } = new ObservableCollection<LinkedElementInfo>();
        public ItemStore() { }



        public void AddLogToInterface(string logMessage) => LogMessageForInterfaceItemStore = LogMessageForInterfaceItemStore + logMessage + "\n \n";

        public event Action<string> LogMessageChanged;






    }
}
