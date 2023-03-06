using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;

namespace SecondTry
{
    [Transaction(TransactionMode.Manual)]

    public class Main: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            ElementId levelId = doc.ActiveView.GenLevel.Id;

            MainViewModel vm = new MainViewModel(doc, levelId);
            vm.UserControl.ShowDialog();


            return Result.Succeeded;
        }
    }
}
