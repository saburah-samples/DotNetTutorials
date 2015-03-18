using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RoFF.Controls
{
    [TemplatePart(Name = "UpButtonElement", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "DownButtonElement", Type = typeof(RepeatButton))]
    [TemplateVisualState(Name = NumericUpDown.STATE_POSITIVE, GroupName = "ValueStates")]
    [TemplateVisualState(Name = NumericUpDown.STATE_NEGATIVE, GroupName = "ValueStates")]
    [TemplateVisualState(Name = NumericUpDown.STATE_FOCUSED, GroupName = "FocusedStates")]
    [TemplateVisualState(Name = NumericUpDown.STATE_UNFOCUSED, GroupName = "FocusedStates")]
    public class NumericUpDown: Control
    {
        private const string ELEMENT_UP_BUTTON = "UpButton";
        private const string ELEMENT_DOWN_BUTTON = "DownButton";
        //ValueStates
        private const string STATE_NEGATIVE = "Negative";
        private const string STATE_POSITIVE = "Positive";
        //FocusStates
        private const string STATE_FOCUSED = "Focused";
        private const string STATE_UNFOCUSED = "Unfocused";

        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty BorderBrushProperty;
        public static readonly DependencyProperty BorderThicknessProperty;
        public static readonly DependencyProperty FontFamilyProperty;
        public static readonly DependencyProperty FontSizeProperty;
        public static readonly DependencyProperty FontStretchProperty;
        public static readonly DependencyProperty FontStyleProperty;
        public static readonly DependencyProperty FontWeightProperty;
        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty HorizontalContentAlignmentProperty;
        public static readonly DependencyProperty PaddingProperty;
        public static readonly DependencyProperty TextAlignmentProperty;
        public static readonly DependencyProperty TextDecorationsProperty;
        public static readonly DependencyProperty TextWrappingProperty;
        public static readonly DependencyProperty VerticalContentAlignmentProperty;

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown), 
            new PropertyMetadata(new PropertyChangedCallback(ValueChangedCallback)));

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Direct,
            typeof(ValueChangedEventHandler), typeof(NumericUpDown));

        private static void ValueChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var ctl = (NumericUpDown)obj;
            var newValue = (double)args.NewValue;

            // Call UpdateStates because the Value might have caused the control to change ValueStates.
            ctl.UpdateStates(true);

            // Call OnValueChanged to raise the ValueChanged event.
            ctl.OnValueChanged(new ValueChangedEventArgs(NumericUpDown.ValueChangedEvent, newValue));
        }

        protected virtual void OnValueChanged(ValueChangedEventArgs args)
        {
            RaiseEvent(args);
        }

        private RepeatButton upButtonElement;
        private RepeatButton downButtonElement;

        public NumericUpDown()
        {
            this.Resources.MergedDictionaries.Add(SharedDictionaryManager.SharedDictionary);
            DefaultStyleKey = typeof(NumericUpDown);
            IsTabStop = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpButtonElement = GetTemplateChild(ELEMENT_UP_BUTTON) as RepeatButton;
            DownButtonElement = GetTemplateChild(ELEMENT_DOWN_BUTTON) as RepeatButton;

            UpdateStates(false);
        }

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Focus();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            UpdateStates(true);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            UpdateStates(true);
        }

        private void UpButtonElementClick(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void DownButtonElementClick(object sender, RoutedEventArgs e)
        {
            Value--;
        }

        private void UpdateStates(bool useTransitions)
        {
            if (Value >= 0)
                VisualStateManager.GoToState(this, STATE_POSITIVE, useTransitions);
            else
                VisualStateManager.GoToState(this, STATE_NEGATIVE, useTransitions);

            if (IsFocused)
                VisualStateManager.GoToState(this, STATE_FOCUSED, useTransitions);
            else
                VisualStateManager.GoToState(this, STATE_UNFOCUSED, useTransitions);
        }

        public Brush Background { get; set; }
        public Brush BorderBrush { get; set; }
        public Thickness BorderThickness { get; set; }
        public FontFamily FontFamily { get; set; }
        public double FontSize { get; set; }
        public FontStretch FontStretch { get; set; }
        public FontStyle FontStyle { get; set; }
        public FontWeight FontWeight { get; set; }
        public Brush Foreground { get; set; }
        public HorizontalAlignment HorizontalContentAlignment { get; set; }
        public Thickness Padding { get; set; }
        public TextAlignment TextAlignment { get; set; }
        public TextDecorationCollection TextDecorations { get; set; }
        public TextWrapping TextWrapping { get; set; }
        public VerticalAlignment VerticalContentAlignment { get; set; }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private RepeatButton UpButtonElement
        {
            get { return upButtonElement; }
            set
            {
                if (upButtonElement != null)
                {
                    upButtonElement.Click -= new RoutedEventHandler(UpButtonElementClick);
                }
                upButtonElement = value;
                if (upButtonElement != null)
                {
                    upButtonElement.Click += new RoutedEventHandler(UpButtonElementClick);
                }
            }
        }

        private RepeatButton DownButtonElement
        {
            get { return downButtonElement; }
            set
            {
                if (downButtonElement != null)
                {
                    downButtonElement.Click -= new RoutedEventHandler(DownButtonElementClick);
                }
                downButtonElement = value;
                if (downButtonElement != null)
                {
                    downButtonElement.Click += new RoutedEventHandler(DownButtonElementClick);
                }
            }
        }

        #region ValueChangedEvent

        public class ValueChangedEventArgs : RoutedEventArgs
        {
            public ValueChangedEventArgs(RoutedEvent id, double value)
            {
                Value = value;
                RoutedEvent = id;
            }
            public double Value { get; private set; }
        }

        public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs args);

        public event ValueChangedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }        

        #endregion
    }
}
