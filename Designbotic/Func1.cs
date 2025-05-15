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
    [Transaction(TransactionMode.Manual)]
    public class Element_ID : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // 1. Zaznaczenie wielu element�w w widoku > ENTER > pojawienie si� listy ID, nazwy, Kategorii, materia�ow
            // 2. Wy�wietlone w komunikacie teksowym a wiec taskdialog

            IList<Reference> zaznaczoneElementy;
            try
            {
                zaznaczoneElementy = uidoc.Selection.PickObjects(ObjectType.Element, "Wybierz elementy");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }

            // Zmienne do przechowywania informacji o elementach
            string parametry = "";

            // Przejscie po szystkich zaznaoczonych elementach
            foreach (Reference r in zaznaczoneElementy)
            {
                Element element = doc.GetElement(r);
                // Sprawdzenie czy element istnieje
                if (element == null)
                {
                    TaskDialog.Show("B��d", "Element nie istnieje");
                    continue;
                }

                string id = element.Id.ToString(); // zawsze istnieje
                string name = element.Name; // zawsze isntieje

                string category; // moze nie istnie�
                if (element.Category != null && element.Category.Name != null)
                {
                    category = element.Category.Name;
                }
                else
                {
                    category = "Brak kategorii";
                }

                // Tworze liste materia��w
                var materials = new List<string>();
                foreach (Parameter param in element.Parameters)
                {
                    if (param.StorageType == StorageType.ElementId && param.Definition.Name.ToLower().Contains("material"))
                    {
                        ElementId matId = param.AsElementId();
                        Material mat = doc.GetElement(matId) as Material;
                        if (mat != null)
                        {
                            materials.Add(mat.Name);
                        }
                    }
                }
                // Tworze liste materia��w bo mo�e by� ich wiele
                string materialList = materials.Count > 0 ? string.Join(", ", materials) : "Brak materia��w";

                //Wype�niam parametry
                parametry += $"ID: {id}\n";
                parametry += $"Nazwa: {name}\n";
                parametry += $"Kategoria: {category}\n";
                parametry += $"Materia�y: {materialList}\n";
                parametry += "-------------------------------\n"; // odst�p

            }
            TaskDialog.Show("parametry element�w", parametry);
            return Result.Succeeded;

        }
    }
}