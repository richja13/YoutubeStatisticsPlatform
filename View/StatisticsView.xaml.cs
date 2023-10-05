using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeExplode;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics.Eventing.Reader;

namespace ChannelInfoPlatform.View
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : UserControl
    {


        public StatisticsView()
        {
            InitializeComponent();
            GetYoutubeVideo();
           
        }

        async void GetYoutubeVideo()
        {
            var youtube = new YoutubeClient();

            // You can specify either the video URL or its ID
            var videoUrl = "https://www.youtube.com/watch?v=SDCx_tbE_0U&ab_channel=PixelHistory";
           // var video = await youtube.Videos.GetAsync(videoUrl);



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


            var channelId = "UCnJ4ljO4dJBvOV-of05ZrUw";
           // var pxhChannel = ;
           // pxhChannel.Id = channelId;
           // TestBox.Text = pxhChannel.ToString();


            var searchListRequest = youtubeService.Search.List("snippet");
            //searchListRequest.Q = "Pixel History";
            searchListRequest.ChannelId = channelId;
            searchListRequest.MaxResults = 5;
            
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new();
            List<BitmapImage> Thumbnails = new();

            foreach(var searchResult in searchListResponse.Items)
            {
                if(searchResult.Id.Kind == "youtube#videos") 
                    videos.Add(String.Format("{0}", searchResult.Snippet.Title, searchResult.Id.VideoId));
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri($"https://img.youtube.com/vi/{searchResult.Id.VideoId}/0.jpg");
                bitmapImage.EndInit();
                Thumbnails.Add(bitmapImage);
            }

            if (videos is null) return;

            foreach (var v in videos)
            {
               TestBox.Text += v + " ";
            }

            foreach(var t in Thumbnails)
            {
                Image img = new()
                {
                    Width = 50,
                };

                img.Source = t;
                mediagird.Children.Add(img);
            }
        }
    }
}
