using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLClassGenerator
{
    public class XMLAttributeElement
    {
        public static string outputFile;
        public XMLAttributeElement(string name)
        {
            this.name = name;
        }
        public string name;
        public string tag;
    }

}
