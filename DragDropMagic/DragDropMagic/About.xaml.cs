using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Media.Animation;

namespace DragDropMagic {
    public sealed partial class About {

        const int ContentAnimationOffset = 100;

        public About() {
            InitializeComponent();
            FlyoutContent.Transitions = new TransitionCollection();
            FlyoutContent.Transitions.Add(new EntranceThemeTransition() {
                FromHorizontalOffset = (SettingsPane.Edge == SettingsEdgeLocation.Right) ? ContentAnimationOffset : (ContentAnimationOffset * -1)
            });
        } 

        private void Back(object sender, RoutedEventArgs e) {
            Popup parent = Parent as Popup;
            if (parent != null) {
                parent.IsOpen = false;
            }

            if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped) {
                SettingsPane.Show();
            }
        }

    
    }
}
