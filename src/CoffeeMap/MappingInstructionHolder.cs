using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap
{
    public class MappingInstructionHolder
    {
        private List<MappingInstruction> _mappingInstructions = new List<MappingInstruction>();

        public IReadOnlyCollection<MappingInstruction> MappingInstructions
        { 
            get { return _mappingInstructions.AsReadOnly(); }
        }

        public MappingInstructionContext AddMapping<T1, T2>()
        {
            MappingInstruction mapping = MappingInstruction.FromTypes<T1, T2>();
            return AddMapping(mapping);
        }

        public MappingInstructionContext AddMapping(MappingInstruction mapping)
        {
            // TODO: Validate duplicated mappings

            _mappingInstructions.Add(mapping);
            return new MappingInstructionContext(this, mapping);
        }
    }
}
