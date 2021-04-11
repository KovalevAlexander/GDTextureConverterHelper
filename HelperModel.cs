using System.Collections.ObjectModel;

namespace TextureConverterHelper
{
    internal class HelperModel
    {
        string _outputPath;

        public HelperView View { get; private set; }

        public ObservableCollection<object> Files{ get; private set; }

        public string OutputPath
        {
            get => _outputPath;

            private set
            {
                _outputPath = value;

                View.UpdateOutputPath(_outputPath);
            }
        }

        public HelperModel(HelperView view)
        {
            View = view;

            Files = new ObservableCollection<object>();
            OutputPath = string.Empty;
        }
    }
}