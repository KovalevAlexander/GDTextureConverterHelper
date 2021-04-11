using System.Text;
using System.Windows.Controls;

namespace TextureConverterHelper
{
    internal class HelperView
    {
        private readonly Label   outputPath;
        private readonly ListBox conversionList;

        public HelperView(Label _outputPath, ListBox _conversionList)
        {
            outputPath     = _outputPath;
            conversionList = _conversionList;
        }

        public void UpdateConversionList() => conversionList.Items.Refresh();

        public void UpdateOutputPath(string path) => outputPath.Content = BuildShortPath(path);

        private string BuildShortPath(string path)
        {
            StringBuilder shortPath = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                var subsringIndex = path.LastIndexOf(@"\");

                if (subsringIndex != -1)
                {
                    shortPath.Insert(0, path.Substring(path.LastIndexOf(@"\")));
                    path = path.Remove(path.LastIndexOf(@"\"));
                }
                else
                    break;
            }

            shortPath.Insert(0, "...");
            shortPath.Append(@"\");

            return shortPath.ToString();
        }
    }
}