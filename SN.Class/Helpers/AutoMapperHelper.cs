using AutoMapper;
using System.Collections.Generic;

namespace SN.Class.Helpers
{
    public class AutoMapperHelper
    {
        public static T MapToSameType<T>(T obj)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, T>();
            });

            var mapper = new Mapper(configuration);

            return mapper.Map<T,T>(obj);
        }

        public static List<T> MapToSameTypeList<T>(List<T> list)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, T>();
            });

            var mapper = new Mapper(configuration);

            return mapper.Map<List<T>, List<T>>(list);
        }

        public static List<TTarget> MapToDifferentTypeList<TSource,TTarget>(List<TSource> list)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TTarget>();
            });

            var mapper = new Mapper(configuration);

            return mapper.Map<List<TSource>, List<TTarget>>(list);
        }

        public static TTarget MapToDifferentType<TSource, TTarget>(TSource list)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TTarget>();
            });

            var mapper = new Mapper(configuration);

            return mapper.Map<TSource, TTarget>(list);
        }
    }
}
