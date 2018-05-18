using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace PathfinderApp
{
    class CharacterInfo
    {
        public string characterName;
        public string alignment;
        public string playerName;
        public string characterLevel;
        public string deity;
        public string homeland;
        public string race;
        public string size;
        public string gender;
        public string age;
        public string height;
        public string weight;
        public string hairColor;
        public string eyeColor;
        public string className;
        public string currentXp;
        public string nextLevelXp;

        //Constructor for Character class
        public CharacterInfo()
        {
            characterName = "";
            alignment = "";
            playerName = "";
            characterLevel = "";
            deity = "";
            homeland = "";
            race = "";
            size = "";
            gender = "";
            age = "";
            height = "";
            weight = "";
            hairColor = "";
            eyeColor = "";
            className = "";
            currentXp = "";
            nextLevelXp = "";
        }

        public void saveCharacterInfo()
        {
            try
            {
                //Create a directory called Characters if it doesn't exist
                System.IO.Directory.CreateDirectory("./Characters");
                //Open the File
                StreamWriter sw = new StreamWriter("./Characters/" + this.characterName + ".txt", false, Encoding.ASCII);

                //Save a bunch of info
                sw.WriteLine(this.characterName);
                sw.WriteLine(this.alignment);
                sw.WriteLine(this.playerName);
                sw.WriteLine(this.characterLevel);
                sw.WriteLine(this.deity);
                sw.WriteLine(this.homeland);
                sw.WriteLine(this.race);
                sw.WriteLine(this.size);
                sw.WriteLine(this.gender);
                sw.WriteLine(this.age);
                sw.WriteLine(this.height);
                sw.WriteLine(this.weight);
                sw.WriteLine(this.hairColor);
                sw.WriteLine(this.eyeColor);
                sw.WriteLine(this.className);
                sw.WriteLine(this.currentXp);
                sw.WriteLine(this.nextLevelXp);

                //close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
