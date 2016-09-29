using Orchard.ContentManagement.Handlers;
using Orchard.Cw.FeedReader.Models;
using Orchard.Data;

namespace Orchard.Cw.FeedReader.Handlers {
    public class RemoteRssPartHandler : ContentHandler {
        public RemoteRssPartHandler(IRepository<RemoteRssPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}