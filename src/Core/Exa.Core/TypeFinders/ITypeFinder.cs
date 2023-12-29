using System.Reflection;

namespace Exa.Core.TypeFinders
{
    public interface ITypeFinder
    {
        IList<Assembly> GetAssemblies();

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<TEntity>(bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<TEntity>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}
