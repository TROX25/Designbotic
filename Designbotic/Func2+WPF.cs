#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Newtonsoft.Json;
using WPF_DESIGNBOTIC;

#endregion

namespace Designbotic
{
    [Transaction(TransactionMode.Manual)]
    public class Element_ID_WPF : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> zaznaczoneElementy;
            try
            {
                zaznaczoneElementy = uidoc.Selection.PickObjects(ObjectType.Element, "Wybierz elementy");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }

            var listaElementow = new List<ElementInfo>();

            foreach (Reference r in zaznaczoneElementy)
            {
                Element element = doc.GetElement(r);
                if (element == null) continue;

                var materials = new List<string>();
                foreach (Parameter param in element.Parameters)
                {
                    if (param.StorageType == StorageType.ElementId && param.Definition.Name.ToLower().Contains("material"))
                    {
                        ElementId matId = param.AsElementId();
                        Material mat = doc.GetElement(matId) as Material;
                        if (mat != null)
                            materials.Add(mat.Name);
                    }
                }

                var elementInfo = new ElementInfo
                {
                    id = element.Id.ToString(),
                    name = element.Name,
                    category = element.Category?.Name ?? "Brak kategorii",
                    materials = materials.Count > 0 ? materials : new List<string> { "Brak materia³ów" }
                };
                // Dodaje element do listy
                listaElementow.Add(elementInfo);
            }

            // NOWA CZÊŒÆ
            // ================================================================================================================
            // Schemat JSON
            string json = JsonConvert.SerializeObject(listaElementow, Formatting.Indented);

            // przechodze do wpf
            MainWindow wpfWindow = new MainWindow(json);
            wpfWindow.ShowDialog();
            // ================================================================================================================
            return Result.Succeeded;
        }
    }
}