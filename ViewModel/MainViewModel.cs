using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Windows.Input;
using SecondTry.ViewModel;
using System.Security.Cryptography;

namespace SecondTry
{
    partial class MainViewModel : BasicViewModel
    {
        #region Properties

        private Document Doc { get; }
        private ElementId LevelId { get; }


        public UserControl1 userControl;

        public UserControl1 UserControl
        {
            get
            {
                if (userControl == null)
                {
                    userControl = new UserControl1() { DataContext = this };
                }
                return userControl;
            }
            set
            {
                userControl = value;
                OnPropertyChanged(nameof(UserControl));
            }
        }


        public ElementId wallSelected;

        public ElementId WallSelected
        {
            get
            {
                return wallSelected;
            }
            set
            {
                wallSelected= value;
                OnPropertyChanged(nameof(WallSelected));
            }
        }


        public double wallHeight = 3.00;
        public double WallHeight
        {
            get
            {
                return wallHeight;
            }
            set
            {
                wallHeight = value;
                OnPropertyChanged(nameof(WallHeight));
            }
        }


        public List<string> wallList = new List<string>();

        public List<string> WallList
        {
            get
            {
                return wallList;
            }
            set
            {
                wallList= value;
                OnPropertyChanged(nameof(WallList));
            }
        }


        public ICommand OnClick { get; set; }

        #endregion

        #region Constructors

        public MainViewModel()
        {

        }

        public MainViewModel(Document doc, ElementId levelId)
        {
            Doc = doc;
            LevelId = levelId;
            WallList = new WallTypeSelector(doc).WallTypes();
            OnClick = new RelayCommand (CreateWalls);

        }
        #endregion

        private void CreateWalls()
        {
            wallSelected = new WallTypeSelector(Doc).WallSelected();

            List<Curve> curves = new List<Curve>();
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Lines);
            List<Element> result = (List<Element>)new FilteredElementCollector(Doc)
                .WherePasses(filter).WhereElementIsNotElementType().ToElements();

            Transaction t = new Transaction(Doc);
            t.Start("Walls Created");
            foreach (Element e in result)
            {
                CurveElement c = e as CurveElement;
                XYZ StarPoint = c.GeometryCurve.GetEndPoint(0);
                XYZ EndPoint = c.GeometryCurve.GetEndPoint(1);
                Curve lineBound = Line.CreateBound(StarPoint, EndPoint) as Curve;
                Wall.Create(Doc, lineBound, wallSelected, LevelId, (wallHeight * 3.280839), 0, false, false);
            }
            t.Commit();
        }
    }
}
