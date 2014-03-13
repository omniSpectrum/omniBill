using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omniBill.InnerComponents.Models
{
    public enum LanguageInUse { English, Finnish, Russian, Portugese}

    public class LanguageRecord
    {
        public LanguageRecord(String english, String finnish, String russian, String portugese)
        {
            Language = new string[4];
            int i = 0;

            Language[i++] = english;
            Language[i++] = finnish;
            Language[i++] = russian;
            Language[i++] = portugese;
        }

        public String[] Language { get; set; } //English, Finnish, Russian, Portugese

        public String this[int index]
        {
            get { return Language[index]; }
            set { Language[index] = value; }
        }
    }
}
