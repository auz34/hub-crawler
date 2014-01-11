/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>

using GitArchiveProcessor.DataLayer.Models;
using JSON = GitArchiveProcessor.DataLayer.Models.Json;

public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        AutoMapper.Mapper.CreateMap<JSON.GitEvent, GitEvent>()
            .ForMember(dest => dest.GitRepositoryId, opt => opt.MapFrom(src => src.Repository.Id));

        AutoMapper.Mapper.CreateMap<JSON.GitRepository, GitRepository>();
    }
}