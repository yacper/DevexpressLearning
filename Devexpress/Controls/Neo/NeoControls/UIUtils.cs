/********************************************************************
    created:	2017/11/15 10:18:32
    author:	rush
    email:		
	
    purpose:	
*********************************************************************/
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace NeoTrader.UiUtils
{
    public static class UIUtils
    {
        public static void SetLostFocus(FrameworkElement frameworkElement)
        {
            FrameworkElement parent = (FrameworkElement)frameworkElement.Parent;
            while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
            {
                parent = (FrameworkElement)parent.Parent;
            }

            DependencyObject scope = FocusManager.GetFocusScope(frameworkElement);
            FocusManager.SetFocusedElement(scope, parent as IInputElement);
        }


        public static List<T> GetChildObjects<T>(DependencyObject obj, Type typename) where T : FrameworkElement //根据控件类型查找子控件，返回列表List<Button> listButtons = GetChildObjects<Button>(parentPanel, typeof(Button));
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).GetType() == typename))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, typename));
            }
            return childList;
        }

        public static List<T> GetChildObjectsEnd<T>(DependencyObject obj, Type typename) where T : FrameworkElement //根据控件类型查找子控件，返回列表List<Button> listButtons = GetChildObjects<Button>(parentPanel, typeof(Button));
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T)
                {
                    childList.Add((T)child);
                }
                else if (child.GetType() == typename)
                {
                    continue;
                }
                childList.AddRange(GetChildObjectsEnd<T>(child, typename));
            }
            return childList;
        }

        public static T GetChildObject<T>(DependencyObject obj, Type typename) where T : FrameworkElement //根据控件类型查找一级子控件，返回第一个符合条件的Button button = GetChildObject<Button>(parentPanel, typeof(Button));
        {
            DependencyObject child = null;
            T grandChild = null;
            for (int i = 0; i <  VisualTreeHelper.GetChildrenCount(obj) ; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).GetType() == typename))
                {
                    return (T)child;
                }
                grandChild = GetChildObject<T>(child, typename);
                if (grandChild != null) return grandChild;
            }
            return null;
        }
        public static T GetChildObject<T>(DependencyObject obj) where T : FrameworkElement //根据控件类型查找一级子控件，返回第一个符合条件的Button button = GetChildObject<Button>(parentPanel, typeof(Button));
        {
            DependencyObject child = null;
            T grandChild = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T)
                {
                    return (T)child;
                }
                grandChild = GetChildObject<T>(child);
                if (grandChild != null) return grandChild;
            }
            return null;
        }

        public static List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement  // List<Button> listButtons = GetChildObjects<Button>(parentPanel, "button1");
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, name));
            }
            return childList;
        }


        public static T     GetChildObject<T>(this DependencyObject obj, string name) where T : FrameworkElement // StackPanel sp = GetChildObject<StackPanel>(this.LayoutRoot, "spDemoPanel");
        {
            DependencyObject child = null;
            T grandChild = null;

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
            return null;
        }

        public static T GetParentObject<T>(DependencyObject obj, string name) where T : FrameworkElement  // Grid layoutGrid = VTHelper.GetParentObject<Grid>(this.spDemoPanel, "LayoutRoot"); 
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T && (((T)parent).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }


        public static T GetParentObject<T>(DependencyObject obj) where T : FrameworkElement  
            // Grid layoutGrid = VTHelper.GetParentObject<Grid>(this.spDemoPanel); 
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
                }
            return null;
        }

        public static T GetElementUnderMouse<T>() where T : DependencyObject   //根据鼠标位置查找控件  ListViewItem lvi = GetElementUnderMouse<ListViewItem>();
        {
            return FindVisualParent<T>(Mouse.DirectlyOver as DependencyObject);
        }
        public static T FindVisualParent<T>(DependencyObject element) where T : DependencyObject //
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }
                parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
            }
            return null;
        }


        public static T GetElementUnderMouse<T>(Type overType) where T : DependencyObject   //根据鼠标位置查找控件  ListViewItem lvi = GetElementUnderMouse<ListViewItem>();
        {
            return FindVisualParent<T>(Mouse.DirectlyOver as DependencyObject, overType);
        }
        public static T FindVisualParent<T>(DependencyObject element, Type overType) where T : DependencyObject //
        {
            DependencyObject parent = element;
            while (parent != null && parent.GetType()!= overType)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }
                parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
            }
            return null;
        }


        public static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject  //ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);    (DependencyObject)e.OriginalSource可以是任意子控件，返回第一个符合类型的父控件,可能会返回模版中如grid等控件，请注意。
        {
            bool first = true;
            do
            {
                if (current is T && !first)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
                first = false;
            }
            while (current != null);
            return null;
        }
        public static void  EnumLogicalTree(int Ident, object logObj)    //遍历逻辑树
        {
            if (!(logObj is DependencyObject))//对象必须派生自DependencyObject对象
                return;

            foreach (object childLogical in LogicalTreeHelper.GetChildren(logObj as DependencyObject))
            {
                Console.WriteLine(new string(' ', Ident) + childLogical);

                EnumLogicalTree(Ident + 1, childLogical);
            }
        }



        public static void  EnumVisualTree(int Ident, Visual visualObj)       //遍历视觉树
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visualObj); i++)
            { 
                //接收特定索引的子元素
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(visualObj, i);
                Console.WriteLine(new string(' ', Ident) + childVisual);

                EnumVisualTree(Ident + 1, childVisual);
            }
        }


        /// <summary>
        /// 鼠标位置可通过Point point = PointToScreen(Mouse.GetPosition(menu))获取;
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="mousePoint"></param>
        /// <returns></returns>
        public static bool IsMouseOverForContextMenu(ContextMenu menu, Point mousePoint)   //鼠标是否在contextmenu内点击
        {
            if (menu.IsOpen)
            {
                Point menuPostion = menu.PointToScreen(new Point());
                double menuWidth = menu.ActualWidth;
                double menuHeight = menu.ActualHeight;
                if (mousePoint.X >= menuPostion.X
                    && mousePoint.X <= menuPostion.X + menuWidth
                    && mousePoint.Y >= menuPostion.Y
                    && mousePoint.Y <= menuPostion.Y + menuHeight)
                {
                    return true;
                }
                foreach (var item in menu.Items)
                {
                    if (item is MenuItem && IsMouseOverForMenuItem((MenuItem)item, mousePoint))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsMouseOverForMenuItem(MenuItem item, Point mousePoint)
        {
            if (item.IsSubmenuOpen)
            {
                Popup p = item.Template.FindName("PART_Popup", item) as Popup;
                if (p != null)
                {
                    FrameworkElement childui = p.Child as FrameworkElement;
                    Point childuiPostion = childui.PointToScreen(new Point());
                    double childuiWidth = childui.ActualWidth;
                    double childuiHeight = childui.ActualHeight;
                    if (mousePoint.X >= childuiPostion.X
                        && mousePoint.X <= childuiPostion.X + childuiWidth
                        && mousePoint.Y >= childuiPostion.Y
                        && mousePoint.Y <= childuiPostion.Y + childuiHeight)
                    {
                        return true;
                    }
                }
                foreach (var childitem in item.Items)
                {
                    if (childitem is MenuItem && IsMouseOverForMenuItem((MenuItem)childitem, mousePoint))
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public static bool IsMouseOverForControl(FrameworkElement control, Point mousePoint)   //鼠标是否在contextmenu内点击
        {
            if (control.IsVisible)
            {
                Point controlPostion = control.PointToScreen(new Point());
                double menuWidth = control.ActualWidth;
                double menuHeight = control.ActualHeight;
                if (mousePoint.X >= controlPostion.X
                    && mousePoint.X <= controlPostion.X + menuWidth
                    && mousePoint.Y >= controlPostion.Y
                    && mousePoint.Y <= controlPostion.Y + menuHeight)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCtrlPressed()
        {
            return ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control);
        }


        public static bool IsShiftPressed()
        {
            return ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift);
        }
        public static bool IsAltPressed()
        {
            return ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt);
        }



        public static T GetElementUnderMouseWithNoFocusableElement<T>() where T : DependencyObject   
        //根据鼠标位置查找控件 ，并且不能为可以被focus的控件，常用于拖拽时调用，可focus的控件常常有其他事件触发，所以需要避开
        {
            return FindVisualParentWithNoFocusableElement<T>(Mouse.DirectlyOver as DependencyObject);
        }


        //根据鼠标位置查找控件 ，并且不能为可以被focus的控件，常用于拖拽时调用，可focus的控件常常有其他事件触发，所以需要避开
        public static T GetElementUnderMouseWithNoFocusableElement<T>(ref DependencyObject mousedirectlyover) where T : DependencyObject
        {
            mousedirectlyover = Mouse.DirectlyOver as DependencyObject;
            return FindVisualParentWithNoFocusableElement<T>(mousedirectlyover);
        }

        public static T FindVisualParentWithNoFocusableElement<T>(DependencyObject element) where T : DependencyObject //
        {
            DependencyObject parent = element;
            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }
                //如果不是当前需要的控件则判断此控件是否可以被Focus
                if (parent is FrameworkElement && (parent as FrameworkElement).Focusable)
                    return null;
                //否则就继续遍历
                parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
            }
            return null;
        }



        //控件和当前鼠标下控件间是否有可focusable的控件，一般target也是需要当前鼠标悬停状态
        public static bool IsUnderMouseElementWithFocusableElement(UIElement target)
        {
            UIElement mousedirectlyover = Mouse.DirectlyOver as UIElement;
            return IsBetweenWithNoFocusableElement(target, mousedirectlyover);
        }


        //soure到target之间是否有可focusable的控件，一般target是父控件，souce是子控件
        public static bool IsBetweenWithNoFocusableElement(UIElement target, UIElement source)
        {
            UIElement parent = source;
            while (parent != null)
            {
                if (parent == target)
                    return true;
                if (parent.Focusable)
                    return false;
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return true;
        }

        //控件和当前鼠标下控件间是否有可focusable的控件，一般target也是需要当前鼠标悬停状态
        public static bool IsUnderMouseElementWithFocusableElement(UIElement target, List<Type> types)
        {
            UIElement mousedirectlyover = Mouse.DirectlyOver as UIElement;
            return IsBetweenWithNoFocusableElement(target, mousedirectlyover, types);
        }


        //soure到target之间是否有可focusable的控件，一般target是父控件，souce是子控件
        public static bool IsBetweenWithNoFocusableElement(UIElement target, UIElement source, List<Type> types)
        {
            UIElement parent = source;
            while (parent != null)
            {
                if (parent == target)
                    return true;
                if (parent.Focusable)
                {
                    Type type = parent.GetType();
                    bool iscontain = false;
                    foreach (var type1 in types)
                    {
                        if (type1 == type)
                        {
                            iscontain = true;
                            break;
                        }
                    }
                    if (!iscontain)
                        return false;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return true;
        }


        //控件和当前鼠标下控件间是否有可focusable的控件，一般target也是需要当前鼠标悬停状态
        public static bool IsUnderMouseElementWithFocusableElement(UIElement target, Type type)
        {
            UIElement mousedirectlyover = Mouse.DirectlyOver as UIElement;
            return IsBetweenWithNoFocusableElement(target, mousedirectlyover, type);
        }


        //soure到target之间是否有可focusable的控件，一般target是父控件，souce是子控件
        public static bool IsBetweenWithNoFocusableElement(UIElement target, UIElement source, Type type)
        {
            UIElement parent = source;
            while (parent != null)
            {
                if (parent == target)
                    return true;
                if (parent.Focusable)
                {
                    Type ptype = parent.GetType();
                    if (ptype != type)
                        return false;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return true;
        }


        public static double GetTextDisplayWidth(string str)
        {
            return GetTextDisplayWidth(str, S_FontFamily, S_FontStyle, S_FontWeight, S_FontStretch, (double)(Application.Current.FindResource("DefaultNormalFontSize")));
        }

        public static double GetTextDisplayWidth(string str, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double FontSize = 11)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;

            var formattedText = new FormattedText(
                str,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                FontSize,
                Brushes.Black
            );
            return formattedText.Width;
        }

        public static Size GetTextDisplaySize(string str, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double FontSize = 11)
        {
            if (string.IsNullOrWhiteSpace(str))
                return Size.Empty;

            var formattedText = new FormattedText(
                str,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                FontSize,
                Brushes.Black
            );

            return new Size(formattedText.Width, formattedText.Height);
        }

        public static FontFamily S_FontFamily = new FontFamily("微软雅黑");
        public static FontStyle S_FontStyle = FontStyles.Normal;
        public static FontWeight S_FontWeight = FontWeights.Normal;
        public static FontStretch S_FontStretch = FontStretches.Normal;


        //输入法焦点问题，静态方法，给Popup设置输入法焦点
        [DllImport("User32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

    }

}
