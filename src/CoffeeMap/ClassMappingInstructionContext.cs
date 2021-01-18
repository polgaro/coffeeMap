using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap
{
    public class ClassMappingInstructionContext
    {
        public ClassMappingInstructionContext(ClassMappingInstructionHolder holder, ClassMappingInstruction mapping)
        {
            Holder = holder;
            Mapping = mapping;
        }

        public ClassMappingInstructionHolder Holder { get; }
        public ClassMappingInstruction Mapping { get; }
    }
}
