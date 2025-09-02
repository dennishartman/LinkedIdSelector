using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using LinkedIdSelector.Model;
using LinkedIdSelector.Stores;

namespace LinkedIdSelector.ExternalEventsModeless
{
    public class ModelessCommands
    {
        public void SelectLinkedElement(UIApplication uiapp, ItemStore itemstore)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            try
            {
                Reference reference = uidoc.Selection.PickObject(ObjectType.LinkedElement, "Select element in a linked model");
                if (reference == null) return;

                RevitLinkInstance linkInstance = uidoc.Document.GetElement(reference) as RevitLinkInstance;
                if (linkInstance == null) return;

                ElementId linkedElementId = reference.LinkedElementId;
                string linkName = linkInstance.Document.Title;

                itemstore.LinkedElementInfos.Add(new LinkedElementInfo(linkedElementId, linkName));
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
            }
        }
    }
}
