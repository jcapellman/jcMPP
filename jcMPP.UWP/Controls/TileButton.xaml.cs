using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace jcMPP.UWP.Controls {
    public sealed partial class TileButton : UserControl {
        public TileButton() {
            this.InitializeComponent();
        }

        public string Caption {
            get { return (string)GetValue(CaptionProperty); }

            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(TileButton), null);

        public Uri Icon {
            get { return (Uri)GetValue(IconProperty); }

            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Uri), typeof(TileButton), null);
    }
}