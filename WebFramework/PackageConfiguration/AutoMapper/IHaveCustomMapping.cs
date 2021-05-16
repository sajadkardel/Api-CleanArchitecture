using AutoMapper;

namespace WebFramework.PackageConfiguration.AutoMapper
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
