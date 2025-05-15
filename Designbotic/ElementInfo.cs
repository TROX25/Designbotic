#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

#endregion

namespace Designbotic
{
    public class ElementInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public List<string> materials { get; set; }
    }
}