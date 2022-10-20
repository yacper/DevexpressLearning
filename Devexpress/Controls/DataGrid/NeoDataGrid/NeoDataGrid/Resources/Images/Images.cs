/********************************************************************
    created:	2022/2/9 17:26:31
    author:		rush
    email:		yacper@gmail.com	
	
    purpose:
    modifiers:	
*********************************************************************/
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NeoTrader{
    public static class ImagePaths
    {
        const string folderSvg = "pack://application:,,,/Resources/Images/Docking/";
        const string folder = "/NeoDataGrid;Resources/Images/Docking/";
        const string DxFloder = "pack://application:,,,/DevExpress.Images.v22.1;component/";
        const string RootFloder = "/NeoDataGrid;Resources/Images/";
        const string ImageFloder = "pack://application:,,,/NeoDataGrid;Resources/Images/";

        //
        public const string Watchlist = $"{ImageFloder}Panel/Watchlist.svg";
        public const string Monitor = $"{ImageFloder}Panel/Monitor.svg";
        public const string Logger = $"{ImageFloder}Panel/Logger.svg";
        public const string Trading = $"{ImageFloder}Panel/Trading.svg";
        public const string Account = $"{ImageFloder}Panel/Account.svg";
        public const string AlertWindow = ImageFloder + "Panel/Alert.svg";

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
        public const string Time = folderSvg + "Time.svg";
        public const string Logo = folder + "Logo.png";
        public const string Icon = folder + "Icon.png";
        public const string Setting = DxFloder + "SvgImages/Icon Builder/Actions_Settings.svg";
        public const string ConnectedStatus = folderSvg + "ConnectedStatus.svg";
        public const string ConnectingStatus = folderSvg + "ConnectingStatus.svg";
        public const string DisConnectedStatus = DxFloder + "SvgImages/XAF/Action_Debug_Breakpoint_Toggle.svg";
        public const string Zoom = DxFloder + "SvgImages/Icon Builder/Actions_Zoom.svg";
        public const string FullScreenExit = DxFloder + "SvgImages/Icon Builder/Actions_FullScreenExit.svg";        
        public const string SortAsc = DxFloder + "SvgImages/XAF/Action_Sorting_Ascending.svg";
        public const string SortDsec = DxFloder + "SvgImages/XAF/Action_Sorting_Descending.svg";
        public const string Attributy = RootFloder + "Monitor/Attributy.png";
        public const string Enum = RootFloder + "Monitor/Enum.png";
        public const string Field = RootFloder + "Monitor/Field.png";
        public const string Codes = DxFloder + "SvgImages/RichEdit/ToggleFieldCodes.svg";
        public const string AppointmentNightClock = DxFloder + "SvgImages/Scheduling/AppointmentNightClock.svg";
        public const string Start = DxFloder + "SvgImages/XAF/Action_Debug_Start.svg";
        public const string Stop = DxFloder + "SvgImages/XAF/Action_Debug_Stop.svg";
        public const string Eye = folderSvg + "Eye.svg";
        public const string EyeClose = folderSvg + "EyeClose.svg";
        public const string HorizontalMore = folderSvg + "HorizontalMore.svg";
        public const string Close = DxFloder + "SvgImages/HybridDemoIcons/BottomPanel/HybridDemo_Close.svg";
        public const string Position = folderSvg + "Position.svg";
        public const string Link = DxFloder + "SvgImages/Icon Builder/Actions_Hyperlink.svg";
        public const string AllProvider = folderSvg + "AllProvider.svg";
        public const string Smtp = folderSvg + "Smtp.svg";

        public const string Ruler = folderSvg + "Ruler.svg";
        public const string HorizontalLine = folderSvg + "HorizontalLine.svg";
        public const string VerticalLine = folderSvg + "VerticalLine.svg";
        public const string TrendLine = folderSvg + "TrendLine.svg";
        public const string Text = folderSvg + "Text.svg";
        public const string StaticText = folderSvg + "StaticText.svg";
        public const string DrawIcon = folderSvg + "Icon.svg";
        public const string FibonacciRetracement = folderSvg + "FibonacciRetracement.svg";
        public const string FibonacciExpansion = folderSvg + "FibonacciExpansion.svg";
        public const string FibonacciFan = folderSvg + "FibonacciFan.svg";
        public const string AndrewsPitchfork = folderSvg + "AndrewsPitchfork.svg";
        public const string Rectangle = folderSvg + "Rectangle.svg";
        public const string Ellipse = folderSvg + "Ellipse.svg";
        public const string Triangle = folderSvg + "Triangle.svg";
        public const string EquidistantChannel = folderSvg + "EquidistantChannel.svg"; 
        public const string Cross = folderSvg + "Cross.svg"; 
        public const string Export = folderSvg + "Export.svg";
        public const string Clear = folderSvg + "Clear.svg";

        public const string Build = folderSvg + "Build.svg";
        public const string BuildTip = folderSvg + "BuildTip.svg";
        public const string VerticalMore = folderSvg + "VerticalMode.svg";
        public const string SystemTip = folderSvg + "SystemTip.svg";

    }

    public static class Images
    {
        public static ImageSource Watchlist { get { return GetSvgImage(ImagePaths.Watchlist); } }
        public static ImageSource Monitor { get { return GetSvgImage(ImagePaths.Monitor); } }
        public static ImageSource Logger { get { return GetSvgImage(ImagePaths.Logger); } }
        public static ImageSource Trading { get { return GetSvgImage(ImagePaths.Trading); } }
        public static ImageSource Account { get { return GetSvgImage(ImagePaths.Account); } }


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
        public static ImageSource Time { get { return GetSvgImage(ImagePaths.Time); } }
        public static ImageSource Icon { get { return GetImage(ImagePaths.Icon); } }
        public static ImageSource Logo { get { return GetImage(ImagePaths.Logo); } }
        public static ImageSource Setting { get { return GetSvgImage(ImagePaths.Setting); } }
        public static ImageSource ConnectedStatus { get { return GetSvgImage(ImagePaths.ConnectedStatus); } }
        public static ImageSource ConnectingStatus { get { return GetSvgImage(ImagePaths.ConnectingStatus); } }
        public static ImageSource DisConnectedStatus { get { return GetSvgImage(ImagePaths.DisConnectedStatus); } }
        public static ImageSource Zoom { get { return GetSvgImage(ImagePaths.Zoom); } }
        public static ImageSource FullScreenExit { get { return GetSvgImage(ImagePaths.FullScreenExit); } }        
        public static ImageSource SortAsc { get { return GetSvgImage(ImagePaths.SortAsc); } }
        public static ImageSource SortDsec { get { return GetSvgImage(ImagePaths.SortDsec); } }
        public static ImageSource Attributy { get { return GetImage(ImagePaths.Attributy); } }
        public static ImageSource Enum { get { return GetImage(ImagePaths.Enum); } }
        public static ImageSource Field { get { return GetImage(ImagePaths.Field); } }
        public static ImageSource Codes { get { return GetSvgImage(ImagePaths.Codes); } }
        public static ImageSource AppointmentNightClock { get { return GetSvgImage(ImagePaths.AppointmentNightClock); } }
        public static ImageSource Start { get { return GetSvgImage(ImagePaths.Start);} }
        public static ImageSource Stop { get { return GetSvgImage(ImagePaths.Stop); } }
        public static ImageSource Eye { get { return GetSvgImage(ImagePaths.Eye); } }
        public static ImageSource EyeClose { get { return GetSvgImage(ImagePaths.EyeClose); } }
        public static ImageSource HorizontalMore { get { return GetSvgImage(ImagePaths.HorizontalMore); } }
        public static ImageSource Close { get { return GetSvgImage(ImagePaths.Close); } }
        public static ImageSource Position { get { return GetSvgImage(ImagePaths.Position); } }
        public static ImageSource Link { get { return GetSvgImage(ImagePaths.Link); } }
        public static ImageSource AllProvider { get { return GetSvgImage(ImagePaths.AllProvider); } }
        public static ImageSource Smtp { get { return GetSvgImage(ImagePaths.Smtp); } }
        public static ImageSource Ruler { get { return GetSvgImage(ImagePaths.Ruler); } }
        public static ImageSource HorizontalLine { get { return GetSvgImage(ImagePaths.HorizontalLine); } }
        public static ImageSource VerticalLine { get { return GetSvgImage(ImagePaths.VerticalLine); } }
        public static ImageSource TrendLine { get { return GetSvgImage(ImagePaths.TrendLine); } }
        public static ImageSource Text { get { return GetSvgImage(ImagePaths.Text); } }
        public static ImageSource StaticText { get { return GetSvgImage(ImagePaths.StaticText); } }
        public static ImageSource DrawIcon { get { return GetSvgImage(ImagePaths.DrawIcon); } }
        public static ImageSource FibonacciRetracement { get { return GetSvgImage(ImagePaths.FibonacciRetracement); } }
        public static ImageSource FibonacciExpansion { get { return GetSvgImage(ImagePaths.FibonacciExpansion); } }
        public static ImageSource FibonacciFan { get { return GetSvgImage(ImagePaths.FibonacciFan); } }
        public static ImageSource AndrewsPitchfork { get { return GetSvgImage(ImagePaths.AndrewsPitchfork); } }
        public static ImageSource Rectangle { get { return GetSvgImage(ImagePaths.Rectangle); } }
        public static ImageSource Ellipse { get { return GetSvgImage(ImagePaths.Ellipse); } }
        public static ImageSource Triangle { get { return GetSvgImage(ImagePaths.Triangle); } }
        public static ImageSource EquidistantChannel { get { return GetSvgImage(ImagePaths.EquidistantChannel); } }
        public static ImageSource Cross { get { return GetSvgImage(ImagePaths.Cross); } }
        public static ImageSource AlertWindow { get { return GetSvgImage(ImagePaths.AlertWindow); } }
        public static ImageSource Export { get { return GetSvgImage(ImagePaths.Export); } }
        public static ImageSource Clear { get { return GetSvgImage(ImagePaths.Clear); } }
        public static ImageSource Build { get { return GetSvgImage(ImagePaths.Build); } }
        public static ImageSource BuildTip { get { return GetSvgImage(ImagePaths.BuildTip); } }
        public static ImageSource VerticalMore { get { return GetSvgImage(ImagePaths.VerticalMore); } }
        public static ImageSource SystemTip { get { return GetSvgImage(ImagePaths.SystemTip); } }
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
 
}
