using CoffeeMap.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoffeeMap
{
    public class CoffeeMapper : ICoffeeMapper
    {
        private readonly MappingInstructionHolder _mappingInstructions;

        public CoffeeMapper(MappingInstructionHolder mappingInstructions)
        {
            _mappingInstructions = mappingInstructions;
        }

        public T Map<T>(object source)
        {
            return (T)Map(source, source.GetType(), typeof(T));
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return (TDestination)Map(source, typeof(TSource), typeof(TDestination));
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            // super TERRIBLE quick -- only for now -- implementation!
            // this is NOT the final implementation at all whatsoever. I swear :D

            // TODO: hashed search
            MappingInstruction mapping = _mappingInstructions.MappingInstructions
                .FirstOrDefault(_ => _.SourceType == sourceType && _.DestinationType == destinationType) ??
                throw new Exceptions.NoMappingFoundException($"Couldn't find mapping from {sourceType.FullName} to {destinationType.FullName}");

            object ret = Activator.CreateInstance(destinationType);

            // TODO: the properties obviously come from the mappings
            PropertyInfo[] sourceProps = sourceType.GetProperties();
            foreach (PropertyInfo destProp in destinationType.GetProperties())
            {
                PropertyInfo sourceProp = sourceProps.FirstOrDefault(_ => _.Name == destProp.Name)
                    ?? throw new Exception($"Prop {destProp.Name} in {sourceType.FullName} not found");

                // TODO: obviously this should be a function/expression, but for now...
                object sourceValueForThisProp = sourceProp.GetValue(source);
                destProp.SetValue(ret, sourceValueForThisProp);
            }

            return ret;
        }
    }
}
