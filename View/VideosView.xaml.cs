using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using ChannelInfoPlatform.Core;

namespace ChannelInfoPlatform.View
{
    /// <summary>
    /// Interaction logic for VideosView.xaml
    /// </summary>
    public partial class VideosView : UserControl
    {
        List<string> videos = new();
        List<BitmapImage> Thumbnails = new();
        List<string> videoUrls = new();


        public VideosView()
        {
            InitializeComponent();
            GetYoutubeVideo();
        }

        async void GetYoutubeVideo()
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


            var channelId = "UCnJ4ljO4dJBvOV-of05ZrUw";
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = channelId;
            searchListRequest.MaxResults = 25;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video" && !searchResult.Snippet.Title.Contains("#"))
                {
                    videos.Add(String.Format("{0}", searchResult.Snippet.Title));
                    videoUrls.Add($"https://youtu.be/{searchResult.Id.VideoId}");
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri($"https://img.youtube.com/vi/{searchResult.Id.VideoId}/0.jpg");
                    bitmapImage.EndInit();
                    Thumbnails.Add(bitmapImage);
                }
            }

            if (videos is null) return;

            
            for(int i = 0; i < videos.Count; i++)
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(10),
                };

                Button btn = new Button()
                {
                    Background = Brushes.Transparent,
                    Name = $"b{i}",
                    BorderBrush = Brushes.Transparent,
                };

                btn.Click += new RoutedEventHandler(OpenVideo);

                TextBlock textBlock = new()
                {
                    Text = videos[i],
                    TextAlignment = TextAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 16,
                    Width = 140,
                    Foreground = Brushes.AliceBlue,
                };

                Image img = new()
                {
                    Width = 150,
                    Source = Thumbnails[i],
                };

                btn.Content = stackPanel;
                stackPanel.Children.Add(img);
                stackPanel.Children.Add(textBlock);
                VideosPanel.Children.Add(btn);



            }
        }

        public void OpenVideo(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var number = Int32.Parse(btn.Name[1].ToString());

            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = videoUrls[number],
                UseShellExecute = true
            });
        }
    }
}
