using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RecipeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // DispatcherTimer for handling timed events
        public DispatcherTimer dt = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            StartLoadingAnimation(); // Starts the loading animation when the window is initialized

            // Setup MainWindow Timer
            dt.Tick += new EventHandler(dt_Method); // Event handler for the timer tick event
            dt.Interval = new TimeSpan(0, 0, 4); // Timer interval set to 4 seconds
            dt.Start(); // Start the timer
        }

        // Method to start the loading animation
        private void StartLoadingAnimation()
        {
            Storyboard loadingAnimation = (Storyboard)FindResource("LoadingAnimation"); // Retrieves and starts the loading animation defined in XAML resources
            loadingAnimation.Begin();
        }

        // Method called when the timer ticks
        private void dt_Method(object sender, EventArgs e)
        {
            // Navigate to the main menu window
            Menu menu = new Menu(); // Create an instance of the Menu window
            menu.Show(); // Show the Menu window
            dt.Stop(); // Stop the timer
            this.Close(); // Close the current MainWindow
        }
    }
}
