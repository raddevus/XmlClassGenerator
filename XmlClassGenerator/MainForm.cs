using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLClassGenerator
{
    public partial class MainForm : Form
    {
        private Dictionary<string, XMLClassElement> allClasses = new Dictionary<string, XMLClassElement>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void ProcessXMLButton_Click(object sender, EventArgs e)
        {
            string fileName = fileSelectorCtrl.FileTextBox.Text;
	        CreateClassFile(fileName);
            // set the output file - easy way to print the class info/ change later
            XMLClassElement.outputFile = Path.Combine(Path.GetDirectoryName(fileName),  "outClass.cs");
            if (File.Exists(XMLClassElement.outputFile))
            {
                File.Delete(XMLClassElement.outputFile);
            }
            
	        foreach (KeyValuePair<string,XMLClassElement> xc in allClasses)
	        {
		        Console.WriteLine(string.Format("name: {0} : id : {1} : parentId : {2}",xc.Value.name.ToUpper(), xc.Value.id, xc.Value.parentId));
		        xc.Value.OutputAttributes();
	        }

	        int attrCount = 0;
	        // ####################################################
	        // ########## GEN FLAT CLASS MODEL ####################
            string targetFile = Path.Combine("",TargetFileTextBox.Text);
            CSharpFileBuilder csFileBuilder = new CSharpFileBuilder(allClasses, targetFile, NamespaceTextBox.Text);
            csFileBuilder.CreateCSharpFile();
            //foreach (KeyValuePair<string,XMLClassElement> xc in allClasses)
            //{
            //    xc.Value.OutputClassInfo();
            //}
	        // ######### ITERATE FOR COUNTS #############
	        foreach (KeyValuePair<string,XMLClassElement> xc in allClasses)
	        {
		        if (xc.Value.attributes.Count() > 0)
		        {
                    File.AppendAllText("output.log", string.Format("name: {0} count: {1}", xc.Value.name, xc.Value.attributes.Count()));
		        }
		        attrCount += xc.Value.attributes.Count();
	        }
            File.AppendAllText("output.log", string.Format("Class count: {0}{1}",allClasses.Count,Environment.NewLine));
            File.AppendAllText("output.log", string.Format("All attributes : {0}{1}", attrCount, Environment.NewLine));
            
        }

        private void CreateClassFile(string xmlFileName)
        {
            allClasses.Clear();
            //XElement xel = XElement.Parse(inXml);
            //XDocument xdoc = new XDocument(xel);

            XDocument xdoc = XDocument.Load(xmlFileName);

            XMLClassElement outKey = null;
            string attrVal = string.Empty;
            foreach (XElement el in xdoc.Descendants())// xels.DescendantsAndSelf())
            {
                // if element has never been added do this
                bool retVal = allClasses.TryGetValue(el.Name.ToString(), out outKey);
                if (!retVal)
                {
                    XMLClassElement tempXClass = new XMLClassElement(el.Name.ToString());
                    if (el.Parent != null)
                    {
                        XMLClassElement xcOut = null;
                        if (allClasses.TryGetValue(el.Parent.Name.ToString(), out xcOut))
                        {
                            File.AppendAllText("output.log", string.Format("Parent: {0}{1}", el.Parent.Name.ToString(), Environment.NewLine));
                            // now we do a special check -- if there is no desc or attr then consider it a property on the class
                            if ((el.Descendants().Count() > 0) || (el.Attributes().Count() > 0))
                            {
                                tempXClass.parentId = xcOut.id;
                                xcOut.children.Add(tempXClass);
                                allClasses.Add(el.Name.ToString(), tempXClass);
                            }
                            else
                            {
                                XMLAttributeElement xaeTemp = null;
                                if (!xcOut.attributes.TryGetValue(el.Name.ToString(), out xaeTemp))
                                {
                                    // It looks a bit odd that we are using the xaeTemp, but it is okay,
                                    // bec. this code only gets hit if the item wasn't already added
                                    // which means xaeTemp is null - it's a fresh, empty bucket.
                                    xaeTemp = new XMLAttributeElement(el.Name.ToString());
                                    xaeTemp.tag = string.Format("\t[XmlElement(ElementName=\"{0}\")]{1}\tpublic List<string> all{0};{1}", el.Name.ToString(), Environment.NewLine);
                                    xcOut.attributes.Add(el.Name.ToString(), xaeTemp);
                                }
                            }

                        }
                    }
                    else
                    {
                        File.AppendAllText("output.log", string.Format("Name: {0}{1}", el.Name.ToString(), Environment.NewLine));
                        allClasses.Add(el.Name.ToString(), tempXClass);
                    }
                    //Console.WriteLine(tempXClass.name);
                }
                else // if element has been added (created /tracked) then get it and add it as a child
                {
                    XMLClassElement tempXClass = outKey;
                    if (el.Parent != null)
                    {
                        XMLClassElement xcOut = null;
                        if (allClasses.TryGetValue(el.Parent.Name.ToString(), out xcOut))
                        {
                            File.AppendAllText("output.log", string.Format("Parent: {0}{1}", el.Parent.Name.ToString(), Environment.NewLine));
                            tempXClass.parentId = xcOut.id;
                            if (!xcOut.children.Contains(tempXClass))
                            {
                                xcOut.children.Add(tempXClass);
                            }
                        }
                    }
                }
                foreach (XAttribute attr in el.Attributes())
                {
                    if (allClasses.TryGetValue(el.Name.ToString(), out outKey))
                    {
                        XMLAttributeElement outAttr = null;
                        if (!outKey.attributes.TryGetValue(attr.Name.ToString(), out outAttr))
                        {
                            XMLAttributeElement tempAttr = new XMLAttributeElement(attr.Name.ToString());
                            // this sets the tag, which gets output later as an XMLAttribute which is a string
                            tempAttr.tag = string.Format("\t[XmlAttribute]{1}\tpublic string {0};{1}", attr.Name.ToString(), Environment.NewLine);
                            outKey.attributes.Add(attr.Name.ToString(), tempAttr);
                            //				Console.WriteLine(string.Format("added: {0}",tempAttr.name));
                        }
                    }

                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            fileSelectorCtrl.FileTextBox.Text = @"D:\LocalSVN\Web3\HEB\HEB856Editor.xml";
        }

    }
}
