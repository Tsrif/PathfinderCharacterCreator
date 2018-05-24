using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderApp
{
    class Skill
    {
        public string skillName;
        public string totalBonus;
        public string modType;
        public string abilityMod;
        public string ranks;
        public string miscMod;


        public Skill(string sn, string tb, string mt, string am, string r, string mm)
        {
            this.skillName = sn;
            this.totalBonus = tb;
            this.modType = mt;
            this.abilityMod = am;
            this.ranks = r;
            this.miscMod = mm;
        }


        
    }
}
