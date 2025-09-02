using Autodesk.Revit.UI;
using LinkedIdSelector.Stores;

namespace LinkedIdSelector.ExternalEventsModeless
{
    public class RevitExternalEvents
    {
        public ExternalEvent RevitExEvent { get; set; }
        public EventHandlerRevit EventHandlerRevit { get; set; }

        public RevitExternalEvents(ItemStore itemstore)
        {
            EventHandlerRevit = new EventHandlerRevit(itemstore);
            RevitExEvent = ExternalEvent.Create(EventHandlerRevit);
        }


        public void MakeRequest(RevitRequestId requestId)
        {
            EventHandlerRevit.RevitRequest.Make(requestId);
            RevitExEvent.Raise();
            RevitModelessUtils.SetFocusToRevit();
        }
    }
}
