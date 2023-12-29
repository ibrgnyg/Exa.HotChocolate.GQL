using System.Reflection;

namespace Exa.Core.TypeFinders
{
    public class AppTypeFinder : TypeFinder
    {
        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return _ensureBinFolderAssembliesLoaded; }
            set { _ensureBinFolderAssembliesLoaded = value; }
        }

        public virtual string GetBinDirectory()
        {
            return AppContext.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                LoadMatchingAssemblies();
            }
            return base.GetAssemblies();
        }

    }
}
