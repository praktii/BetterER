using System;
using System.IO;
using System.Reflection;
using System.Windows;
using BetterER.Controller;
using BetterER.Controller.Contracts;
using BetterER.MVVM;

namespace BetterER.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IWindowController _windowController;

        private bool _diagramSaved;
        public bool DiagramSaved
        {
            get => _diagramSaved;
            set { _diagramSaved = value; OnPropertyChanged(); }
        }

        private bool _diagramOpen;
        public bool DiagramOpen
        {
            get => _diagramOpen;
            set { _diagramOpen = value; OnPropertyChanged(); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; OnPropertyChanged(); }
        }


        #region Commands
        public RelayCommand ExitApplicationCommand { get; }
        public RelayCommand CloseDiagramCommand { get; }
        public RelayCommand SaveDiagramCommand { get; }
        public RelayCommand SaveDiagramUnderCommand { get; }
        public RelayCommand ExportDiagramCommand { get; }
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }
        public RelayCommand CopyCommand { get; }
        public RelayCommand PasteCommand { get; }
        public RelayCommand CutCommand { get; }
        public RelayCommand SelectAllCommand { get; }
        public RelayCommand GenerateSqlScriptCommand { get; }
        public RelayCommand PdfExportCommand { get; }
        public RelayCommand PngExportCommand { get; }
        public RelayCommand ErrorAnalysisCommand { get; }
        public RelayCommand OpenSettingsCommand { get; }
        public RelayCommand OpenHelpfileCommand { get; }
        public RelayCommand ReportErrorCommand { get; }
        public RelayCommand ShowAboutCommand { get; }
        public RelayCommand OpenEditorWindowCommand { get; }

        #endregion

        public MainViewModel(string title) : base(title)
        {
            _windowController = new WindowController();

            ExitApplicationCommand = new RelayCommand(ExitApplication);
            CloseDiagramCommand = new RelayCommand(CloseDiagram, CanInteractWithDiagram);
            ExportDiagramCommand = new RelayCommand(ExportDiagram, CanInteractWithDiagram);
            SaveDiagramUnderCommand = new RelayCommand(SaveDiagramUnder, CanInteractWithDiagram);
            SaveDiagramCommand = new RelayCommand(SaveDiagram, CanInteractWithDiagram);
            CutCommand = new RelayCommand(Cut);
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
            CopyCommand = new RelayCommand(Copy);
            PasteCommand = new RelayCommand(Paste);
            SelectAllCommand = new RelayCommand(SelectAll);
            GenerateSqlScriptCommand = new RelayCommand(GenerateSQLScript);
            PdfExportCommand = new RelayCommand(PdfExport);
            PngExportCommand = new RelayCommand(PngExport);
            ErrorAnalysisCommand = new RelayCommand(ErrorAnalysis);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            OpenHelpfileCommand = new RelayCommand(OpenHelpfile);
            ReportErrorCommand = new RelayCommand(ReportError);
            ShowAboutCommand = new RelayCommand(ShowAbout);
            OpenEditorWindowCommand = new RelayCommand(OpenEditorWindow);

            DiagramSaved = false;
            DiagramOpen = false;

            FilePath = GetFilePathOfAssemlbyForTest();
        }

        private string GetFilePathOfAssemlbyForTest()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        private void ShowAbout()
        {
            _windowController.ShowAboutWindow();
        }

        private void ReportError()
        {
            _windowController.ShowReportErrorWindow();
        }

        private void OpenHelpfile()
        {
        }

        private void OpenSettings()
        {
            _windowController.ShowSettingsWindow();
        }

        private void GenerateSQLScript()
        {
            _windowController.ShowDiagramToScriptWindow();
        }

        private void PdfExport()
        {
        }

        private void PngExport()
        {
        }

        private void ErrorAnalysis()
        {
        }

        private void SelectAll()
        {
        }

        private void Paste()
        {
        }

        private void Cut()
        {
        }

        private void Copy()
        {
        }

        private void Redo()
        {
        }

        private void Undo()
        {
        }

        private void SaveDiagram()
        {
        }

        private void SaveDiagramUnder()
        {
        }

        private void ExportDiagram()
        {
        }

        private void CloseDiagram()
        {
        }

        private void ExitApplication()
        {
            if (!DiagramOpen)
                Application.Current.Shutdown();
            else
            {
                if (!DiagramSaved)
                    Application.Current.Shutdown();
            }
        }

        private bool CanInteractWithDiagram()
        {
            return DiagramOpen;
        }
    }
}
