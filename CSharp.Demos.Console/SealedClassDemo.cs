using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Demos.Console
{
    sealed class SealedClassDemo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string GetName(string name)
        {
            return name;
        }
    }
}
