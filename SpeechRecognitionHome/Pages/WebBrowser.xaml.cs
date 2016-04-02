using SpeechRPi.Model;
using SpeechRPi.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpeechRPi.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebBrowser : Page
    {
        private readonly SpeechRecognizer recognizer;

        public WebBrowser()
        {
            this.InitializeComponent();

            this.recognizer = new SpeechRecognizer();

            this.AttachToEvents();
        }

        private void AttachToEvents()
        {
            this.recognizer.SpeechRecognized += this.Recognizer_SpeechRecognized;
            this.recognizer.SpeechRecognitionError += this.Recognizer_SpeechRecognitionError;
            this.Loaded += this.WebBrowser_Loaded;
        }

        private bool GoToUrl(string url)
        {
            Uri validUrl;
            bool success = false;
            if (WebHelper.TryGetValidUrl(url, out validUrl))
            {
                this.webView.Navigate(validUrl);
                success = true;
            }

            return success;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.webView.CanGoBack)
            {
                this.webView.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.webView.CanGoForward)
            {
                this.webView.GoForward();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.webView.Refresh();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.webView.Stop();
        }

        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            //this.browser.StartRecognizingSpeech();
        }

        private void TextBoxUrl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                this.GoToUrl(this.textBoxUrl.Text);
            }
        }

        private void SpeakButton_Click(object sender, RoutedEventArgs e)
        {
            this.recognizer.StartRecognizingSpeech();
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            this.GoToUrl(e.RecognizedText);
            this.textBoxUrl.Text = e.RecognizedText;
        }

        private void Recognizer_SpeechRecognitionError(object sender, EventArgs e)
        {
            this.recognizer.StartRecognizingSpeech();
        }

        private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            this.recognizer.StartRecognizingSpeech();
        }
    }
}
