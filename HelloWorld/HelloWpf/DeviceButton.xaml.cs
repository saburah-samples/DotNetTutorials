using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            DependencyProperty.Register("PressCommandParameter", typeof(object), typeof(DeviceButton), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for ReleaseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReleaseCommandProperty =
            DependencyProperty.Register("ReleaseCommand", typeof(ICommand), typeof(DeviceButton), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for ReleaseCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReleaseCommandParameterProperty =
            DependencyProperty.Register("ReleaseCommandParameter", typeof(object), typeof(DeviceButton), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for LatchValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LatchValueProperty =
            DependencyProperty.Register("LatchValue", typeof(double), typeof(DeviceButton), new PropertyMetadata(0.0));

        // Using a DependencyProperty as the backing store for LatchSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LatchSizeProperty =
            DependencyProperty.Register("LatchSize", typeof(double), typeof(DeviceButton), new PropertyMetadata(100.0));

        // Using a DependencyProperty as the backing store for LatchAreaDivider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LatchAreaDividerProperty =
            DependencyProperty.Register("LatchAreaDivider", typeof(double), typeof(DeviceButton), new PropertyMetadata(2.0));

        // Using a DependencyProperty as the backing store for IsLatchActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLatchActiveProperty =
            DependencyProperty.Register("IsLatchActive", typeof(bool), typeof(DeviceButton), new PropertyMetadata(false));

        private Point startPosition;

        public DeviceButton()
        {
            InitializeComponent();
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            startPosition = e.MouseDevice.GetPosition(this);
            IsLatchActive = IsInLatchArea(startPosition);
            LatchValue = 0;
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (IsPressed)
            {
                var position = e.MouseDevice.GetPosition(this);
                if (position.X > ActualWidth || position.Y > ActualHeight)
                {
                    if (IsMouseCaptured)
                        ReleaseMouseCapture();
                }
                else if (IsLatchActive && IsLatched)
                {
                    LatchValue = Math.Round(Math.Max(0, position.X - startPosition.X));
                    ExecutePressCommand();
                }
            }
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            ExecuteReleaseCommand();
            IsLatchActive = false;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            ExecuteReleaseCommand();
            IsLatchActive = false;
        }

        private bool IsInLatchArea(Point position)
        {
            return position.X * LatchAreaDivider < ActualWidth;
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
            return IsLatchActive && !IsLatched && PressCommand != null && PressCommand.CanExecute(PressCommandParameter);
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
            return IsLatchActive && !IsLatched && ReleaseCommand != null && ReleaseCommand.CanExecute(ReleaseCommandParameter);
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

        public double LatchValue
        {
            get { return (double)GetValue(LatchValueProperty); }
            set { SetValue(LatchValueProperty, value); }
        }

        public double LatchSize
        {
            get { return (double)GetValue(LatchSizeProperty); }
            set { SetValue(LatchSizeProperty, value); }
        }

        public double LatchAreaDivider
        {
            get { return (double)GetValue(LatchAreaDividerProperty); }
            set { SetValue(LatchAreaDividerProperty, value); }
        }

        public double ActualLatchSize
        {
            get { return Math.Min(LatchSize, ActualWidth - BorderThickness.Left - BorderThickness.Right); }
        }

        public bool IsLatched
        {
            get { return LatchValue < ActualLatchSize; }
        }

        public bool IsLatchActive
        {
            get { return (bool)GetValue(IsLatchActiveProperty); }
            set { SetValue(IsLatchActiveProperty, value); }
        }
    }
}
