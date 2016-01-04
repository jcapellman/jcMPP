using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace jcMPP.UWP.Controls {
    public sealed partial class TileButton : UserControl {
        public TileButton() {
            this.InitializeComponent();
        }

        private string _caption;

        protected string Caption
        {
            get { return _caption; }

            set { _caption = value; }
        }

        private Uri _icon;

        protected Uri Icon
        {
            get { return _icon; }

            set { _icon = value; }
        }
    }
}
