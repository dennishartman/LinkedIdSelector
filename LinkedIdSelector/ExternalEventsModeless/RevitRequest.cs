using System.Collections.Concurrent;

namespace LinkedIdSelector.ExternalEventsModeless
{
    public class RevitRequest
    {
        private ConcurrentQueue<RevitRequestId> m_requests = new ConcurrentQueue<RevitRequestId>();

        public RevitRequestId? Take()
        {
            if (m_requests.TryDequeue(out RevitRequestId requestId))
            {
                return requestId;
            }
            else
            {
                return null;
            }
        }

        public void Make(RevitRequestId request) => m_requests.Enqueue(request);
    }
    public enum RevitRequestId
    {
        None = 0,
        SampleRequest = 1,

    }
}