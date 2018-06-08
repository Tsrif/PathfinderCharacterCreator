using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderApp
{
    public class Feat
    {
        public string featName;
        public string description;

        //create Feat using given info 
        public Feat(string fn, string descript)
        {
            this.featName = fn;
            this.description = descript;
        }

        //create Blank Feat
        public Feat() {
            this.featName = "Feat Name";
            this.description = "Feat Description";
        }
    }

    public class AFeat
    {
        public string name { get; set; }
    }

    public class Feat_Detail
    {
        public string name { get; set; }
        public string description { get; set; }
    }
}
