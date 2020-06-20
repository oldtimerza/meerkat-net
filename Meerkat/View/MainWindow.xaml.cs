using System.Windows;
using System.Windows.Input;

namespace Meerkat.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand InsertModeShortcutCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
