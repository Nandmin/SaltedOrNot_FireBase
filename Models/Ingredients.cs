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
        public float Fat { get; set; }
        public float FullFat { get; set; }
        public float Carbohydrate { get; set; }
        public float Sugar { get; set; }
        public float Protein { get; set; }
        public float Salt { get; set; }
    }
}