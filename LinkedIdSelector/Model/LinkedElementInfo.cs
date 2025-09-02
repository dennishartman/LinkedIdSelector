using Autodesk.Revit.DB;

namespace LinkedIdSelector.Model
{
    public class LinkedElementInfo
    {
        public int ElementId { get; set; }
        public string LinkName { get; set; }

        public LinkedElementInfo(int elementId, string linkName)
        {
            ElementId = elementId;
            LinkName = linkName;
        }
    }
}
