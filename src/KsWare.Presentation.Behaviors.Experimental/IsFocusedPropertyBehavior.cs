using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace KsWare.Presentation.Behaviors.Experimental {

    /// <summary>
    /// Provides the bindable <see cref="IsElementFocused"/> property. 
    /// [DRAFT] Do not use in productive code. The name, interface und behavior is subject to change.
    /// </summary>
    public class IsFocusedPropertyBehavior : Behavior<FrameworkElement> {

        public static readonly DependencyProperty IsElementFocusedProperty = DependencyProperty.Register(
            "IsElementFocused", typeof(bool), typeof(IsFocusedPropertyBehavior),
            new FrameworkPropertyMetadata(default(bool), IsElementFocused_Changed) {BindsTwoWayByDefault = true});

        public bool IsElementFocused { get => (bool) GetValue(IsElementFocusedProperty); set => SetValue(IsElementFocusedProperty, value);}

        protected override void OnAttached() {
            base.OnAttached();

            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(AssociatedObject, "Loaded",
                AssociatedObject_Loaded);
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(AssociatedObject, "Unloaded",
                AssociatedObject_Unloaded);
        }

        protected override void OnDetaching() {
            base.OnDetaching();

            WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(AssociatedObject, "Loaded",
                AssociatedObject_Loaded);
            WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(AssociatedObject, "Unloaded",
                AssociatedObject_Unloaded);
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
            if (sender is FrameworkElement fwElement && fwElement.ActualWidth > 0.0 && fwElement.ActualHeight > 0.0) {
                if (IsElementFocused && fwElement.IsKeyboardFocusWithin == false) {
                    FocusElement(fwElement);
                }

                AssociatedObject.IsKeyboardFocusWithinChanged += AssociatedObject_IsKeyboardFocusWithinChanged;
            }
            else {
                WeakEventManager<FrameworkElement, SizeChangedEventArgs>.AddHandler(AssociatedObject, "SizeChanged",
                    AssociatedObject_InitialSizeChanged);
            }
        }

        private void AssociatedObject_InitialSizeChanged(object sender, SizeChangedEventArgs e) {
            if (!(sender is FrameworkElement fwElement)) return;
            if(!(fwElement.ActualWidth > 0.0) || !(fwElement.ActualHeight > 0.0))return;

            WeakEventManager<FrameworkElement, SizeChangedEventArgs>.RemoveHandler(AssociatedObject, "SizeChanged",
                AssociatedObject_InitialSizeChanged);
            AssociatedObject.IsKeyboardFocusWithinChanged += AssociatedObject_IsKeyboardFocusWithinChanged;
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e) {
            AssociatedObject.IsKeyboardFocusWithinChanged -= AssociatedObject_IsKeyboardFocusWithinChanged;
            SetCurrentValue(IsElementFocusedProperty, false);
        }

        private void AssociatedObject_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e) {
            SetCurrentValue(IsElementFocusedProperty, AssociatedObject.IsKeyboardFocusWithin);
        }

        private static void IsElementFocused_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            if (!(bool) e.NewValue) return;

            var senderBehavior = sender as IsFocusedPropertyBehavior;
            var fwElement = senderBehavior?.AssociatedObject;
            if (fwElement == null || fwElement.IsKeyboardFocusWithin != false) return;

            if (fwElement.ActualWidth > 0.0 && fwElement.ActualHeight > 0.0) {
                if (fwElement.IsLoaded) {
                    senderBehavior.FocusElement(fwElement);
                }
            }
            else {
                WeakEventManager<FrameworkElement, SizeChangedEventArgs>.AddHandler(fwElement, "SizeChanged",
                    senderBehavior.AssociatedObject_SizeChangedForFocus);
            }
        }

        private void AssociatedObject_SizeChangedForFocus(object sender, SizeChangedEventArgs e) {
            if (sender is FrameworkElement fwElement && fwElement.ActualWidth > 0.0 && fwElement.ActualHeight > 0.0) {
                WeakEventManager<FrameworkElement, SizeChangedEventArgs>.RemoveHandler(fwElement, "SizeChanged",
                    AssociatedObject_SizeChangedForFocus);
                FocusElement(fwElement);
            }
        }

        private void FocusElement(FrameworkElement elementToFocus) {
            elementToFocus.Focus();
            Keyboard.Focus(elementToFocus);
        }

    }

}
