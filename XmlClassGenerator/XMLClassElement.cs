using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XMLClassGenerator
{
    public class XMLClassElement
    {
        public static string outputFile;
        public XMLClassElement(string name)
        {
            this.name = name;
            id = classCounter;
            classCounter++;
        }
        public List<XMLClassElement> children = new List<XMLClassElement>();
        public int id;
        public static int classCounter;
        public string name;
        public int parentId;
        public Dictionary<string, XMLAttributeElement> attributes = new Dictionary<string, XMLAttributeElement>();
        public StringBuilder sbOutput = new StringBuilder();

        public void OutputAttributes()
        {
            foreach (KeyValuePair<string, XMLAttributeElement> xa in this.attributes)
            {
                Console.WriteLine(string.Format("attr: {0}", xa.Value.name));
                sbOutput.Append(string.Format("attr: {0}", xa.Value.name));
            }

        }
        public void OutputClassInfo(bool isFirstClassFoundInXml)
        {
            File.AppendAllText(XMLClassElement.outputFile,string.Format("[Serializable]{0}public class {1}{0}",
                Environment.NewLine, this.name));
            File.AppendAllText(XMLClassElement.outputFile, "{");

            if (isFirstClassFoundInXml)
            {
                // if the user adds a using.xcg file to app run directory
                // then I open it and at the statements as using statements.
                if (File.Exists("methods.xcg"))
                {
                    string[] allUsingLines = File.ReadAllLines("methods.xcg");
                    List<string> allUsingLinesUpdated = new List<string>();
                    foreach (string str in allUsingLines)
                    {
                        allUsingLinesUpdated.Add(string.Format(str, this.name));
                    }
                    File.AppendAllLines(XMLClassElement.outputFile, allUsingLinesUpdated);
                }

            }

            foreach (KeyValuePair<string, XMLAttributeElement> xa in this.attributes)
            {
                File.AppendAllText(XMLClassElement.outputFile, xa.Value.tag);
            }
            foreach (XMLClassElement xce in children)
            {
                File.AppendAllText(XMLClassElement.outputFile, string.Format("\t[XmlElement(ElementName=\"{0}\")]{1}\tpublic List<{0}> all{0} = new List<{0}>();{1}", xce.name, Environment.NewLine));
            }

            File.AppendAllText(XMLClassElement.outputFile, "}" + Environment.NewLine);
        }

    }
}
