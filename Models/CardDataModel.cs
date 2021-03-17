using System.Collections;
using System.Collections.Generic;

namespace SrPogo.Models
{
    public class CardDataModel
    {
        public double amount { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public string source { get; set; }
        public ArrayList metadata { get; set; }
    }
}