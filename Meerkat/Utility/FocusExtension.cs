using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Meerkat.Utility
{
    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                //This is an ugly solution for gett keyboard focus, but it works for now.
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Input,
                    new Action(delegate ()
                    {
                        uie.Focus();         // Set Logical Focus
                        Keyboard.Focus(uie); // Set Keyboard Focus
                    }));
            }
        }
    }
}
