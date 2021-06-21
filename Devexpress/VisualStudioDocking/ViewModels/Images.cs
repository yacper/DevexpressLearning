using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core;

namespace VisualStudioDocking.ViewModels
{
	public static class Images
	{
		public static ImageSource About { get { return GetSvgImage(ImagePaths.About); } }
		public static ImageSource CloseSolution { get { return GetSvgImage(ImagePaths.CloseSolution); } }
		public static ImageSource Copy { get { return GetSvgImage(ImagePaths.Copy); } }
		public static ImageSource Cut { get { return GetSvgImage(ImagePaths.Cut); } }
		public static ImageSource DevExpressLogo { get { return GetImage(ImagePaths.DevExpressLogo); } }
		public static ImageSource Error { get { return GetSvgImage(ImagePaths.Error); } }
		public static ImageSource File { get { return GetSvgImage(ImagePaths.File); } }
		public static ImageSource FileCS { get { return GetSvgImage(ImagePaths.FileCS); } }
		public static ImageSource FileXaml { get { return GetSvgImage(ImagePaths.FileXaml); } }
		public static ImageSource Find { get { return GetSvgImage(ImagePaths.Find); } }
		public static ImageSource FindInFiles { get { return GetSvgImage(ImagePaths.FindInFiles); } }
		public static ImageSource FindInFilesWindow { get { return GetSvgImage(ImagePaths.FindInFilesWindow); } }
		public static ImageSource Info { get { return GetSvgImage(ImagePaths.Info); } }
		public static ImageSource LoadLayout { get { return GetSvgImage(ImagePaths.LoadLayout); } }
		public static ImageSource NewProject { get { return GetSvgImage(ImagePaths.NewProject); } }
		public static ImageSource OpenFile { get { return GetSvgImage(ImagePaths.OpenFile); } }
		public static ImageSource OpenSolution { get { return GetSvgImage(ImagePaths.OpenSolution); } }
		public static ImageSource Output { get { return GetSvgImage(ImagePaths.Output); } }
		public static ImageSource Paste { get { return GetSvgImage(ImagePaths.Paste); } }
		public static ImageSource PropertiesWindow { get { return GetSvgImage(ImagePaths.PropertiesWindow); } }
		public static ImageSource Redo { get { return GetSvgImage(ImagePaths.Redo); } }
		public static ImageSource References { get { return GetSvgImage(ImagePaths.References); } }
		public static ImageSource Refresh { get { return GetSvgImage(ImagePaths.Refresh); } }
		public static ImageSource Replace { get { return GetSvgImage(ImagePaths.Replace); } }
		public static ImageSource Run { get { return GetSvgImage(ImagePaths.Run); } }
		public static ImageSource Save { get { return GetSvgImage(ImagePaths.Save); } }
		public static ImageSource SaveAll { get { return GetSvgImage(ImagePaths.SaveAll); } }
		public static ImageSource SaveLayout { get { return GetSvgImage(ImagePaths.SaveLayout); } }
		public static ImageSource ShowAllFiles { get { return GetSvgImage(ImagePaths.ShowAllFiles); } }
		public static ImageSource SolutionExplorer { get { return GetSvgImage(ImagePaths.SolutionExplorer); } }
		public static ImageSource TaskList { get { return GetSvgImage(ImagePaths.TaskList); } }
		public static ImageSource Toolbox { get { return GetSvgImage(ImagePaths.Toolbox); } }
		public static ImageSource Undo { get { return GetSvgImage(ImagePaths.Undo); } }
		public static ImageSource Warning { get { return GetSvgImage(ImagePaths.Warning); } }

		static ImageSource GetImage(string path)
		{
			return new BitmapImage(new Uri(path, UriKind.Relative));
		}

		static ImageSource GetSvgImage(string path)
		{
			var svgImageSource = new SvgImageSourceExtension() { Uri = new Uri(path) }.ProvideValue(null);
			return (ImageSource)svgImageSource;
		}
	}

	public static class ImagePaths
	{
		public const string About = folderSvg + "About.svg";
		public const string CloseSolution = folderSvg + "CloseSolution.svg";
		public const string Copy = folderSvg + "Copy.svg";
		public const string Cut = folderSvg + "Cut.svg";
		public const string DevExpressLogo = folder + "DevExpressLogo.png";
		public const string Error = folderSvg + "Error.svg";
		public const string File = folderSvg + "File.svg";
		public const string FileCS = folderSvg + "FileCS.svg";
		public const string FileXaml = folderSvg + "FileXaml.svg";
		public const string Find = folderSvg + "Find.svg";
		public const string FindInFiles = folderSvg + "FindInFiles.svg";
		public const string FindInFilesWindow = folderSvg + "FindInFilesWindow.svg";
		public const string Info = folderSvg + "Info.svg";
		public const string LoadLayout = folderSvg + "LoadLayout.svg";
		public const string NewProject = folderSvg + "NewProject.svg";
		public const string OpenFile = folderSvg + "OpenFile.svg";
		public const string OpenSolution = folderSvg + "OpenSolution.svg";
		public const string Output = folderSvg + "Output.svg";
		public const string Paste = folderSvg + "Paste.svg";
		public const string PropertiesWindow = folderSvg + "PropertiesWindow.svg";
		public const string Redo = folderSvg + "Redo.svg";
		public const string References = folderSvg + "References.svg";
		public const string Refresh = folderSvg + "Refresh.svg";
		public const string Replace = folderSvg + "Replace.svg";
		public const string Run = folderSvg + "Run.svg";
		public const string Save = folderSvg + "Save.svg";
		public const string SaveAll = folderSvg + "SaveAll.svg";
		public const string SaveLayout = folderSvg + "SaveLayout.svg";
		public const string ShowAllFiles = folderSvg + "ShowAllFiles.svg";
		public const string SolutionExplorer = folderSvg + "SolutionExplorer.svg";
		public const string TaskList = folderSvg + "TaskList.svg";
		public const string Toolbox = folderSvg + "Toolbox.svg";
		public const string Undo = folderSvg + "Undo.svg";
		public const string Warning = folderSvg + "Warning.svg";
		const string folderSvg = "pack://application:,,,/VisualStudioDocking;component/Images/Docking/";
		const string folder = "/VisualStudioDocking;component/Images/Docking/";
	}




}
