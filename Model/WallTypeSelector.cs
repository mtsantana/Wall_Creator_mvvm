using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

namespace SecondTry
{
    public class WallTypeSelector
    {
        Document Doc;

        public WallTypeSelector(Document document)
        {
            Doc = document;
        }

        public List<String> WallTypes()
        {
            List<string> types = new List<string>();

            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            List<Element> result = (List<Element>)new FilteredElementCollector(Doc)
                .WherePasses(filter).WhereElementIsElementType().ToElements();

            foreach(Element e in result)
            {
                types.Add(e.Name);
            }

            return types;
        }

        public ElementId WallSelected()
        {

            ElementId selectedWall = null;

            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            List<Element> result = (List<Element>)new FilteredElementCollector(Doc)
                .WherePasses(filter).WhereElementIsElementType().ToElements();

            foreach (Element e in result)
            {
                if (e.Name != "Curtain Wall")
                {
                    selectedWall = e.Id;
                    break;
                }
                else
                {
                    selectedWall = result[0].Id;
                }
            }
            return selectedWall;
        }
    }
}
