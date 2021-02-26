using NHotkey.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Meerkat.Views
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

        private void TodoMessageText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            /*This is a hack and should be cleaned up with another a proper way to do it. 
             * For now it works the way I want to the app to though so I'm leaving it this way*/
            
            var shortCutKeys = new List<Tuple<Key, string>> {
                new Tuple<Key, string>( Key.I, "i" ),
                new Tuple<Key, string>( Key.J, "j" ), 
                new Tuple<Key, string>( Key.K, "k" ),
                new Tuple<Key, string>( Key.D, "d" ) 
            };
            if(!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift))
            {
                foreach (var shortcutKey in shortCutKeys)
                {
                    if (e.Key == shortcutKey.Item1)
                    {
                        TextBox target = (TextBox)sender;
                        target.RaiseEvent(new TextCompositionEventArgs(InputManager.Current.PrimaryKeyboardDevice, new TextComposition(InputManager.Current, target, shortcutKey.Item2))
                        {
                            RoutedEvent = TextCompositionManager.TextInputEvent
                        });
                    }
                }
            }
        }
    }
}
