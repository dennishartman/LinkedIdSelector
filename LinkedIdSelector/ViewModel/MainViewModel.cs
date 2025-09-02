using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Autodesk.Revit.DB;
using LinkedIdSelector.Commands;
using LinkedIdSelector.ExternalEventsModeless;
using LinkedIdSelector.Model;
using LinkedIdSelector.Stores;
using Microsoft.Win32;

namespace LinkedIdSelector.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Document _doc { get; set; }
        private RevitExternalEvents _revitExternalEvent;
        public ICommand RunScriptCommand { get; }
        public ICommand SelectLinkedElementCommand { get; }
        public ICommand LoadFileCommand { get; }
        public ItemStore ItemStore { get; private set; }

        public DataGridViewModel DataGridViewModel { get; private set; }
        public ObservableCollection<LinkedElementInfo> LinkedElements { get; }

        private string _logMessageForInterface;
        public string LogMessageForInterface
        {
            get => _logMessageForInterface;
            set
            {
                _logMessageForInterface = value;
                OnPropertyChanged(nameof(LogMessageForInterface));
            }
        }


        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private bool _filePathExist;
        public bool FilePathExist
        {
            get
            {
                return _filePathExist;
            }
            set
            {
                _filePathExist = value;
                OnPropertyChanged(nameof(FilePathExist));
            }
        }


        public MainViewModel(Document doc, RevitExternalEvents externalEvent, ItemStore itemStore)
        {
            _revitExternalEvent = externalEvent;
            ItemStore = itemStore;
            LogMessageForInterface = ItemStore.LogMessageForInterfaceItemStore;
            ItemStore.LogMessageChanged += OnLogMessageChanged;
            _doc = doc;
            DataGridViewModel = new DataGridViewModel(this);
            LinkedElements = ItemStore.LinkedElementInfos;
            RunScriptCommand = new RelayCommand(x => RunExampleScript());
            SelectLinkedElementCommand = new RelayCommand(x => _revitExternalEvent.MakeRequest(RevitRequestId.SelectLinkedElement));
            LoadFileCommand = new RelayCommand(x => RunLoadExcelCommand());
        }

        public bool SetFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (AppSettings.Instance.FilePath != string.Empty)
            {
                try
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(AppSettings.Instance.FilePath);
                }
                catch
                {
                    openFileDialog.InitialDirectory = @"c:\";
                }

            }
            else
            {
                openFileDialog.InitialDirectory = @"c:\";
            }

            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;

            if (openFileDialog.FileName == string.Empty) return false;

            AppSettings.Instance.FilePath = filePath;
            AppSettings.Instance.Save();

            FilePath = filePath;
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();

            return true;
        }

        public void RunLoadExcelCommand()
        {
            if (SetFilePath())
            {
                LoadData();
            }


        }

        public void GetElementsInCurrentView()
        {
            ItemStore.ElementIdsInCurrentView.Clear();
            if (_doc?.ActiveView == null) return;

            var collector = new FilteredElementCollector(_doc, _doc.ActiveView.Id);
            ItemStore.ElementIdsInCurrentView.AddRange(collector.ToElementIds());
        }

        public void LoadData()
        {

        }


        public void LoadExcelFileOnStartUp()
        {
            FilePath = AppSettings.Instance.FilePath;



            if (File.Exists(FilePath))
            {
                LoadData();
            }
            else
            {
                FilePath = string.Empty;
            }
        }

        public void RunExampleScript() => _revitExternalEvent.MakeRequest(RevitRequestId.SampleRequest);

        private void OnLogMessageChanged(string message) => LogMessageForInterface = message;
    }
}
