using System;
using System.Collections.Generic;
using System.Text;

namespace TokBlastPrototype1.Models.Local
{
    public class WordProperties
    {
        public int id { get; set; }
        public string tok_type { get; set; }
        public string tok_group { get; set; }
        public string category { get; set; }

        public string primary_text { get; set; }
        public string primary_trimmed { get; set; }
        public string secondary_text { get; set; }
        public int number_of_words { get; set; }

        public string longest_word { get; set; }
        public int longest_word_length { get; set; }
    }
}
