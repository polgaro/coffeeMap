using CoffeeMap.Exceptions;
using CoffeeMap.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CoffeeMap.Tests
{
    [TestClass]
    public class BasicTests
    {
        #region Example super duper basic classes
        public class ExampleClass1
        {
            public string A { get; set; }
            public string B { get; set; }
        }

        public class ExampleClass2
        {
            public string B { get; set; }
            public string A { get; set; }
        }
        #endregion Example super duper basic classes

        [TestMethod]
        public void NoMappingFoundException()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            ExampleClass1 exampleObject1 = GetObject1(); 
            Assert.ThrowsException<NoMappingFoundException>
                    (() => mapper.Map<ExampleClass2>(exampleObject1));
        }

        [TestMethod]
        public void BasicMap0T()
        {
            ICoffeeMapper mapper = CreateMapperWithExampleMapping();
            ExampleClass1 exampleObject1 = GetObject1();
            object objResult = mapper.Map(exampleObject1, typeof(ExampleClass1), typeof(ExampleClass2));

            Assert.IsInstanceOfType(objResult, typeof(ExampleClass2));
            ExampleClass2 result = (ExampleClass2)objResult;

            Assert.AreEqual(exampleObject1.A, result.A);
        }

        [TestMethod]
        public void BasicMap1T()
        {
            ICoffeeMapper mapper = CreateMapperWithExampleMapping();
            ExampleClass1 exampleObject1 = GetObject1();
            ExampleClass2 result = mapper.Map<ExampleClass2>(exampleObject1);
            Assert.AreEqual(exampleObject1.A, result.A);
        }

        [TestMethod]
        public void BasicMap2T()
        {
            ICoffeeMapper mapper = CreateMapperWithExampleMapping();
            ExampleClass1 exampleObject1 = GetObject1();
            ExampleClass2 result = mapper.Map<ExampleClass1, ExampleClass2>(exampleObject1);
            Assert.AreEqual(exampleObject1.A, result.A);
        }
        private static ExampleClass1 GetObject1()
        {
            return new ExampleClass1 { A = Guid.NewGuid().ToString(), B = Guid.NewGuid().ToString() };
        }

        private static ICoffeeMapper CreateMapperWithExampleMapping()
        {
            ClassMappingInstructionHolder mappingInstructions = new ClassMappingInstructionHolder();
            mappingInstructions.AddMapping<ExampleClass1, ExampleClass2>();
            ICoffeeMapper mapper = new CoffeeMapper(mappingInstructions);
            return mapper;
        }
    }
}
