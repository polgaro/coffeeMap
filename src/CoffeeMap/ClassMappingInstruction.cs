using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoffeeMap
{
    public class ClassMappingInstruction
    {
        private bool mappingsDiscovered = false;
        private Dictionary<PropertyInfo, Func<object, object>> mappings = new Dictionary<PropertyInfo, Func<object, object>>();

        public ClassMappingInstruction(Type sourceType, Type destinationType)
            : this (sourceType, destinationType, new ClassMappingInstructionOptions())
        {
        }


        public ClassMappingInstruction(Type sourceType, Type destinationType, ClassMappingInstructionOptions options)
        {
            SourceType = sourceType;
            DestinationType = destinationType;

            // TODO: actually use the options :D
            Options = options;
        }

        public Type SourceType { get; }
        public Type DestinationType { get; }
        public ClassMappingInstructionOptions Options { get; }

        internal static ClassMappingInstruction FromTypes<T1, T2>()
        {
            return FromTypes<T1, T2>(new ClassMappingInstructionOptions());
        }

        internal static ClassMappingInstruction FromTypes<T1, T2>(ClassMappingInstructionOptions options)
        {
            return new ClassMappingInstruction(typeof(T1), typeof(T2), options);
        }

        internal void Map(object source, object destination)
        {
            var propertyMappings = GetPropertyMappings();

            foreach(var propMapping in propertyMappings)
            {
                object sourceValueForThisProp = propMapping.Value(source);

                // todo: the biggest magic goes here. If the source and destination are NOT the same type, we need to execute THAT mapping :)

                propMapping.Key.SetValue(destination, sourceValueForThisProp);
            }
        }

        // todo: this should be its own object with property info and func to get info
        private Dictionary<PropertyInfo, Func<object, object>> GetPropertyMappings()
        {
            if (!mappingsDiscovered)
            {
                DiscoverPropertyMappings();
            }

            return mappings;
        }

        private void DiscoverPropertyMappings()
        {
            lock(this)
            {
                // in a multi-threaded environment, this CAN be true, so we need to check again
                if (!mappingsDiscovered)
                {
                    if (Options.AutoAddPropertiesWithSameName)
                    {
                        // TODO: allow to ignore properties!
                        PropertyInfo[] sourceProps = SourceType.GetProperties();
                        foreach (PropertyInfo destProp in DestinationType.GetProperties())
                        {
                            PropertyInfo sourceProp = sourceProps.FirstOrDefault(_ => _.Name == destProp.Name);

                            if (sourceProp != null)
                            {
                                mappings.Add(destProp, (object source) => sourceProp.GetValue(source));
                            }
                        }
                    }

                    // todo: the current mappings won't be on PropertyInfo, Func<object, object>. Now it's the time to convert them!
                    
                    mappingsDiscovered = true;
                }
            }
        }
    }
}
