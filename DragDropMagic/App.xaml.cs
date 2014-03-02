using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace DragDropMagic
{
    sealed partial class App : Application
    {
        private Rect _windowBounds;
        private double aboutWidth = 346;
        private Popup settingsScreenPopup;

        private SettingsCommand aboutCommand;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            RequestedTheme = ApplicationTheme.Light;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    
                }
                
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                if (!rootFrame.Navigate(typeof(Main), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            Window.Current.Activate();
        }


        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }


        void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            _windowBounds = Window.Current.Bounds;
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            base.OnWindowCreated(args);

            _windowBounds = Window.Current.Bounds;
            Window.Current.SizeChanged += OnWindowSizeChanged;
            SettingsPane.GetForCurrentView().CommandsRequested += onCommandsRequested;
        }

        void onCommandsRequested(SettingsPane settingsPane, SettingsPaneCommandsRequestedEventArgs eventArgs)
        {
            UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);

            aboutCommand = new SettingsCommand("AboutId", "About / More Info", handler);
            eventArgs.Request.ApplicationCommands.Add(aboutCommand);
        }

        void onSettingsCommand(IUICommand command)
        {

            settingsScreenPopup = new Popup();
            settingsScreenPopup.Closed += OnScreenPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            settingsScreenPopup.IsLightDismissEnabled = true;
            settingsScreenPopup.Width = aboutWidth;
            settingsScreenPopup.Height = _windowBounds.Height;

            settingsScreenPopup.ChildTransitions = new TransitionCollection();
            settingsScreenPopup.ChildTransitions.Add(new PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
                       EdgeTransitionLocation.Right :
                       EdgeTransitionLocation.Left
            });

            if (command == aboutCommand)
            {
                About aboutPane = new About();

                aboutPane.Width = aboutWidth;
                aboutPane.Height = _windowBounds.Height;
                settingsScreenPopup.Child = aboutPane;
                settingsScreenPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (_windowBounds.Width - aboutWidth) : 0);
            }

            settingsScreenPopup.SetValue(Canvas.TopProperty, 0);
            settingsScreenPopup.IsOpen = true;
        }

        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                settingsScreenPopup.IsOpen = false;
            }
        }

        void OnScreenPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }
    }
}
