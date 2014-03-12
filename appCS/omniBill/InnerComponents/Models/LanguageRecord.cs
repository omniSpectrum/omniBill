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
            English = english;
            Finnish = finnish;
            Russian = russian;
            Portugese = portugese;
        }

        public String English { get; set; }
        public String Finnish { get; set; }
        public String Russian { get; set; }
        public String Portugese { get; set; }
    }
}
