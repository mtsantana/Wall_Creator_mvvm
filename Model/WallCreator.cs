using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;

namespace SecondTry.Model
{
    [Transaction(TransactionMode.Manual)]
    public class WallCreator : IExternalCommand
    {
        List<Curve> curves = new List<Curve>();
        ElementId wallId;
        ElementId levelId;
        double wallHeight;


        public WallCreator()
        {

        }

        public WallCreator(List<Curve> curves, ElementId wallId, ElementId levelId, double wallHeight)
        {
            this.curves = curves;
            this.wallId = wallId;
            this.levelId = levelId;
            this.wallHeight = wallHeight;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            Transaction t = new Transaction(doc);
            t.Start();

            foreach (Curve curve in curves)
            {
                Wall.Create(doc, curve, wallId, levelId, wallHeight, 0, false, false);
            }

            t.Commit();
            return Result.Succeeded;
        }
    }
}
