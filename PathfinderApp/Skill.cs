using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PathfinderApp
{
    public class Skill
    {
        public string skillName;
        public string totalBonus;
        public string modType;
        public string abilityMod;
        public string ranks;
        public string miscMod;

        //Create a skill with all the info 
        public Skill(string sn, string tb, string mt, string am, string r, string mm)
        {
            this.skillName = sn;
            this.totalBonus = tb;
            this.modType = mt;
            this.abilityMod = am;
            this.ranks = r;
            this.miscMod = mm;
        }

        //Creates a blank skill 
        public Skill()
        {
            this.skillName = "Skill Name";
            this.totalBonus = "0";
            this.modType = "Dex";
            this.abilityMod = "0";
            this.ranks = "0";
            this.miscMod = "0";
        }
    }


 
    public class ASkill
    {
        public string name { get; set; }
    }

    public class Skills_Detail
    {
        public string name { get; set; }
        public string stat { get; set; }
        public string useuntrained { get; set; }
    }

   

  
}
