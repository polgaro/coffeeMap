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
        private readonly ClassMappingInstructionHolder _mappingInstructions;

        public CoffeeMapper(ClassMappingInstructionHolder mappingInstructions)
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
            // TODO: hashed search (dictionary?)
            ClassMappingInstruction mapping = _mappingInstructions.MappingInstructions
                .FirstOrDefault(_ => _.SourceType == sourceType && _.DestinationType == destinationType) ??
                throw new Exceptions.NoMappingFoundException($"Couldn't find mapping from {sourceType.FullName} to {destinationType.FullName}");

            object destination = Activator.CreateInstance(destinationType);

            mapping.Map(source, destination);

            return destination;
        }
    }
}
