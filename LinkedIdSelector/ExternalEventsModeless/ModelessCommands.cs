using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LinkedIdSelector.Stores;

namespace LinkedIdSelector.ExternalEventsModeless
{
    public class ModelessCommands
    {
        public void ModelessSampleCommand(UIApplication uiapp, ItemStore itemstore)
        {
            Document doc = uiapp.ActiveUIDocument.Document;
            Transaction trans = new Transaction(doc);
            trans.Start("StartSanpleCommand");

            itemstore.AddLogToInterface("the button was clicked");
            TaskDialog.Show("Message", "This is a sample message");

            trans.Commit();

        }
    }
}
