

using Orchard.Cw.FeedReader.Models;

namespace Orchard.Cw.FeedReader
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.DisplayManagement.Descriptors;
    using Orchard.Widgets.Models;

    public class FeedReaderAlternates : IShapeTableProvider
    {
        private readonly IWorkContextAccessor workContextAccessor;

        public FeedReaderAlternates(IWorkContextAccessor workContextAccessor)
        {
            this.workContextAccessor = workContextAccessor;
        }

        /// <summary>Create the alternates!</summary>
        /// <param name="builder">The builder.</param>
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Parts_RemoteRss")
                .OnDisplaying(displaying =>
                   {
                       ContentItem contentItem = displaying.Shape.ContentItem;
                       if (contentItem != null)
                       {
                           if (contentItem.Is<RemoteRssPart>())
                           {
                               if (!string.IsNullOrEmpty(displaying.ShapeMetadata.DisplayType))
                               {

                                   var feedPart = contentItem.As<RemoteRssPart>();

                                   if (feedPart != null)
                                   {
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

                                       // blog name alternate
                                       var alternates = new List<string>
                                           {
                                               prefix,
                                               string.Concat(prefix, displayType)
                                           };

                                       foreach (var alternate in alternates.Distinct().Where(alternate => !displaying.ShapeMetadata.Alternates.Contains(alternate)))
                                       {
                                           displaying.ShapeMetadata.Alternates.Add(alternate);
                                       }
                                   }
                               }
                           }
                       }
                   });
        }

        /// <summary>Sanatise the Alternate Name from the title</summary>
        /// <param name="title">The title.</param>
        /// <returns>a sanatised string</returns>
        private static string SanitiseAlternateTitle(string title)
        {
            title = title.ToLower().Replace("https", string.Empty).Replace("http", string.Empty).Trim();
            var rgx = new Regex("[^a-zA-Z0-9]");
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rgx.Replace(title, string.Empty));
        }
    }
}