using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using DevExpress.Mvvm;

namespace VisualStudioDocking
{
	public abstract class ViewModel : ViewModelBase
	{
		public virtual string BindableName { get { return GetBindableName(DisplayName); } }
		public virtual string DisplayName { get; protected set; }
		public virtual ImageSource Glyph { get; set; }

		string GetBindableName(string name) { return "_" + Regex.Replace(name, @"\W", ""); }

		#region IDisposable Members
		public void Dispose()
		{
			OnDispose();
		}
		protected virtual void OnDispose() { }
#if DEBUG
		~ViewModel()
		{
			string msg = string.Format("{0} ({1}) ({2}) Finalized", GetType().Name, DisplayName, GetHashCode());
			System.Diagnostics.Debug.WriteLine(msg);
		}
#endif
		#endregion
	}
}
