using Domain.Markers;

namespace Infrastructure.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
