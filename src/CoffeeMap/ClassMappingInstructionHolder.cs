using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap
{
    public class ClassMappingInstructionHolder
    {
        private List<ClassMappingInstruction> _mappingInstructions = new List<ClassMappingInstruction>();

        public IReadOnlyCollection<ClassMappingInstruction> MappingInstructions
        { 
            get { return _mappingInstructions.AsReadOnly(); }
        }

        public ClassMappingInstructionContext AddMapping<T1, T2>()
        {
            ClassMappingInstruction mapping = ClassMappingInstruction.FromTypes<T1, T2>();
            return AddMapping(mapping);
        }

        public ClassMappingInstructionContext AddMapping<T1, T2>(ClassMappingInstructionOptions options)
        {
            ClassMappingInstruction mapping = ClassMappingInstruction.FromTypes<T1, T2>(options);
            return AddMapping(mapping);
        }

        public ClassMappingInstructionContext AddMapping(ClassMappingInstruction mapping)
        {
            // TODO: Validate duplicated mappings

            _mappingInstructions.Add(mapping);
            return new ClassMappingInstructionContext(this, mapping);
        }
    }
}
