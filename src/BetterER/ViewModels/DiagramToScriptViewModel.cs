using BetterER.Controller;
using BetterER.Controller.Contracts;
using BetterER.Models;
using ICSharpCode.AvalonEdit.Document;
using System.Collections.Generic;

namespace BetterER.ViewModels
{
    public class DiagramToScriptViewModel : BaseViewModel
    {
        private ISQLController _sqlController;
        private IMySQLController _mysqlController;
        private ISQLiteController _sqliteController;

        private List<string> _databaseTypes;
        public List<string> DatabaseTypes
        {
            get { return _databaseTypes; }
            set { _databaseTypes = value; OnPropertyChanged(); }
        }

        private string _selectedDatabaseType;
        public string SelectedDatabaseType
        {
            get { return _selectedDatabaseType; }
            set { _selectedDatabaseType = value; OnPropertyChanged(); DatabaseTypeChanged(); }
        }

        private TextDocument _scriptDocument;
        public TextDocument ScriptDocument
        {
            get { return _scriptDocument; }
            set { _scriptDocument = value; OnPropertyChanged(); }
        }

        private BasicDiagram _basicDiagram;
        public BasicDiagram BasicDiagram
        {
            get { return _basicDiagram; }
            set { _basicDiagram = value; OnPropertyChanged(); }
        }


        public DiagramToScriptViewModel(string title) : base(title)
        {
            _sqlController = new SQLController();
            _mysqlController = new MySQLController();
            _sqliteController = new SQLiteController();

            ScriptDocument = new TextDocument();
            BasicDiagram = new BasicDiagram();

            DatabaseTypes = new List<string>();
            DatabaseTypes.Add("SQL");
            DatabaseTypes.Add("MSSQL");
            DatabaseTypes.Add("MySQL");
            DatabaseTypes.Add("SQLite");

            SelectedDatabaseType = "SQL";
        }

        private void DatabaseTypeChanged()
        {
            SetExampleScript();
        }

        private void SetExampleScript()
        {
            BasicDiagram = new BasicDiagram();

            var basicEntity1 = new BasicEntity() { Name = "Tabelle-1" };
            var basicEntity2 = new BasicEntity() { Name = "Tabelle-2" };

            basicEntity1.Attributes.Add(new BasicAttribute { Name = "Tb1-Id", DataType = "varchar", DataTypeModifier = "50", Default = "", IsPrimary = true, NotNull = true });
            basicEntity1.Attributes.Add(new BasicAttribute { Name = "Tb1-Name", DataType = "varchar", DataTypeModifier = "50", Default = "Ohne", IsPrimary = false, NotNull = true });
            basicEntity1.Attributes.Add(new BasicAttribute { Name = "Tb1-Alter", DataType = "varchar", DataTypeModifier = "50", Default = "", IsPrimary = false, NotNull = false });

            basicEntity2.Attributes.Add(new BasicAttribute { Name = "Tb2-Id", DataType = "varchar", DataTypeModifier = "50", Default = "", IsPrimary = true, NotNull = true });
            basicEntity2.Attributes.Add(new BasicAttribute { Name = "Tb2-Name", DataType = "varchar", DataTypeModifier = "50", Default = "Ohne", IsPrimary = false, NotNull = true });
            basicEntity2.Attributes.Add(new BasicAttribute { Name = "Tb2-Alter", DataType = "varchar", DataTypeModifier = "50", Default = "", IsPrimary = false, NotNull = false });

            BasicDiagram.BasicEntities.Add(basicEntity1);
            BasicDiagram.BasicEntities.Add(basicEntity2);

            string script = string.Empty;
            if (SelectedDatabaseType == "MSSQL")
                script = _sqlController.CreateCompleteScriptForMsSQL(BasicDiagram);
            else if (SelectedDatabaseType == "SQL")
                script = _sqlController.CreateCompleteScriptForSQL(BasicDiagram);
            else if (SelectedDatabaseType == "SQLite")
                script = _sqliteController.CreateCompleteScriptForSQLite(BasicDiagram);
            if (ScriptDocument != null)
                ScriptDocument.Text = script;
        }
    }
}
