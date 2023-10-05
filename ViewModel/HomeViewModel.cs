using ChannelInfoPlatform.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ChannelInfoPlatform.ViewModel
{
    class HomeViewModel : ObservableObject
    {

        public RelayCommand GoToLatestCommand { get; set; }
        public RelayCommand GoToFirstVideoCommand { get; set; }
        public RelayCommand GoToSecondVideoCommand { get; set; }
        public RelayCommand GoToThirdVideoCommand { get; set; }
        public RelayCommand VisitChannelCommand { get; set; }

        public List<string> videoUrls = new List<string>();
        string ChannelUrl = "https://www.youtube.com/@pixelhistory3767";

        public HomeViewModel()
        {
            AddUrls();


            GoToLatestCommand = new(o =>
            {
                OpenVideo(0);
            });

            GoToFirstVideoCommand = new(o =>
            {
                OpenVideo(1);
            });

            GoToSecondVideoCommand = new(o =>
            {
                OpenVideo(2);
            });

            GoToThirdVideoCommand = new(o =>
            {
                OpenVideo(3);
            });

            VisitChannelCommand = new(o =>
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = ChannelUrl,
                    UseShellExecute = true
                });
            });

        }

        public void AddUrls()
        {
            videoUrls.Add("https://www.youtube.com/watch?v=SDCx_tbE_0U&ab_channel=PixelHistory");
            videoUrls.Add("https://youtu.be/sGvk3MS-qK4?si=zBS6niXZ_6CfGyzW");
            videoUrls.Add("https://youtu.be/2ZszrnDzSFU?si=bpkPFhYz5nu9-x5R");
            videoUrls.Add("https://youtu.be/5w3hrIyas38?si=uZbWsh1DAtjpQvXR");
        }

        public void OpenVideo(int videoNumber)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = videoUrls[videoNumber],
                UseShellExecute = true
            });
        }
    }
}
