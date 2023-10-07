using ChannelInfoPlatform.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ChannelInfoPlatform.ViewModel
{
    class StatisticsViewModel : ObservableObject
    {
        public RelayCommand GetYoutubeStatsCommand { get; set; }

        private BitmapImage _profilePicture;
        public BitmapImage ProfilePicture
        {
            get => _profilePicture;
            set
            {
                _profilePicture = value;
                OnPropertyChanged("ProfilePicture");
            }
        }

        private string _subscriptions;
        public string Subscriptions
        {
            get => _subscriptions;
            set
            {
                _subscriptions = value;
                OnPropertyChanged("Subscriptions");
            }
        }

        private string _viewsCount;
        public string ViewsCount
        {
            get => _viewsCount;
            set
            {
                _viewsCount = value;
                OnPropertyChanged("ViewsCount");
            }
        }

        private bool _isLoaded;

        public bool IsLoaded
        {
            get => _isLoaded;
            set
            {
                _isLoaded = value;
                OnPropertyChanged("IsLoaded");
            }
        }

        public StatisticsViewModel()
        {
            IsLoaded = true;

            GetYoutubeStatsCommand = new(o =>
            {
                GetChannelSubs();
            });
        }


        async void GetChannelSubs()
        {

            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for full read/write access to the
                    // authenticated user's account.
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())
                );

            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            IsLoaded = false;

            var channelId = "UCnJ4ljO4dJBvOV-of05ZrUw";
            var channelList = youtubeService.Channels.List("statistics");
            var channelListSnippet = youtubeService.Channels.List("snippet");

            channelList.Id = channelId;
            channelListSnippet.Id = channelId;

            var channelRequest = await channelList.ExecuteAsync();
            var channelRequestSnippet = await channelListSnippet.ExecuteAsync();

            foreach (var channel in channelRequest.Items)
            {
                var subscriptionCount = channel.Statistics.SubscriberCount;
                var viewsCount = channel.Statistics.ViewCount;
                Subscriptions = $"Subscribers: {subscriptionCount}";
                ViewsCount = $"Total views: {viewsCount}";
            }

            foreach (var channel in channelRequestSnippet.Items)
            {
                ProfilePicture = new BitmapImage(new Uri(channel.Snippet.Thumbnails.Default__.Url));
            }

        }
    }
}
