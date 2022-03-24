using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaltedOrNot_FireBase.Models
{
    public class Ingredients
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Energy { get; set; }
        public int Kcal { get; set; }
        public double Fat { get; set; }
        public double FullFat { get; set; }
        public double Carbohydrate { get; set; }
        public double Sugar { get; set; }
        public double Protein { get; set; }
        public double Salt { get; set; }
    }
}