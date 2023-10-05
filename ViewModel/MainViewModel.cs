using ChannelInfoPlatform.Core;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChannelInfoPlatform.ViewModel
{
  
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand StatisticsViewCommand { get; set; }

        public HomeViewModel HomeVm { get; set; }
        public StatisticsViewModel StatVM { get; set; }

        public RelayCommand CloseApplicationCommand { get; set; }
      


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value; 
                OnPropertyChanged(); 
            }
        }

       


        public MainViewModel()
        {
            HomeVm = new();
            StatVM = new();
            CurrentView = HomeVm;
            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVm;
            });

            StatisticsViewCommand = new RelayCommand(o =>
            {
                CurrentView = StatVM;
            });

            CloseApplicationCommand = new(o =>
            {
                Application.Current.Shutdown();
            });

            
        }


        


    }
}
