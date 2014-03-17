using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Models
{
    public enum LanguageInUse { English, Finnish, Russian, Portugese}

    public class LanguageRecord
    {
        private String[] language;

        public LanguageRecord(String english, String finnish, String russian, String portugese)
        {
            language = new string[4];
            int i = 0;

            language[i++] = english;
            language[i++] = finnish;
            language[i++] = russian;
            language[i++] = portugese;
        }

        public String this[int index]
        {
            get { return language[index]; }
            set { language[index] = value; }
        }
    }
}
