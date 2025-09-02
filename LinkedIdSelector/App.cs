using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LinkedIdSelector.ExternalEventsModeless;
using LinkedIdSelector.Model;
using LinkedIdSelector.Stores;
using LinkedIdSelector.View;
using LinkedIdSelector.ViewModel;
using NLog;

namespace LinkedIdSelector
{
    [Transaction(TransactionMode.Manual)]
    public class App : IExternalCommand
    {
        private MainView _mainView { get; set; }
        private Document _doc;
        private ItemStore _itemStore;
        private RevitExternalEvents _revitExternalEvents;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            _itemStore = new ItemStore();
            _revitExternalEvents = new RevitExternalEvents(_itemStore);

        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            NLogger.Setup();
            _logger.Info("------------------------------------------------");
            _logger.Info("LinkedIdSelector is gestart. \n");
            _itemStore.AddLogToInterface("LinkedIdSelector is gestart");

            _doc = commandData.Application.ActiveUIDocument.Document;
            try
            {
                if (_mainView == null)
                {
                    _mainView = new MainView()
                    {
                        DataContext = new MainViewModel(_doc, _revitExternalEvents, _itemStore)
                    };

                    _mainView.Closed += new EventHandler(ClosingMainView);
                    _mainView.Show();
                }
                _mainView.Activate();
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
        // put here anything that needs to happen when the window is closed
        public void ClosingMainView(object sender, EventArgs e) => _logger.Info("LinkedIdSelector is closed \n");
    }
}
