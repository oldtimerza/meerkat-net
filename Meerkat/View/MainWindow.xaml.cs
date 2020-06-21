using NHotkey.Wpf;
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
        public MainWindow()
        {
            HotkeyManager.Current.AddOrReplace("ShowHideWindow", Key.Space, ModifierKeys.Alt, ShowHide);
            InitializeComponent();
        }

        public void ShowHide(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                return;
            }
            WindowState = WindowState.Minimized;
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
