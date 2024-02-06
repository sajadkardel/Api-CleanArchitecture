using AutoMapper;

namespace Infrastructure.PackageConfiguration.AutoMapper
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
