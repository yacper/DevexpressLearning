﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cars
{
    /// <summary>
    /// CarDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class CarDetailView : UserControl
    {
        public CarDetailView()
        {
            InitializeComponent();
        }

        private Car car;

        public Car Car
        {
            get { return car; }
            set
            {
                car = value;
                this.txtBlockName.Text = car.Name;
                this.txtBlockAutoMark.Text = car.AutoMark;
                this.txtBlockYear.Text = car.Year;
                this.txtTopSpeed.Text = car.TopSpeed;
                this.imgPhoto.Source = new BitmapImage(new Uri(@"Resource/Image/" + car.Name + ".jpg", UriKind.Relative));
            }
        }
    }
}
