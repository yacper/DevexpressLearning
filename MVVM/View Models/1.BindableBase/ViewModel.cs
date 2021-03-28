using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using DevExpress.Mvvm;

namespace _1.ViewModelBase
{

	public class ViewModel : BindableBase
	{

		public string FirstName
		{
			//  CallerMemberName using
			get { return GetValue<string>(); }
			set { SetValue(value); }
			//get { return GetValue<string>(nameof(FirstName)); }
			//set { SetValue(value, nameof(FirstName)); }
		}


		public string LastName {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: OnLastNameChanged); }		// use changed callback
        }
        void OnLastNameChanged() {
            //...
        }



	}

}
