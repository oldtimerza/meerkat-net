using System;
using System.Windows;
using System.Windows.Controls;
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

        public void TextBoxGotKeyboardFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if(textBox != null)
            {
                textBox.Clear();
            }
        }
    }
}
