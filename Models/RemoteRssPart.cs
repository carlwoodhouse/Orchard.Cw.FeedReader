using Orchard.ContentManagement;

namespace Orchard.Cw.FeedReader.Models {
    public class RemoteRssPart : ContentPart{
        public string RemoteRssUrl {
            get { return this.Retrieve(x => x.RemoteRssUrl); }
            set { this.Store(x => x.RemoteRssUrl, value); }
        }

        public int ItemsToDisplay {
            get { return this.Retrieve(x => x.ItemsToDisplay); }
            set { this.Store(x => x.ItemsToDisplay, value); }
        }

        public int CacheDuration {
            get { return this.Retrieve(x => x.CacheDuration); }
            set { this.Store(x => x.CacheDuration, value); }
        }
    }
}