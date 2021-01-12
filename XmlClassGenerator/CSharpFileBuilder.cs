using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XMLClassGenerator
{
    public class CSharpFileBuilder
    {
        private Dictionary<string, XMLClassElement> allClasses;
        private string csharpFileName;
        private string namespaceValue;

        public CSharpFileBuilder(Dictionary<string, XMLClassElement> allClasses, 
            string fileName, string namespaceValue)
        {
            this.allClasses = allClasses;
            csharpFileName = fileName;
            this.namespaceValue = namespaceValue;
        }

        public void CreateCSharpFile()
        {
            XMLClassElement.outputFile = csharpFileName;
            // if the file exists, then delete it
            File.Delete(csharpFileName);

            WriteCSharpFileHeader();
            // isFirstClass allows me to write extra info to the first class found in 
            // the xml file.
            bool isFirstClassFoundInXml = true;
            foreach (KeyValuePair<string, XMLClassElement> xc in allClasses)
            {
                xc.Value.OutputClassInfo(isFirstClassFoundInXml);
                // after the first time through, obviously it isn't the first class anymore
                isFirstClassFoundInXml = false;
            }
            WriteCSharpFileFooter();
        }

        private void WriteCSharpFileHeader()
        {
            // if the user adds a using.xcg file to app run directory
            // then I open it and at the statements as using statements.
            if (File.Exists("using.xcg"))
            {
                string [] allUsingLines = File.ReadAllLines("using.xcg");
                File.AppendAllLines(csharpFileName, allUsingLines);
            }
            // Note: double-curly is escaped curly in string.Format
            File.AppendAllText(csharpFileName, 
                string.Format("namespace {0}{1}{{{1}",namespaceValue, Environment.NewLine));
        }

        private void WriteCSharpFileFooter()
        {
            File.AppendAllText(csharpFileName, 
                string.Format("}} // closes namespace {0}{1}",namespaceValue, Environment.NewLine));
        }
    }
}
