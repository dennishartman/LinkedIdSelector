using Autodesk.Revit.DB;


namespace LinkedIdSelector.Model
{
    public class DataGridItem
    {
        public string Name { get; set; }
        public ElementId ElementId { get; set; }
        public DataGridItem(Element element)
        {
            Name = element.Name;
            ElementId = element.Id;
        }
    }
}

