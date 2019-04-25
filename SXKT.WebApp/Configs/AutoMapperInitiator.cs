using AutoMapper;
using SXKT.Infrastructure.Extensions;
using SXKT.Infrastructure.Utility;
using System.Collections.Generic;

namespace SXKT.WebApp.Configs
{
    public class AutoMapperInitiator
    {
        public static void Init()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.ForAllMaps((map, exp) =>
                {
                    foreach (var unmappedPropertyName in map.GetUnmappedPropertyNames())
                    {
                        exp.ForMember(unmappedPropertyName, opt => opt.Ignore());
                    }
                });


                cfg.CreateMap<string, List<int>>().ConvertUsing<StringToListIntConverter>();
                cfg.CreateMap<List<int>, string>().ConvertUsing<ListIntToStringConverter>();
                //cfg.CreateMap<FilterArticleRequest, ArticleFilter>().ForMember(des => des.IsPublish, opt => opt.MapFrom(source => source.IsPublish.ToString()));
            });
            RegisterMapping();
        }

        public static void RegisterMapping()
        {
            //Add CustomMapper
            return;
        }
    }

    public class StringToListIntConverter : ITypeConverter<string, List<int>>
    {
        public List<int> Convert(string source, List<int> destination, ResolutionContext context)
        {
            return source.ToListInt();
        }
    }

    public class ListIntToStringConverter : ITypeConverter<List<int>, string>
    {
        public string Convert(List<int> source, string destination, ResolutionContext context)
        {
            if (source.IsBlank())
                return null;
            return string.Join(",", source);
        }
    }
}