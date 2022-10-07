using System.Threading.Tasks;
using Segment.Model;

namespace Segment.Request
{
    internal class FakeRequestHandler : IRequestHandler
    {
        private readonly Client _client;

        public FakeRequestHandler(Client client)
        {
            _client = client;
        }

        #pragma warning disable CS1998 // async required for interface
        public async Task MakeRequest(Batch batch)
        #pragma warning restore CS1998
        {
            foreach (var action in batch.batch)
            {
                _client.Statistics.IncrementSucceeded();
                _client.RaiseSuccess(action);
            }
        }
    }
}
