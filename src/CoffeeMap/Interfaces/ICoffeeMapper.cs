using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap.Interfaces
{
    public interface ICoffeeMapper
    {
        T Map<T>(object source);

        TDestination Map<TSource, TDestination>(TSource source);

        object Map(object source, Type sourceType, Type destinationType);
    }
}
