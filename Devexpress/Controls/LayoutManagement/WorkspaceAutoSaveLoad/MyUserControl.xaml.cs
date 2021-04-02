using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DXSample {
    /// <summary>
    /// Interaction logic for MyUserControl.xaml
    /// </summary>
    public partial class MyUserControl : UserControl {
        public MyUserControl() {
            InitializeComponent();
            DataContext = new List<TestObject>() {
                new TestObject(){ Date = DateTime.Now, Number = 0, Text = "Zero" },
                new TestObject(){ Date = DateTime.Now, Number = 1, Text = "One" },
                new TestObject(){ Date = DateTime.Now, Number = 2, Text = "Two" },
            };
        }
    }

    public class TestObject {
        public int Number { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
