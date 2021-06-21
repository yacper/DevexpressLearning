using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Utils.About;
using DevExpress.Xpf;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.PropertyGrid;
using Microsoft.Win32;
using VisualStudioDocking.ViewModels;

namespace VisualStudioDocking
{
public class BarModel : ViewModel
	{
		public BarModel(string displayName)
		{
			DisplayName = displayName;
		}
		public List<CommandViewModel> Commands { get; set; }
		public bool IsMainMenu { get; set; }
	}

	public class CommandViewModel : ViewModel
	{
		public CommandViewModel() { }
		public CommandViewModel(string displayName, List<CommandViewModel> subCommands)
			: this(displayName, null, null, subCommands)
		{
		}
		public CommandViewModel(string displayName, ICommand command = null)
			: this(displayName, null, command, null)
		{
		}
		public CommandViewModel(WorkspaceViewModel owner, ICommand command)
			: this(string.Empty, owner, command)
		{
		}
		private CommandViewModel(string displayName, WorkspaceViewModel owner = null, ICommand command = null, List<CommandViewModel> subCommands = null)
		{
			IsEnabled = true;
			Owner = owner;
			if (Owner != null)
			{
				DisplayName = Owner.DisplayName;
				Glyph = Owner.Glyph;
			}
			else DisplayName = displayName;
			Command = command;
			Commands = subCommands;
		}

		public ICommand Command { get; private set; }
		public List<CommandViewModel> Commands { get; set; }
		public BarItemDisplayMode DisplayMode { get; set; }
		public bool IsComboBox { get; set; }
		public bool IsEnabled { get; set; }
		public bool IsSeparator { get; set; }
		public bool IsSubItem { get; set; }
		public KeyGesture KeyGesture { get; set; }
		public WorkspaceViewModel Owner { get; private set; }
	}}
