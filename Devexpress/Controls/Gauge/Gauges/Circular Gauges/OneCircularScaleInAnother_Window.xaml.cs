using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gauges.Circular_Gauges
{
	/// <summary>
	/// OneCircularScaleInAnother_Window.xaml 的交互逻辑
	/// </summary>
	public partial class OneCircularScaleInAnother_Window : Window
	{
		public OneCircularScaleInAnother_Window()
		{
			InitializeComponent();
			DataContext = new ClockViewModel();
		}

		public class ClockViewModel : DependencyObject
		{
			public static readonly DependencyProperty HourProperty =
				DependencyProperty.Register("Hour", typeof(double), typeof(ClockViewModel));
			public static readonly DependencyProperty MinuteProperty =
				DependencyProperty.Register("Minute", typeof(double), typeof(ClockViewModel));
			public static readonly DependencyProperty SecondProperty =
				DependencyProperty.Register("Second", typeof(double), typeof(ClockViewModel));
			public static readonly DependencyProperty DayOfWeekProperty =
				DependencyProperty.Register("DayOfWeek", typeof(double), typeof(ClockViewModel));
			public static readonly DependencyProperty DayOfMonthProperty =
				DependencyProperty.Register("DayOfMonth", typeof(double), typeof(ClockViewModel));
			public static readonly DependencyProperty MonthOfYearProperty =
				DependencyProperty.Register("MonthOfYear", typeof(double), typeof(ClockViewModel));

			public double Hour
			{
				get { return (double)GetValue(HourProperty); }
				set { SetValue(HourProperty, value); }
			}

			public double Minute
			{
				get { return (double)GetValue(MinuteProperty); }
				set { SetValue(MinuteProperty, value); }
			}

			public double Second
			{
				get { return (double)GetValue(SecondProperty); }
				set { SetValue(SecondProperty, value); }
			}

			public double DayOfWeek
			{
				get { return (double)GetValue(DayOfWeekProperty); }
				set { SetValue(DayOfWeekProperty, value); }
			}

			public double DayOfMonth
			{
				get { return (int)GetValue(DayOfMonthProperty); }
				set { SetValue(DayOfMonthProperty, value); }
			}

			public double MonthOfYear
			{
				get { return (double)GetValue(MonthOfYearProperty); }
				set { SetValue(MonthOfYearProperty, value); }
			}

			readonly DispatcherTimer timer = new DispatcherTimer();

			public ClockViewModel()
			{
				timer.Tick += new EventHandler(OnTimedEvent);
				timer.Start();
				Update();
			}

			void Update()
			{
				DateTime currentDate = DateTime.Now;

				Second = (currentDate.Second / 60.0) * 12;
				Minute = ((currentDate.Minute + currentDate.Second / 60.0) / 60.0) * 12;
				Hour = (currentDate.Hour % 12) + currentDate.Minute / 60.0;

				DayOfMonth = currentDate.Day;
				DayOfWeek = (int)currentDate.DayOfWeek;
				MonthOfYear = currentDate.Month;
			}

			void OnTimedEvent(object source, EventArgs e)
			{
				Update();
			}
		}
	}
}
