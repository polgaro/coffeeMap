using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap
{
    public class MappingInstruction
    {
        public MappingInstruction(Type sourceType, Type destinationType)
            : this (sourceType, destinationType, new MappingInstructionOptions())
        {
        }


        public MappingInstruction(Type sourceType, Type destinationType, MappingInstructionOptions options)
        {
            SourceType = sourceType;
            DestinationType = destinationType;

            // TODO: actually use the options :D
            Options = options;
        }

        public Type SourceType { get; }
        public Type DestinationType { get; }
        public MappingInstructionOptions Options { get; }

        internal static MappingInstruction FromTypes<T1, T2>()
        {
            return new MappingInstruction(typeof(T1), typeof(T2));
        }
    }
}
