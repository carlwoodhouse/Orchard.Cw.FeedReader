using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace Orchard.Cw.FeedReader.Models {
    // - this will go soon
    public class RemoteRssPartRecord : ContentPartRecord {
        public RemoteRssPartRecord() {
            ItemsToDisplay = 5;
            CacheDuration = 20;
        }

        public virtual string RemoteRssUrl { get; set; }
        public virtual int ItemsToDisplay { get; set; }
        public virtual int CacheDuration { get; set; }
    }

    public class RemoteRssPart : ContentPart<RemoteRssPartRecord> {
        public string RemoteRssUrl {
            get { return this.Retrive(x => x.RemoteRssUrl); }
            set { this.Store(x => x.RemoteRssUrl, value); }
        }

        public int ItemsToDisplay {
            get { return this.Retrive(x => x.ItemsToDisplay); }
            set { this.Store(x => x.ItemsToDisplay, value); }
        }

        public int CacheDuration {
            get { return this.Retrive(x => x.CacheDuration); }
            set { this.Store(x => x.CacheDuration, value); }
        }
    }
}