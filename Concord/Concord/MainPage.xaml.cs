using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Concord {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();

            CheckInternetConnection(CrossConnectivity.IsSupported);
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            refreshButton.Clicked += OnRefreshButton;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            refreshButton.Clicked -= OnRefreshButton;
        }

        private async void OnRefreshButton(object sender, EventArgs e) {
            try {
                if (CrossConnectivity.Current.IsConnected) {
                    webView.IsVisible = await CrossConnectivity.Current.IsReachable(ApplicationConst.MAIN_HOST);
                    webView.Source = "http://new.concord-shop.pl/pl-pl";
                }
            }
            catch (Exception) {
                Debugger.Break();
            }
        }

        private async void CheckInternetConnection(bool isSupported) {
            try {
                if (isSupported) {
                    if (CrossConnectivity.Current.IsConnected) {
                        webView.IsVisible = await CrossConnectivity.Current.IsReachable(ApplicationConst.MAIN_HOST);
                    } else {
                        webView.IsVisible = false;
                    }
                }
            }
            catch (Exception) {
                Debugger.Break();
            }
        }
    }
}
