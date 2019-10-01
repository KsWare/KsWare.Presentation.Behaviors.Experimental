using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace KsWare.Presentation.Behaviors.Experimental.TestApp
{
    /// <summary>
    /// Interaction logic for IsFocusedPropertyBehaviorTest.xaml
    /// </summary>
    public partial class IsFocusedPropertyBehaviorTest : UserControl
    {
        public IsFocusedPropertyBehaviorTest()
        {
            InitializeComponent();
        }
    }

    public class IsFocusedPropertyBehaviorTestViewModel : INotifyPropertyChanged {

        private bool _isFocused;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsFocused {
            get { return _isFocused; }
            set {
                if(_isFocused == value) return;
                _isFocused = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
