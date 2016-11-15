using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;

namespace Orchard.Cw.FeedReader.Models {
    public class RemoteRssFeedElement : Element {
        public override string Category
        {
            get { return "Content"; }
        }

        public override bool HasEditor
        {
            get { return true; }
        }

        public string AlternateShapeName
        {
            get { return this.Retrieve(x => x.AlternateShapeName); }
            set { this.Store(x => x.AlternateShapeName, value); }
        }

        public string RemoteUrl
        {
            get { return this.Retrieve(x => x.RemoteUrl); }
            set { this.Store(x => x.RemoteUrl, value); }
        }

        public int CacheDuration
        {
            get { return this.Retrieve(x => x.CacheDuration); }
            set { this.Store(x => x.CacheDuration, value); }
        }

        public int ItemCount
        {
            get { return this.Retrieve(x => x.ItemCount); }
            set { this.Store(x => x.ItemCount, value); }
        }
    }
}