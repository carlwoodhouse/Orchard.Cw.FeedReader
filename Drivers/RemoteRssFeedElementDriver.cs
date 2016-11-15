using Orchard.Cw.FeedReader.Models;
using Orchard.Cw.FeedReader.Services;
using Orchard.Cw.FeedReader.ViewModels;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;

namespace Orchard.Cw.FeedReader.Drivers {
    public class RemoteRssFeedElementDriver : ElementDriver<RemoteRssFeedElement> {
        private readonly IRemoteRssService _remoteRssService;

        public RemoteRssFeedElementDriver(IRemoteRssService remoteRssService) {
            _remoteRssService = remoteRssService;
        }

        protected override EditorResult OnBuildEditor(RemoteRssFeedElement element, ElementEditorContext context) {
            var viewModel = new RemoteRssFeedViewModel {
                RemoteUrl = element.RemoteUrl,
                CacheDuration = element.CacheDuration,
                ItemCount = element.ItemCount,
                AlternateShapeName = element.AlternateShapeName
            };

            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.RemoteRssFeedElement", Model: viewModel);

            if (context.Updater != null) {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.RemoteUrl = viewModel.RemoteUrl;
                element.CacheDuration = viewModel.CacheDuration;
                element.AlternateShapeName = viewModel.AlternateShapeName;
                element.ItemCount = viewModel.ItemCount;
            }

            return Editor(context, editor);
        }

        protected override void OnDisplaying(RemoteRssFeedElement element, ElementDisplayingContext context) {
            context.ElementShape.Feed = _remoteRssService.GetFeed(element.RemoteUrl, element.CacheDuration);
        }
    }
}