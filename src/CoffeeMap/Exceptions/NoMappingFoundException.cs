using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMap.Exceptions
{
    public class NoMappingFoundException : Exception
    {
        public NoMappingFoundException(string descrpition) : base(descrpition)
        {

        }
    }
}
