using BetterER.Controller;
using BetterER.Controller.Contracts;
using BetterER.Models;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;

namespace BetterER.ViewModels
{
    public class DiagramToScriptViewModel : BaseViewModel
    {
        private ISQLController _sqlController;
        private IMySQLController _mysqlController;

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

        public DiagramToScriptViewModel(string title) : base(title)
        {
            _sqlController = new SQLController();
            _mysqlController = new MySQLController();

            DatabaseTypes = new List<string>();
            DatabaseTypes.Add("SQL");
            DatabaseTypes.Add("MSSQL");
            DatabaseTypes.Add("MySQL");
            DatabaseTypes.Add("SQLite");


            ScriptDocument = new TextDocument();
            SelectedDatabaseType = "SQL";
        }

        private void DatabaseTypeChanged()
        {
            SetExampleScript(SelectedDatabaseType);
        }

        private void SetExampleScript(string databaseType)
        {
            var basicEntity = new BasicEntity
            {
                Name = "Example"
            };
            basicEntity.Attributes.Add(new BasicAttribute
            {
                Name = "Column1",
                DataType = "varchar",
                DataTypeModifier = "max",
                Default = "",
                IsPrimary = false,
                NotNull = false
            });
            basicEntity.Attributes.Add(new BasicAttribute
            {
                Name = "Column2",
                DataType = "varchar",
                DataTypeModifier = "50",
                Default = "Col2",
                IsPrimary = false,
                NotNull = true
            });
            basicEntity.Attributes.Add(new BasicAttribute
            {
                Name = "Id",
                DataType = "varchar",
                DataTypeModifier = "50",
                Default = "",
                IsPrimary = true,
                NotNull = true
            });

            string script = string.Empty;
            if (databaseType == "MSSQL")
                script = _sqlController.CreateCompleteScriptForMsSQL(basicEntity);
            else if (databaseType == "SQL")
                script = _sqlController.CreateTableScriptWithFullAttributes(basicEntity);
            else if (databaseType == "SQLite")
                script = _sqlController.CreateCompleteScriptForSQLite(basicEntity);
            if (ScriptDocument != null)
                ScriptDocument.Text = script;
        }

    }
}
