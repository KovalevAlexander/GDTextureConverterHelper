using System;

namespace TextureConverterHelper
{
    internal class HelperSettings
    {
        private readonly string appDirectory;
        private readonly string fileDialogFilter = "tga textures (*.tga)|*.tga";
        private readonly string argumentsFormat =
            "\u0022{0}\u0022 " +
            "\u0022{1}\u005C{2}.tex\u0022 " +
            "-format tex";

        public HelperSettings()
        {
            appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        public string GetAppDirectory() => appDirectory;
        public string GetFileDialogFilter() => fileDialogFilter;
        public string GetArgumentsFormat() => argumentsFormat;
    }
}