//using C1.WPF.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Data;
using C1.C1Excel;

namespace WPFGridsBenchmark
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public ObservableCollection<Customer> Datas { get; set; }
		TestGrid                              _grid       = null;
		ManualResetEvent                      _uiUpdated  = null;
		List<double>                          _totalTimes = null;
		Stopwatch                             _sw;


		public Array		VirtualizationModes
		{
			get { return Enum.GetValues(typeof(VirtualizationMode)); }
		}
		public Array		BindingModes
		{
			get { return Enum.GetValues(typeof(BindingMode)); }
		}

		public Array		ScrollUnits
		{
			get { return Enum.GetValues(typeof(ScrollUnit)); }
		}

		protected bool               _IsVirtualizing;
		protected VirtualizationMode _SelectedVirtualizationMode;
		protected bool               _EnableColumnVirtualization;
		protected bool               _EnableRowVirtualization;
		protected bool               _CanContentScroll;

		protected bool       _IsAsyncBinding;
		protected bool       _IsDeferredScrolling;
		protected ScrollUnit _selectedScrollUnit;

		protected BindingMode        _SelectedBindingMode;


		public bool IsVirtualizing
		{
			get { return _IsVirtualizing; }
			set
			{
				if (_IsVirtualizing == value)
					return;

				//if(_grid?.Grid != null)
				//{
				//	(_grid.Grid as DataGrid).SetValue(VirtualizingStackPanel.IsVirtualizingProperty, value);

				//}

					_IsVirtualizing = value;
					RaisePropertyChanged(nameof(IsVirtualizing));
			}
		}


		public VirtualizationMode SelectedVirtualizationMode
		{
			get { return _SelectedVirtualizationMode; }
			set
			{
				if (_SelectedVirtualizationMode == value)
					return;

				//if(_grid?.Grid != null)
				//{
				//	(_grid.Grid as DataGrid).SetValue(VirtualizingStackPanel.VirtualizationModeProperty, value);
				//}
					_SelectedVirtualizationMode = value;
					RaisePropertyChanged(nameof(SelectedVirtualizationMode));
			}

		}

		public bool EnableColumnVirtualization
		{
			get { return _EnableColumnVirtualization; }
			set
			{
				if (_EnableColumnVirtualization == value)
					return;

				//if(_grid?.Grid != null)
				//{
				//	(_grid.Grid as DataGrid).EnableColumnVirtualization = value;
				//}
					_EnableColumnVirtualization = value;

					RaisePropertyChanged(nameof(EnableColumnVirtualization));
			}
		}
		public bool EnableRowVirtualization
		{
			get { return _EnableRowVirtualization; }
			set
			{
				if (_EnableRowVirtualization == value)
					return;

				//if(_grid?.Grid != null)
				//{
				//	(_grid.Grid as DataGrid).EnableRowVirtualization = value;
				//}
					_EnableRowVirtualization = value;
					
					RaisePropertyChanged(nameof(EnableRowVirtualization));
			}
		}

		public bool CanContentScroll
		{
			get { return _CanContentScroll; }
			set
			{
				if (_CanContentScroll == value)
					return;

				//if(_grid?.Grid != null)
				//{
				//	(_grid.Grid as DataGrid).EnableRowVirtualization = value;
				//}
					_CanContentScroll = value;
					
					RaisePropertyChanged(nameof(CanContentScroll));
			}
		}


		public bool IsAsyncBinding
		{
			get
			{
				return _IsAsyncBinding;
			}
			set
			{
				if (_IsAsyncBinding == value)
					return;

				_IsAsyncBinding = value;

				RaisePropertyChanged(nameof(IsAsyncBinding));
			}
		}

		public BindingMode SelectedBindingMode 
		{
			get
			{
				return _SelectedBindingMode;
			}
			set
			{
				if (_SelectedBindingMode == value)
					return;

				_SelectedBindingMode = value;

				RaisePropertyChanged(nameof(SelectedBindingMode));
			}
		}


		public bool IsDeferredScrolling
		{
			get
			{
				return _IsDeferredScrolling;
			}
			set
			{
				if (_IsDeferredScrolling == value)
					return;

				_IsDeferredScrolling = value;

				RaisePropertyChanged(nameof(IsDeferredScrolling));
			}
		}

		public ScrollUnit SelectedScrollUnit
		{
			get
			{
				return _selectedScrollUnit;
			}
			set
			{
				if (_selectedScrollUnit == value)
					return;

				_selectedScrollUnit = value;

				RaisePropertyChanged(nameof(SelectedScrollUnit));
			}
		}


		Dictionary<string, Type> _grids;

		private bool _runningAllTests = false;

		public MainWindow()
		{
			InitializeComponent();
			Test.DataContext = new List<string>()
			                   {
				                   "First loading", // load grid in application for the first time, should include xaml parsing and clr compilation. Performed once regardless of test number
				                   "Create grid and load data", // create new grid and load data
				                   "Load data", // reload data into existent grid
				                   "Sort column", // sort single column
				                   "Scroll 100 rows", // scroll by 100 rows
				                   "Scroll full grid"
			                   }; // scrool to End/Home
			Times.DataContext    = new List<int>() {1, 5, 10, 100, 1000};
			Number.DataContext   = new List<int>() {10, 100, 1000, 10000, 50000, 100000};
			_grids               = TestGrid.Grids;
			GridType.DataContext = _grids.Keys;

			DataContext = this;

			Loaded += OnLoaded;


		}

		protected override void OnClosing(CancelEventArgs e)
		{
			CleanExcel();
			base.OnClosing(e);
		}

		#region handlers

		void GridChanged(object sender, SelectionChangedEventArgs e) { DisposeGrid(true); }

		private void Number_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// re-create data
			DisposeGrid(true);
			Datas = Customer.GetCustomerList((int) Number.SelectedItem);
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			Number.SelectedIndex = GridType.SelectedIndex = Test.SelectedIndex = 0;
			Times.SelectedIndex  = 1;
			_totalTimes          = new List<double>();
			_uiUpdated           = new ManualResetEvent(false);
			_sw                  = new Stopwatch();

			InitExcelBook();

			/// defalut settings
			DataGrid g = new DataGrid();  // default grid

			IsVirtualizing = (bool)g.GetValue(VirtualizingStackPanel.IsVirtualizingProperty);
			SelectedVirtualizationMode = (VirtualizationMode)g.GetValue(VirtualizingStackPanel.VirtualizationModeProperty);
			CanContentScroll = (bool)(g.GetValue(ScrollViewer.CanContentScrollProperty));
			IsDeferredScrolling = (bool)(g.GetValue(ScrollViewer.IsDeferredScrollingEnabledProperty));
			SelectedScrollUnit = 
				(ScrollUnit)g.GetValue(VirtualizingPanel.ScrollUnitProperty);  // no use for performance

			EnableRowVirtualization = g.EnableRowVirtualization;
			EnableColumnVirtualization = g.EnableColumnVirtualization;
		}

		private void RunTest_Click(object sender, RoutedEventArgs e) { RunNextTest(); }

		private void RunAllTests(object sender, RoutedEventArgs e)
		{
			_runningAllTests             = true;
			IsFixedColumnWidth.IsChecked = true;
			GridType.SelectedIndex       = -1;
			Test.SelectedIndex           = -1;
			RunNextTest();
		}

		private void RunNextTest()
		{
			if (_runningAllTests)
			{
				// move to next test if we are not done yet
				if (Test.SelectedIndex == -1)
				{
					Test.SelectedIndex     = 0;
					GridType.SelectedIndex = 0;
				}
				else if (Test.SelectedIndex == Test.Items.Count - 1)
				{
					if (GridType.SelectedIndex == GridType.Items.Count - 1)
					{
						if (IsFixedColumnWidth.IsChecked.Value)
						{
							IsFixedColumnWidth.IsChecked = false;
							Test.SelectedIndex           = 1;
							GridType.SelectedIndex       = 0;
						}
						else
						{
							// we are done
							Load.IsEnabled   = true;
							_runningAllTests = false;
							return;
						}
					}
					else
					{
						if (IsFixedColumnWidth.IsChecked.Value) { Test.SelectedIndex = 0; }
						else
						{
							// skip first load at second pass
							Test.SelectedIndex = 1;
						}

						GridType.SelectedIndex++;
					}
				}
				else { Test.SelectedIndex++; }
			}

			// Initialize test
			DisposeGrid(true);
			long currentMemoryUsage = GC.GetTotalMemory(true);
			Load.IsEnabled = false;

			// Start
			Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
			                                                             {
				                                                             switch (Test.SelectedIndex)
				                                                             {
					                                                             case 0:
						                                                             DoCreateAndLoad(currentMemoryUsage);
						                                                             break;
					                                                             case 1:
						                                                             DoCreateAndLoad(currentMemoryUsage);
						                                                             break;
					                                                             case 2:
						                                                             DoLoad(currentMemoryUsage);
						                                                             break;
					                                                             case 3:
						                                                             DoSort(currentMemoryUsage);
						                                                             break;
					                                                             case 4:
						                                                             DoScroll(currentMemoryUsage, 100);
						                                                             break;
					                                                             case 5:
						                                                             DoScroll(currentMemoryUsage,
						                                                              -1); // full grid (to Home, to End)
						                                                             break;
				                                                             }
			                                                             }));
		}

		#endregion

		#region tests

		#region ** create grid and load data

		async void DoCreateAndLoad(long currentMemory)
		{
			_totalTimes.Clear();
			Info.Text = String.Empty;
			int count = Test.SelectedIndex == 0 ? 1 : (int) Times.SelectedItem;

			if (Test.SelectedIndex > 0 && !IsFixedColumnWidth.IsChecked.Value)
			{
				Type gridType = _grids[(string) GridType.SelectedItem];
				if (!TestGrid.GridsForAutoSizeTesting.Contains(gridType))
				{
					Result res = new Result(null, false, true);
					Info.Text      = res.ToString();
					Load.IsEnabled = true;
					Log(res);
					return;
				}
			}

			for (int i = 0; i < count; i++)
			{
				Info.Text = String.Format("Create and load {1} of {0} times", count, i + 1);
				await InitializeGrid(i, true);
			}

			Result result = new Result(_totalTimes, false);
			long   m      = GC.GetTotalMemory(true);
			result.MemoryUsed = (GC.GetTotalMemory(true) - currentMemory) / 1024;
			Info.Text         = result.ToString();
			_sw.Reset();
			Load.IsEnabled = true;
			Log(result);
		}

		#endregion

		#region ** load data into existent grid

		async void DoLoad(long currentMemory)
		{
			_totalTimes.Clear();
			Info.Text = String.Empty;
			int count = (int) Times.SelectedItem;

			// create grid before test, so that test only counts loading data
			CreateGrid(false);

			if (!IsFixedColumnWidth.IsChecked.Value && !TestGrid.GridsForAutoSizeTesting.Contains(_grid.GetType()))
			{
				DisposeGrid(true);
				Result res = new Result(null, false, true);
				Info.Text      = res.ToString();
				Load.IsEnabled = true;
				Log(res);
				return;
			}

			for (int i = 0; i < count; i++)
			{
				Info.Text = String.Format("Re-load {1} of {0} times", count, i + 1);
				await InitializeGrid(i, false);
			}

			if (_runningAllTests) { DisposeGrid(true); }

			Result result = new Result(_totalTimes, false);
			long   m      = GC.GetTotalMemory(true);
			result.MemoryUsed = (GC.GetTotalMemory(true) - currentMemory) / 1024;
			Info.Text         = result.ToString();
			_sw.Reset();
			Load.IsEnabled = true;
			Log(result);
		}

		#endregion

		#region ** sort grid

		async void DoSort(long currentMemory)
		{
			_totalTimes.Clear();
			Info.Text = String.Empty;
			int count = (int) Times.SelectedItem;

			// create grid and load before test, so that test only counts sorting
			CreateGrid(true);
			if (!IsFixedColumnWidth.IsChecked.Value && !TestGrid.GridsForAutoSizeTesting.Contains(_grid.GetType()))
			{
				DisposeGrid(true);
				Result res = new Result(null, false, true);
				Info.Text      = res.ToString();
				Load.IsEnabled = true;
				Log(res);
				return;
			}

			await Task.Delay(1000 * (Number.SelectedIndex
			                       + 1)); // delay to make sure that control is really initialized before test

			bool ascending = false;
			for (int i = 0; i < count; i++)
			{
				Info.Text = String.Format("Sorting {1} of {0} times", count, i + 1);
				await SortGrid(i, ascending);
				ascending = !ascending;
			}

			if (_runningAllTests) { DisposeGrid(true); }

			Result result = new Result(_totalTimes, false);
			long   m      = GC.GetTotalMemory(true);
			result.MemoryUsed = (GC.GetTotalMemory(true) - currentMemory) / 1024;
			Info.Text         = result.ToString();
			_sw.Reset();
			Load.IsEnabled = true;
			Log(result);
		}

		async Task SortGrid(int index, bool ascending)
		{
			_sw.Reset();
			_sw.Start();
			_grid.LayoutUpdated += HandleGridLayoutUpdated; // * subscribe in the last moment
			_grid.Sort(ascending);
			await RunTaskAsync();
			_sw.Stop();
			_totalTimes.Add(_sw.ElapsedMilliseconds);
		}

		#endregion

		#region ** scrolling

		async void DoScroll(long currentMemory, int rowsNumber)
		{
			_totalTimes.Clear();
			Info.Text = String.Empty;
			int count = (int) Times.SelectedItem;

			// create grid and load before test, so that test only counts sorting
			CreateGrid(true);
			if (!IsFixedColumnWidth.IsChecked.Value && !TestGrid.GridsForAutoSizeTesting.Contains(_grid.GetType()))
			{
				DisposeGrid(true);
				Result res = new Result(null, false, true);
				Info.Text      = res.ToString();
				Load.IsEnabled = true;
				Log(res);
				return;
			}

			await Task.Delay(1000 * (Number.SelectedIndex
			                       + 1)); // delay to make sure that control is really initialized before test

			for (int i = 0; i < count; i++)
			{
				Info.Text = String.Format("Scrolling {1} of {0} times", count, i + 1);
				await ScrollGrid(i, rowsNumber);
			}

			if (_runningAllTests) { DisposeGrid(true); }

			Result result = new Result(_totalTimes, false);
			long   m      = GC.GetTotalMemory(true);
			result.MemoryUsed = (GC.GetTotalMemory(true) - currentMemory) / 1024;
			Info.Text         = result.ToString();
			_sw.Reset();
			Load.IsEnabled = true;
			Log(result);
		}

		async Task ScrollGrid(int index, int rowsNumber)
		{
			int newIndex;
			int totalRows = (int) Number.SelectedItem;
			if (rowsNumber == -1)
			{
				newIndex = (index % 2 == 0) ? 1 : totalRows - 1; // use 1 for first index, as some grids have no 0 row
			}
			else { newIndex = ((index + 1) * rowsNumber) % totalRows; }

			_sw.Reset();
			_sw.Start();
			_grid.LayoutUpdated += HandleGridLayoutUpdated; // * subscribe in the last moment
			_grid.ScrollInView(newIndex);
			await RunTaskAsync();
			_sw.Stop();
			_totalTimes.Add(_sw.ElapsedMilliseconds);
		}

		#endregion

		#endregion

		#region implementation

		void CreateGrid(bool loadData)
		{
			if (_grid == null)
			{
				_grid = Activator.CreateInstance(_grids[(string) GridType.SelectedItem],
				                                 new object[] {IsFixedColumnWidth.IsChecked.Value}) as TestGrid;
			}

			{

				(_grid as MSDataGrid).Grid.SetValue(VirtualizingStackPanel.IsVirtualizingProperty, _IsVirtualizing);
				(_grid as MSDataGrid).Grid.SetValue(VirtualizingStackPanel.VirtualizationModeProperty, _SelectedVirtualizationMode);

				(_grid as MSDataGrid).Grid.SetValue(ScrollViewer.CanContentScrollProperty, CanContentScroll);

				(_grid as MSDataGrid).Grid.SetValue(ScrollViewer.IsDeferredScrollingEnabledProperty, IsDeferredScrolling);
				(_grid as MSDataGrid).Grid.SetValue(VirtualizingPanel.ScrollUnitProperty, SelectedScrollUnit);  // no use for performance


				(_grid as MSDataGrid).Grid.EnableColumnVirtualization = _EnableColumnVirtualization;
				(_grid as MSDataGrid).Grid.EnableRowVirtualization = _EnableRowVirtualization;
			}


			if (Container.Child == null)
			{
				Container.Child = _grid;
				if (loadData) { _grid.Load(this, Datas, SelectedBindingMode, IsAsyncBinding); }
			}
		}

		void HandleGridLayoutUpdated(object sender, object e)
		{
			if (_grid != null) { _grid.LayoutUpdated -= HandleGridLayoutUpdated; }

			// use grid.Dispatcher instead of Window.Dispatcher
			_grid.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() => { _uiUpdated.Set(); }));
		}

		void DisposeGrid(bool needsDisposeGrid)
		{
			if (_grid != null)
			{
				if (needsDisposeGrid)
				{
					_grid.LayoutUpdated -= HandleGridLayoutUpdated;
					Container.Child     =  null;
					_grid.Dispose();
					_grid = null;
				}
				else
				{
					// only clear grid from data
					_grid.Load(this, null, SelectedBindingMode, IsAsyncBinding);
				}
			}

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		async Task InitializeGrid(int index, bool fullDispose)
		{
			_sw.Reset();
			_sw.Start();
			CreateGrid(false);
			_grid.LayoutUpdated += HandleGridLayoutUpdated; // * subscribe in the last moment
			_grid.Load(this, Datas, SelectedBindingMode, IsAsyncBinding);
			await RunTaskAsync();
			_sw.Stop();
			_totalTimes.Add(_sw.ElapsedMilliseconds);
			if (index != (int) Times.SelectedItem - 1) { DisposeGrid(fullDispose); }
		}

		async Task RunTaskAsync()
		{
			var task = Task.Run(() =>
			                    {
				                    _uiUpdated.WaitOne(30000);
				                    _uiUpdated.Reset();
			                    });

			await task;
		}

		#endregion

		#region loggging

		string _xlFileName = string.Empty;
		C1XLBook _xlBook;

		XLStyle _cornerStyle;
		XLStyle _colHeaderStyle;
		XLStyle _rowHeaderStyle;
		XLStyle _legendStyle;

		void InitExcelBook()
		{
			_xlFileName = string.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.Now) + ".xls";
			_xlBook = new C1XLBook();

			XLSheet sheet = _xlBook.Sheets[0];
			sheet.Columns[0].Width = C1XLBook.PixelsToTwips(400);
			for (int i = 1; i <= 12; i++) { sheet.Columns[i].Width = C1XLBook.PixelsToTwips(100); }

			_cornerStyle = new XLStyle(_xlBook);
			_cornerStyle.AlignHorz = XLAlignHorzEnum.Center;
			_cornerStyle.AlignVert = XLAlignVertEnum.Bottom;
			_cornerStyle.BorderRight = XLLineStyleEnum.Thin;
			_cornerStyle.BorderBottom = XLLineStyleEnum.Thin;

			_rowHeaderStyle = new XLStyle(_xlBook);
			_rowHeaderStyle.AlignHorz = XLAlignHorzEnum.Left;
			_rowHeaderStyle.AlignVert = XLAlignVertEnum.Top;
			_rowHeaderStyle.BorderRight = XLLineStyleEnum.Thin;
			//_rowHeaderStyle.Font =
			//	new XLFont(_rowHeaderStyle.Font.FontName, _rowHeaderStyle.Font.FontSize, true, false);

			_colHeaderStyle = new XLStyle(_xlBook);
			_colHeaderStyle.AlignHorz = XLAlignHorzEnum.Left;
			_colHeaderStyle.AlignVert = XLAlignVertEnum.Top;
			_colHeaderStyle.BorderBottom = XLLineStyleEnum.Thin;
			//_colHeaderStyle.Font =
			//	new XLFont(_rowHeaderStyle.Font.FontName, _rowHeaderStyle.Font.FontSize, true, false);
			_colHeaderStyle.WordWrap = true;

			_legendStyle = new XLStyle(_xlBook);
			_legendStyle.AlignHorz = XLAlignHorzEnum.Left;
			_legendStyle.AlignVert = XLAlignVertEnum.Center;
//			_legendStyle.Font = new XLFont(_rowHeaderStyle.Font.FontName, _rowHeaderStyle.Font.FontSize, true, false);
			_legendStyle.WordWrap = true;
		}

		void OutputExcel(object output, XLStyle style, int row, int col)
		{
			XLSheet sheet = _xlBook.Sheets[0];
			if (sheet[row, col].Value == null) sheet[row, col].Value = output;
			if (style != null && sheet[row, col].Style == null) sheet[row, col].Style = style;
			_xlBook.Save(_xlFileName);
			}

		void OutputEnvironmentInfo()
		{
			string output = "OS: " + Environment.OSVersion + ", " +
							".Net Version: " + Environment.Version + ", " +
							(Environment.Is64BitOperatingSystem ? "x64" : "x86") + " machine, " +
							(Environment.Is64BitProcess ? "x64" : "x86") + " process, " +
							"CPUs: " + Environment.ProcessorCount + ", " +
							"working set: " + Environment.WorkingSet + ", Debugger.IsAttached = "
						  + Debugger.IsAttached.ToString().ToLower() +
							" Test control size: " + this.Container.ActualWidth + "*" + this.Container.ActualHeight;

			Trace.WriteLine(output);

			XLSheet sheet = _xlBook.Sheets[0];
			int row = sheet.Rows.Count + 2;
			XLCellRange environmentInfoRange = new XLCellRange(row, row + 2, 0, 6);
			sheet.MergedCells.Add(environmentInfoRange);

			OutputExcel(output, _legendStyle, row, 0);
			row += 2;
			foreach (Type gridType in _grids.Values)
			{
				row++;
				TestGrid grid = Activator.CreateInstance(gridType, new object[] { true }) as TestGrid;
				OutputExcel(grid.GetGridInfo(), _legendStyle, row, 0);
			}
		}

		void CleanExcel()
		{
			if (_xlBook == null) { return; }

			OutputEnvironmentInfo();
			XLSheet sheet = _xlBook.Sheets[0];

			int row = 0;
			int col = 0;

			if (sheet.MergedCells.Count > 0)
			{
				while (row < sheet.MergedCells[0].RowTo)
				{
					col = 1;
					while (col < sheet.Columns.Count)
					{
						if (sheet[row, col].Value == null || (sheet[row, col].Value is string
														   && string.IsNullOrEmpty(sheet[row, col].Value.ToString())))
							col++;
						else
							break;
					}

					if (col == sheet.Columns.Count && !sheet.MergedCells[0].Contains(sheet, row, 0))
						sheet.Rows.Remove(sheet.Rows[row]);
					else
						row++;
				}
			}

			col = 0;
			row = 0;

			while (col < sheet.Columns.Count)
			{
				row = 1;
				while (row < sheet.Rows.Count)
				{
					if (sheet[row, col].Value == null || (sheet[row, col].Value is string
													   && string.IsNullOrEmpty(sheet[row, col].Value.ToString())))
						row++;
					else
						break;
				}

				if (row == sheet.Rows.Count && !sheet.MergedCells[0].Contains(sheet, 0, col))
					sheet.Columns.Remove(sheet.Columns[col]);
				else
					col++;
			}

			_xlBook.Save(_xlFileName);
			_xlBook = null;
		}

		void Log(Result result)
		{

			// log to Excel and to Trace
			string grid = (string)GridType.SelectedItem;
			string fixedCols = IsFixedColumnWidth.IsChecked.Value ? "Fixed columns" : "Autosize columns";
			string text = string.Format("Type: {0}, {5}, Test: {1}, Rows: {2}, Times: {3}, {4} ",
										grid, Test.SelectedItem, (int)Number.SelectedItem, (int)Times.SelectedItem,
										result.ToString(), fixedCols);
			Trace.WriteLine(text);

			int timeRowIndex = GridType.SelectedIndex + 1;
			OutputExcel(grid + ", time, ms", _rowHeaderStyle, timeRowIndex, 0);

			string dataDisplayModeAndCommand = (string)Test.SelectedItem + " " + fixedCols + " "
											 + ((int)Number.SelectedItem).ToString() + " rows";

			int colIndex = Test.SelectedIndex + 1;
			if (!IsFixedColumnWidth.IsChecked.Value) { colIndex += Test.Items.Count - 1; }

			OutputExcel(dataDisplayModeAndCommand, _colHeaderStyle, 0, colIndex);
			if (result.NotSupported) { OutputExcel(0, null, timeRowIndex, colIndex); }
			else { OutputExcel(result.Average, null, timeRowIndex, colIndex); }

			if (_runningAllTests)
			{
				// continue
				RunNextTest();
			}
		}

		#endregion

#region ** INotifyPropertyChanged Members

		// this interface allows bounds controls to react to changes in the data objects.

		void RaisePropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null) PropertyChanged(this, e);
		}

		#endregion

	}
}