using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelloWpf
{
    /// <summary>
    /// Interaction logic for DeviceButton.xaml
    /// </summary>
    public partial class DeviceButton : Button
    {
        // Using a DependencyProperty as the backing store for PressCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressCommandProperty =
            DependencyProperty.Register("PressCommand", typeof(ICommand), typeof(DeviceButton), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for PressCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressCommandParameterProperty =
            DependencyProperty.Register("PressCommandParameter", typeof(object), typeof(DeviceButton), new PropertyMetadata(0));

        // Using a DependencyProperty as the backing store for ReleaseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReleaseCommandProperty =
            DependencyProperty.Register("ReleaseCommand", typeof(ICommand), typeof(DeviceButton), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for ReleaseCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReleaseCommandParameterProperty =
            DependencyProperty.Register("ReleaseCommandParameter", typeof(object), typeof(DeviceButton), new PropertyMetadata(0));

        private Point startPosition;
        private bool isCommandsPrepared;

        public DeviceButton()
        {
            InitializeComponent();
            isCommandsPrepared = false;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            startPosition = e.MouseDevice.GetPosition(this);
            isCommandsPrepared = false;
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && !isCommandsPrepared)
            {
                var position = e.MouseDevice.GetPosition(this);
                var x = Math.Abs(position.X - startPosition.X);
                var y = Math.Abs(position.Y - startPosition.Y);
                isCommandsPrepared = (x > 100) && (position.X < ActualWidth) && (position.Y < ActualHeight);
                ExecutePressCommand();
            }
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            ExecuteReleaseCommand();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (e.LeftButton == MouseButtonState.Pressed)
                ExecuteReleaseCommand();
        }

        private void ExecutePressCommand()
        {
            if (CanExecutePressCommand())
            {
                PressCommand.Execute(PressCommandParameter);
            }
        }

        private bool CanExecutePressCommand()
        {
            return isCommandsPrepared && PressCommand != null && PressCommand.CanExecute(PressCommandParameter);
        }

        private void ExecuteReleaseCommand()
        {
            if (CanExecuteReleaseCommand())
            {
                ReleaseCommand.Execute(ReleaseCommandParameter);
            }
        }

        private bool CanExecuteReleaseCommand()
        {
            return isCommandsPrepared && ReleaseCommand != null && ReleaseCommand.CanExecute(ReleaseCommandParameter);
        }

        public ICommand PressCommand
        {
            get { return (ICommand)GetValue(PressCommandProperty); }
            set { SetValue(PressCommandProperty, value); }
        }

        public object PressCommandParameter
        {
            get { return (object)GetValue(PressCommandParameterProperty); }
            set { SetValue(PressCommandParameterProperty, value); }
        }

        public ICommand ReleaseCommand
        {
            get { return (ICommand)GetValue(ReleaseCommandProperty); }
            set { SetValue(ReleaseCommandProperty, value); }
        }

        public object ReleaseCommandParameter
        {
            get { return (object)GetValue(ReleaseCommandParameterProperty); }
            set { SetValue(ReleaseCommandParameterProperty, value); }
        }
    }
}
