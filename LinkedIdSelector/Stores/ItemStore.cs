using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

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

        public List<ElementId> ElementIdsInCurrentView { get; set; } = new List<ElementId>();
        public ItemStore() { }



        public void AddLogToInterface(string logMessage) => LogMessageForInterfaceItemStore = LogMessageForInterfaceItemStore + logMessage + "\n \n";

        public event Action<string> LogMessageChanged;






    }
}
