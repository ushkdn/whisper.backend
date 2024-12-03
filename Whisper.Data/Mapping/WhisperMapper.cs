using AutoMapper;

namespace Whisper.Data.Mapping;

public partial class WhisperMapper
{
    private static readonly MapperConfiguration? configuration = null;
    private static readonly IMapper mapper;
    public static IMapper Mapper => mapper;

    static WhisperMapper()
    {
        if (configuration == null)
        {
            configuration = new MapperConfiguration(cfg =>
            {
                MapUser(cfg);
            });
        }
        mapper ??= configuration.CreateMapper();
    }
}