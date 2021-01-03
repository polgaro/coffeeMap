using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap
{
    public class MappingInstructionContext
    {
        public MappingInstructionContext(MappingInstructionHolder holder, MappingInstruction mapping)
        {
            Holder = holder;
            Mapping = mapping;
        }

        public MappingInstructionHolder Holder { get; }
        public MappingInstruction Mapping { get; }
    }
}
