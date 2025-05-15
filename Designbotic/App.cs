#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using System.IO;

#endregion

namespace Designbotic
{

    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            // Ribbon
            String tabName = "Designbotic";
            a.CreateRibbonTab(tabName);

            // Panel
            RibbonPanel panel1 = a.CreateRibbonPanel(tabName, "Element Identification");

            // 1 button
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData button1 = new PushButtonData("Button1", "Show ID's", thisAssemblyPath, "Designbotic.Element_ID");
            PushButtonData button2 = new PushButtonData("Button2", "Show ID's + WPF", thisAssemblyPath, "Designbotic.Element_ID_WPF");


            // Add buttons to the panel
            PushButton pb1 = panel1.AddItem(button1) as PushButton;
            PushButton pb2 = panel1.AddItem(button2) as PushButton;

            pb1.LargeImage = LoadImage("Designbotic;component/Resources/icon.png"); // Dwa razy to samo, szkoda czasu naszukanie icon ktore beda dobrze wyswietlone
            pb2.LargeImage = LoadImage("Designbotic;component/Resources/icon.png");

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
        private BitmapImage LoadImage(string relativePath)
        {
            try
            {
                Uri uri = new Uri($"pack://application:,,,/{relativePath}", UriKind.Absolute);
                return new BitmapImage(uri);
            }
            catch
            {
                TaskDialog.Show("Error", $"Failed to load embedded image: {relativePath}");
                return null;
            }
        }
    }
}
