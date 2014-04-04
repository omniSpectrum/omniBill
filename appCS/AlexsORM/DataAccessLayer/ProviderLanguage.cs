using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using omniBill.InnerComponents.Models;

namespace omniBill.InnerComponents.DataAccessLayer
{
    public class ProviderLanguage
    {
        //FIELDS
        String filePath; //Xml file path

        public ProviderLanguage(String dataDirectory, String fileName)
        {
            filePath = dataDirectory + fileName;
        }

        //METHODS
        public Dictionary<String, LanguageRecord> FindAll()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);

            Dictionary<String, LanguageRecord> myLangSet = new Dictionary<string, LanguageRecord>();

            foreach (XmlNode node in xml.SelectNodes("/language/record"))
            {
                String eng = node.SelectSingleNode("english").InnerText;
                String fin = node.SelectSingleNode("finnish").InnerText;
                String rus = node.SelectSingleNode("russian").InnerText;
                String por = node.SelectSingleNode("portugese").InnerText;

                myLangSet.Add(eng, new LanguageRecord(eng, fin, rus, por));
            }

            return myLangSet;
        }
    }
}
