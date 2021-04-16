using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using DevExpress.DirectX.Common.Direct2D;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Mvvm.Native;
using Google.Protobuf.WellKnownTypes;
using RandomGen;

public partial class Security : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	static Random _rnd = new Random();
	static string[] _Names =
				"Andy|Ben|Charlie|Dan|Ed|Fred|Gil|Herb|Jack|Karl|Larry|Mark|Noah|Oprah|Paul|Quince|Rich|Steve|Ted|Ulrich|Vic|Xavier|Zeb"
				   .Split('|');

	// ** ctors
	//public Security()
	//		: this(_rnd.Next())
	//{
	//}

	public Security(int id)
	{
		ID = id;
		Code = id.ToString();

		Name = GetString(_Names);

		Open    = Random(1, 2000);
		High    = Open + Random(0, Open * 0.1);
		Low     = Open - Random(0, Open * 0.1);
		Close   = Open;
		Current = Open;

		UpdateTime = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());

		LotSize = 1;

		Volume      = Random(1, 2000);
		VolumeTotal = Volume;

		TurnOver      = Volume * LotSize;
		TurnOverTotal = TurnOver;
		TurnOverRate = Random(1, 100) / 100;
	}

	public void ChangeRandom()
	{
		int i = Gen.Random.Numbers.Integers(0, 2)();
		if (i!=0) return;

		double offset =  Current/100;
		Change        = Gen.Random.Numbers.Doubles(-offset, offset)();
		ChangePercent = Change / Current;

		Current += offset;

		Ask    = Current + Gen.Random.Numbers.Doubles(0, 1)();
		Bid    = Current - Gen.Random.Numbers.Doubles(0, 1)();
		Spread = Ask - Bid;
		AskVol = Gen.Random.Numbers.Doubles(0, 1000)();
		BidVol = Gen.Random.Numbers.Doubles(0, 1000)();






	}

	public double Random(double min, double max)                        // 从包括min和不包括max中，获取一个随机的值
	{
		Debug.Assert(min <= max);

		if (min == max)
			return min;

		int delay = _rnd.Next((int)min, (int)max);

		return delay;
	}

	static string GetString(string[] arr) { return arr[_rnd.Next(arr.Length)]; }
}

namespace DataGrid_ProtoFodyPropetyChanged.ViewModels
{




	public class MainViewModel : ViewModelBase
	{
		public void Generate(int num)
		{
			for(int i =0; i!= num; ++i)
				Securities.Add(new Security(i));
		}

		public void Change()
		{
			Securities.ForEach(p=>p.ChangeRandom());
		}


		public MainViewModel()
		{
			Securities = new ObservableCollection<Security>();

			//people.Add(new Customer() { Name = "Gregory S. Price", City = "Hong Kong", Visits = 4, Birthday = Timestamp.FromDateTime(new DateTime(1980, 1, 1).ToUniversalTime()), Salary = 1000});
			//people.Add(new Customer() { Name = "Irma R. Marshall", City = "Madrid", Visits = 2, Birthday = Timestamp.FromDateTime(new DateTime(1966, 4, 15).ToUniversalTime()), Salary = 800});
			//people.Add(new Customer() { Name = "John C. Powell", City = "Los Angeles", Visits = 6, Birthday = Timestamp.FromDateTime(new DateTime(1982, 3, 11).ToUniversalTime()), Salary = 900 });
			//people.Add(new Customer() { Name = "Christian P. Laclair", City = "London", Visits = 11, Birthday = Timestamp.FromDateTime(new DateTime(1977, 12, 5).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Karen J. Kelly", City = "Hong Kong", Visits = 8, Birthday = Timestamp.FromDateTime(new DateTime(1956, 9, 5).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Brian C. Cowling", City = "Los Angeles", Visits = 5, Birthday = Timestamp.FromDateTime(new DateTime(1990, 2, 27).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Thomas C. Dawson", City = "Madrid", Visits = 21, Birthday = Timestamp.FromDateTime(new DateTime(1965, 5, 5).ToUniversalTime()), Salary = 500 });
			//people.Add(new Customer() { Name = "Angel M. Wilson", City = "Los Angeles", Visits = 8, Birthday = Timestamp.FromDateTime(new DateTime(1987, 11, 9).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Winston C. Smith", City = "London", Visits = 1, Birthday = Timestamp.FromDateTime(new DateTime(1949, 6, 18).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Harold S. Brandes", City = "Bangkok", Visits = 3, Birthday = Timestamp.FromDateTime(new DateTime(1989, 1, 8).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Michael S. Blevins", City = "Hong Kong", Visits = 4, Birthday = Timestamp.FromDateTime(new DateTime(1972, 9, 14).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Jan K. Sisk", City = "Bangkok", Visits = 6, Birthday = Timestamp.FromDateTime(new DateTime(1989, 5, 7).ToUniversalTime()), Salary = 800 });
			//people.Add(new Customer() { Name = "Sidney L. Holder", City = "London", Visits = 19, Birthday =Timestamp.FromDateTime( new DateTime(1971, 10, 3).ToUniversalTime()), Salary = 800 });
			//Securities = sec;


			//var cities = from c in people select c.City;
			//var cities = sec.Select(p => p.City);
			//Cities = cities.Distinct().ToObservableCollection();
		}
		//public ObservableCollection<string> Cities { get; private set; }
		public ObservableCollection<Security> Securities { get; private set; }
	}

}