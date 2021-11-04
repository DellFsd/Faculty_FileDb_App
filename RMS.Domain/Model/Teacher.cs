using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Domain.Model
{
   public class Teacher
   {
       private static int _id = 0;
        public int ID { 
            get => _id;
            set => _id = _id + 1;
        } 
        public string Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }


    }
}
