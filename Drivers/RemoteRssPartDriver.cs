using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Cw.FeedReader.Models;
using Orchard.Cw.FeedReader.Services;
using System;

namespace Orchard.Cw.FeedReader.Drivers {
    public class RemoteRssPartDriver : ContentPartDriver<RemoteRssPart> {
        private readonly IRemoteRssService _remoteRss;

        public RemoteRssPartDriver(IRemoteRssService remoteRss) {
            _remoteRss = remoteRss;
        }

        protected override string Prefix {
            get {
                return "remoterss";
            }
        }

        protected override DriverResult Display(RemoteRssPart part, string displayType, dynamic shapeHelper) {
            if (!string.IsNullOrWhiteSpace(part.RemoteRssUrl)) {
                return ContentShape("Parts_RemoteRss", () => shapeHelper.Parts_RemoteRss(
                    RemoteRssUrl: part.RemoteRssUrl,
                    ItemsToDisplay: part.ItemsToDisplay,
                    Feed: _remoteRss.GetFeed(part),
                    ContentItem: part.ContentItem
                    ));
            }
            return new DriverResult();
        }

        protected override DriverResult Editor(
            RemoteRssPart part, dynamic shapeHelper) {

                return ContentShape("Parts_RemoteRss_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/RemoteRss",
                    Model: part,
                    Prefix: Prefix));
        }
   
        protected override DriverResult Editor(
            RemoteRssPart part, IUpdateModel updater, dynamic shapeHelper) {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
		
		protected override void Importing(RemoteRssPart part, ImportContentContext context) {
			part.RemoteRssUrl = context.Attribute(part.PartDefinition.Name, "RemoteRssUrl");

            var count = context.Attribute(part.PartDefinition.Name, "ItemsToDisplay");
            if (count != null) {
                part.ItemsToDisplay = Convert.ToInt32(count);
            }
			
			var cacheDuration = context.Attribute(part.PartDefinition.Name, "CacheDuration");
            if (cacheDuration != null) {
                part.CacheDuration = Convert.ToInt32(cacheDuration);
            }
        }

        protected override void Exporting(RemoteRssPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("RemoteRssUrl", part.RemoteRssUrl);
            context.Element(part.PartDefinition.Name).SetAttributeValue("ItemsToDisplay", part.ItemsToDisplay);
			context.Element(part.PartDefinition.Name).SetAttributeValue("CacheDuration", part.CacheDuration);
        }
    }
}