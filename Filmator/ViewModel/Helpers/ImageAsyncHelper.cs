using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Filmator.ViewModel.Helpers {
    class ImageAsyncHelper : DependencyObject {
        public static Uri GetSourceUri(DependencyObject obj) { return (Uri)obj.GetValue(SourceUriProperty); }
        public static void SetSourceUri(DependencyObject obj, Uri value) { obj.SetValue(SourceUriProperty, value); }
        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.RegisterAttached("SourceUri", typeof(Uri), typeof(ImageAsyncHelper), new PropertyMetadata {
            PropertyChangedCallback = 
                (obj, e) => ((Image)obj)
                    .SetBinding(Image.SourceProperty, new Binding("VerifiedUri") {
                        Source = new ImageAsyncHelper { _givenUri = (Uri)e.NewValue },
                        IsAsync = true,
                })
        });

        private Uri _givenUri;
        public Uri VerifiedUri {
            get {
                try {
                    Dns.GetHostEntry(_givenUri.DnsSafeHost);
                    return _givenUri;
                } catch (Exception) {
                    return null;
                }
            }
        }
    }
}
