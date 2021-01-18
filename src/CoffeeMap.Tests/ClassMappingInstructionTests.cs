using CoffeeMap.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap.Tests
{
    [TestClass]
    public class ClassMappingInstructionTests
    {
        #region Example super duper basic classes
        public class ExampleClass1
        {
            public string A { get; set; }
        }

        public class ExampleClass2
        {
            public string A { get; set; }
        }

        public class ExampleClassWithIntProperty
        {
            public int A { get; set; }
        }

        public class ExampleClassWithLongProperty
        {
            public long A { get; set; }
        }
        #endregion Example super duper basic classes

        [TestMethod]
        public void ClassMappingAutoDiscoversBasicProperty()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            mappingInstructions.AddMapping<ExampleClass1, ExampleClass2>(new ClassMappingInstructionOptions { AutoAddPropertiesWithSameName = true });
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            ExampleClass1 exampleObject1 = GetObject1();
            ExampleClass2 result = mapper.Map<ExampleClass2>(exampleObject1);
            Assert.AreEqual(exampleObject1.A, result.A);
        }

        /// <summary>
        /// As the "AutoAddPropertiesWithSameName" and there are no instructions for the property "A"
        ///     Then the property "A" in the destination will be null (default of string)
        /// </summary>

        [TestMethod]
        public void ClassMappingWithoutAutoDiscoversBasicProperty()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            mappingInstructions.AddMapping<ExampleClass1, ExampleClass2>(new ClassMappingInstructionOptions { AutoAddPropertiesWithSameName = false });
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            ExampleClass1 exampleObject1 = GetObject1();
            ExampleClass2 result = mapper.Map<ExampleClass2>(exampleObject1);
            Assert.AreEqual(null, result.A);
        }

        [TestMethod]
        public void ClassMappingAutoDiscoversBasicPropertyWithMismatchTypes()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            mappingInstructions.AddMapping<ExampleClass1, ExampleClassWithIntProperty>(new ClassMappingInstructionOptions { AutoAddPropertiesWithSameName = true });
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            ExampleClass1 exampleObject1 = GetObject1();
            ExampleClassWithIntProperty result = mapper.Map<ExampleClassWithIntProperty>(exampleObject1);
            Assert.AreEqual(exampleObject1.A, result.A);
        }

        [TestMethod]
        public void ClassMappingAutoDiscoversBasicPropertyFromIntToLong()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            mappingInstructions.AddMapping<ExampleClassWithIntProperty, ExampleClassWithLongProperty>();
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            ExampleClassWithIntProperty exampleObject1 = GetExampleObjectWithIntProperty();
            ExampleClassWithLongProperty result = mapper.Map<ExampleClassWithLongProperty>(exampleObject1);
            Assert.AreEqual(exampleObject1.A, result.A);
        }

        [TestMethod]
        public void ClassMappingAutoDiscoversBasicPropertyFromLongToInt()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            mappingInstructions.AddMapping<ExampleClassWithLongProperty, ExampleClassWithIntProperty>();
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            ExampleClassWithLongProperty exampleObject1 = GetExampleObjectWithLongProperty();
            ExampleClassWithIntProperty result = mapper.Map<ExampleClassWithIntProperty>(exampleObject1);
            Assert.AreEqual(exampleObject1.A, result.A);
        }

        [TestMethod]
        public void ClassMappingAutoDiscoversBasicPropertyFromLongToIntTooLong()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            mappingInstructions.AddMapping<ExampleClassWithLongProperty, ExampleClassWithIntProperty>();
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            ExampleClassWithLongProperty exampleObject1 = GetExampleObjectWithLongProperty();
            exampleObject1.A += int.MaxValue;

            ExampleClassWithIntProperty result = mapper.Map<ExampleClassWithIntProperty>(exampleObject1);
            Assert.AreEqual(exampleObject1.A, result.A);
        }

        private static ExampleClass1 GetObject1()
        {
            return new ExampleClass1 { A = Guid.NewGuid().ToString()};
        }

        private static ExampleClassWithIntProperty GetExampleObjectWithIntProperty()
        {
            return new ExampleClassWithIntProperty { A = new Random().Next() };
        }

        private static ExampleClassWithLongProperty GetExampleObjectWithLongProperty()
        {
            return new ExampleClassWithLongProperty { A = new Random().Next() };
        }
    }
}
