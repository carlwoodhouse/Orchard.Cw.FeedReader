using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Orchard.ContentManagement;
using Orchard.Cw.FeedReader.Models;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Layouts.Framework.Elements;
using Orchard.Widgets.Models;

namespace Orchard.Cw.FeedReader {
    public class FeedReaderAlternates : IShapeTableProvider {
        private readonly IWorkContextAccessor workContextAccessor;

        public FeedReaderAlternates(IWorkContextAccessor workContextAccessor) {
            this.workContextAccessor = workContextAccessor;
        }

        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Element").OnDisplaying(context => {
                if (context.Shape.Element.Type == "Orchard.Cw.FeedReader.Models.RemoteRssFeedElement") {
                    if (!string.IsNullOrEmpty((string)context.Shape.Element.AlternateShapeName)) {
                        context.ShapeMetadata.Alternates.Add(string.Concat("Elements_RemoteRssFeedElement_",
                            (string)context.Shape.Element.AlternateShapeName));
                    }
                }
            });

            builder.Describe("Parts_RemoteRss")
                .OnDisplaying(displaying => {
                    ContentItem contentItem = displaying.Shape.ContentItem;
                    if (contentItem != null) {
                        if (contentItem.Is<RemoteRssPart>()) {
                            if (!string.IsNullOrEmpty(displaying.ShapeMetadata.DisplayType)) {

                                var feedPart = contentItem.As<RemoteRssPart>();

                                if (feedPart != null) {
                                    var title = feedPart.Has<WidgetPart>()
                                                    ? feedPart.As<WidgetPart>().Name
                                                    : feedPart.RemoteRssUrl;

                                    var prefix = string.Concat(
                                        "Parts_RemoteRss_",
                                        SanitiseAlternateTitle(title));

                                    var displayType = displaying.ShapeMetadata.DisplayType;
                                    displayType = !string.IsNullOrEmpty(displayType)
                                                      ? string.Concat("_", displayType)
                                                      : string.Empty;

                                    var alternates = new List<string>
                                        {
                                               prefix,
                                               string.Concat(prefix, displayType)
                                           };

                                    foreach (var alternate in alternates.Distinct().Where(alternate => !displaying.ShapeMetadata.Alternates.Contains(alternate))) {
                                        displaying.ShapeMetadata.Alternates.Add(alternate);
                                    }
                                }
                            }
                        }
                    }
                });
        }

        static Regex rgx = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled);
        private static string SanitiseAlternateTitle(string title) {
            title = title.ToLower().Replace("https", string.Empty).Replace("http", string.Empty).Trim();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rgx.Replace(title, string.Empty));
        }
    }
}