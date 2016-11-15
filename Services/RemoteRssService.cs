using System;
using System.Xml.Linq;
using Orchard.Caching;
using Orchard.Cw.FeedReader.Extensions;
using Orchard.Cw.FeedReader.Models;
using Orchard.Services;

namespace Orchard.Cw.FeedReader.Services {
    public interface IRemoteRssService : IDependency {
        XElement GetFeed(RemoteRssPart remoteRss);
        XElement GetFeed(string remoteUrl, int cacheDuration);
    }

    public class RemoteRssService : IRemoteRssService {
        private readonly IClock _clock;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;
        private const string RemoteRssCacheKey = "FeedReaderRemoteRss_";

        public RemoteRssService(IClock clock, ICacheManager cacheManager, ISignals signals) {
            _clock = clock;
            _cacheManager = cacheManager;
            _signals = signals;
        }

        public XElement GetFeed(RemoteRssPart remoteRss) {
            return GetFeed(remoteRss.RemoteRssUrl, remoteRss.CacheDuration);
        }

        public XElement GetFeed(string remoteUrl, int cacheDuration) {
            if (string.IsNullOrEmpty(remoteUrl)) { return null; }

            return _cacheManager.Get(string.Concat(RemoteRssCacheKey, remoteUrl),
                    s => {
                        s.Monitor(_clock.When(TimeSpan.FromMinutes(cacheDuration)));
                        s.Monitor(_signals.When(string.Concat(s.Key, "_Invalidate")));
                        return XElement.Load(remoteUrl).RemoveAllXmlNamespace();
                    });
        }
    }
}