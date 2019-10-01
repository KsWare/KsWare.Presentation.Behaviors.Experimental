using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace KsWare.Presentation.Behaviors.Experimental {

    public class FocusFirstElementBehavior : Behavior<FrameworkElement> {

        private bool _isLoadEventRegistered;
        private bool _isSizeChangedEventRegistered;

        protected override void OnAttached() {
            base.OnAttached();
            RegisterEvents();
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            UnregisterEvents();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
            if (sender is FrameworkElement fwElement && fwElement.ActualWidth > 0.0 && fwElement.ActualHeight > 0.0) {
                FocusElement();
            }
            else {
                WeakEventManager<FrameworkElement, SizeChangedEventArgs>.AddHandler(AssociatedObject, "SizeChanged",
                    AssociatedObject_InitialSizeChanged);
                _isSizeChangedEventRegistered = true;
            }
        }

        private void AssociatedObject_InitialSizeChanged(object sender, SizeChangedEventArgs e) {
            if (!(sender is FrameworkElement fwElement)) return;
            if (!(fwElement.ActualWidth > 0.0) || !(fwElement.ActualHeight > 0.0)) return;

            WeakEventManager<FrameworkElement, SizeChangedEventArgs>.RemoveHandler(AssociatedObject, "SizeChanged",
                AssociatedObject_InitialSizeChanged);
            _isSizeChangedEventRegistered = false;

            FocusElement();
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e) {

        }

        void FocusElement() {
            AssociatedObject.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            UnregisterEvents();
        }

        private void RegisterEvents() {
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(AssociatedObject, "Loaded",
                AssociatedObject_Loaded);
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(AssociatedObject, "Unloaded",
                AssociatedObject_Unloaded);
            _isLoadEventRegistered = true;
        }

        private void UnregisterEvents() {
            if (_isLoadEventRegistered) {
                WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(AssociatedObject, "Loaded",
                    AssociatedObject_Loaded);
                WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(AssociatedObject, "Unloaded",
                    AssociatedObject_Unloaded);
                _isLoadEventRegistered = false;
            }

            if (_isSizeChangedEventRegistered) {
                WeakEventManager<FrameworkElement, SizeChangedEventArgs>.RemoveHandler(AssociatedObject, "SizeChanged",
                    AssociatedObject_InitialSizeChanged);
            }

        }

    }

}
