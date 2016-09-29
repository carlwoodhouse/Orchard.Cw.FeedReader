using System.Xml.Linq;

namespace Orchard.Cw.FeedReader.Extensions {
    public static class XmlExtensions {
        public static XElement RemoveAllXmlNamespace(this XElement xElement) {
            XNamespace ns = string.Empty;

            foreach (var e in xElement.DescendantsAndSelf()) {
                e.Name = ns.GetName(e.Name.LocalName);
            }

            return xElement;
        }
    }
}
