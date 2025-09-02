using System;
using Autodesk.Revit.UI;
using LinkedIdSelector.Stores;
using NLog;

namespace LinkedIdSelector.ExternalEventsModeless
{
    public class EventHandlerRevit : IExternalEventHandler
    {

        private ItemStore _itemStore;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        // The value of the latest request made by the modeless form 
        private RevitRequest m_request = new RevitRequest();

        /// <summary>
        /// A public property to access the current request value
        /// </summary>
        public RevitRequest RevitRequest
        {
            get { return m_request; }
        }

        private ModelessCommands _modelessCommands = new ModelessCommands();


        public EventHandlerRevit(ItemStore itemStore)
        {
            _itemStore = itemStore;
        }


        public void Execute(UIApplication uiapp)
        {
            try
            {

                RevitRequestId? requestId;
                while ((requestId = RevitRequest.Take()) != null)
                {
                    switch (requestId.Value)
                    {
                        case RevitRequestId.None:
                            {
                                return;
                            }

                        case RevitRequestId.SelectLinkedElement:
                            {
                                _modelessCommands.SelectLinkedElement(uiapp, _itemStore);
                                break;
                            }

                        case RevitRequestId.SelectMultipleLinkedElements:
                            {
                                _modelessCommands.SelectMultipleLinkedElements(uiapp, _itemStore);
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error executing Revit event request");
                TaskDialog.Show("LinkedIdSelector", ex.Message);
            }
        }
        public string GetName() => "Basic External EventHandler";
    }
}

