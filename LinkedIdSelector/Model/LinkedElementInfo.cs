using Autodesk.Revit.DB;

namespace LinkedIdSelector.Model
{
    public class LinkedElementInfo
    {
        public ElementId ElementId { get; set; }
        public string LinkName { get; set; }

        public LinkedElementInfo(ElementId elementId, string linkName)
        {
            ElementId = elementId;
            LinkName = linkName;
        }
    }
}
