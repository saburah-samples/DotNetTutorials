﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragDropExample
{
    /// <summary>
    /// Interaction logic for Circle.xaml
    /// </summary>
    public partial class Circle : UserControl
    {
        private Brush previousFill;
        public Circle()
        {
            InitializeComponent();
        }

        public Circle(Circle c)
        {
            InitializeComponent();
            this.circleUI.Height = c.circleUI.Height;
            this.circleUI.Width = c.circleUI.Height;
            this.circleUI.Fill = c.circleUI.Fill;
        }
    }
}
