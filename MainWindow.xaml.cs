using System.Windows;
using System.Text;

namespace TextureConverterHelper
{
    public partial class MainWindow : Window
    {
        private HelperSettings Settings { get; set; }
        private HelperModel    Model    { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            Settings = new HelperSettings();

            Model = new HelperModel(new HelperView(OutputPathLabelOut, ConversionList));
            ConversionList.ItemsSource = Model.Files;
        }

        private void RemoveFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var file = ConversionList.SelectedItem;

            Model.Files.Remove(file);
        }

        private void RemoveAllFilesButton_Click(object sender, RoutedEventArgs e) => Model.Files.Clear();

        private void ChangeOutputPathButton_Click(object sender, RoutedEventArgs e) => ShowChangeOutputDialog();

        private void ShowChangeOutputDialog()
        {
            using var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            {
                folderBrowserDialog.SelectedPath = Settings.GetAppDirectory();
                folderBrowserDialog.Description = "Set the output folder";

                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Model.OutputPath = folderBrowserDialog.SelectedPath;
            }
        }

        private void AddFilesButton_Click(object sender, RoutedEventArgs e)
        {
            using var fileBrowsingDialog = new System.Windows.Forms.OpenFileDialog();
            {
                fileBrowsingDialog.InitialDirectory = Settings.GetAppDirectory();
                fileBrowsingDialog.Filter = Settings.GetFileDialogFilter();
                fileBrowsingDialog.Multiselect = true;

                if (fileBrowsingDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    foreach (string file in fileBrowsingDialog.FileNames)
                        AddNewFiles(file);
            }
        }

        private void AddNewFiles(object file)
        {
            bool isDuplicate(object file)
            {
                foreach (var item in ConversionList.Items)
                {
                    if (item.ToString().Contains(file.ToString()))
                    {
                        return true;
                    }
                }

                return false;
            }

            if (!isDuplicate(file))
                Model.Files.Add(file);
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (Model.OutputPath == string.Empty)
                ShowChangeOutputDialog();

            ConvertAllFiles();
        }

        private void ConvertAllFiles()
        {
            foreach (var File in Model.Files)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "TextureCompiler.exe",
                    UseShellExecute = true,
                    Arguments = BuildCompilerArguments(File.ToString())
                };
                process.Start();

                process.WaitForExit();
            }

            Model.Files.Clear();
        }

        private string BuildCompilerArguments(string fullFilePath)
        {
            StringBuilder arguments = new StringBuilder();

            var fileName = System.IO.Path.GetFileNameWithoutExtension(fullFilePath);

            arguments.AppendFormat(
                Settings.GetArgumentsFormat(), 
                fullFilePath, 
                Model.OutputPath, 
                fileName);

            return arguments.ToString();
        }
    }
}
