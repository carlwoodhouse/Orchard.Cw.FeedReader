using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Cw.FeedReader.Models;
using Orchard.Data.Migration;

namespace Orchard.Cw.FeedReader {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            ContentDefinitionManager.AlterPartDefinition(
                typeof(RemoteRssPart).Name, cfg => cfg.Attachable());

            ContentDefinitionManager.AlterTypeDefinition(
                "RemoteRssWidget",
                cfg => cfg
                           .WithPart("RemoteRssPart")
                           .WithPart("CommonPart")
                           .WithPart("WidgetPart")
                           .WithSetting("Stereotype", "Widget")
                );

            return 2;
        }

        public int UpdateFrom1() {
            SchemaBuilder.DropTable("RemoteRssPartRecord");
            return 2;
        }
    }
}