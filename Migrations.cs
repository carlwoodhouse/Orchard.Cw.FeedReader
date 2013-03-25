using System.Data;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Cw.FeedReader.Models;
using Orchard.Data.Migration;

namespace Orchard.Cw.FeedReader {
    public class Migrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable(
                "RemoteRssPartRecord",
                table => table
                             .ContentPartRecord()
                             .Column("RemoteRssUrl", DbType.String)
                             .Column("CacheDuration", DbType.Int32)
                             .Column("ItemsToDisplay", DbType.Int32)
                );

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

            return 1;
        }
    }
}