using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*This Script handles basically everything for the Character Form
 * There's code in here that could definitely use improvement. 
 * Since this project is very early, efficiency isn't a big concern at the moment, functionality is.
 */

namespace PathfinderApp
{
    public partial class CharacterForm : MetroFramework.Forms.MetroForm
    {
        //Create new charactet Object
        Character character = new Character();
        List<MetroFramework.Controls.MetroPanel> list_skillsPanels = new List<MetroFramework.Controls.MetroPanel>();
        Dictionary<string, string> slow = new Dictionary<string, string>
        {
            { "1", "3,000" },
            { "2", "7,500" },
            { "3", "14,000" },
            { "4", "23,000" },
            { "5", "35,000" },
            { "6", "53,000" },
            { "7", "77,000" },
            { "8", "115,000" },
            { "9", "160,000" },
            { "10", "235,000" },
            { "11", "330,000" },
            { "12", "475,000" },
            { "13", "665,000" },
            { "14", "955,000" },
            { "15", "1,350,000" },
            { "16", "1,900,000" },
            { "17", "2,700,000" },
            { "18", "3,850,000" },
            { "19", "5,350,000" },
            { "20", ">5,350,000" }
        };
        Dictionary<string, string> medium = new Dictionary<string, string>
        {
            { "1", "2,000" },
            { "2", "5,000" },
            { "3", "9,000" },
            { "4", "15,000" },
            { "5", "23,000" },
            { "6", "35,000" },
            { "7", "51,000" },
            { "8", "75,000" },
            { "9", "105,000" },
            { "10", "155,000" },
            { "11", "220,000" },
            { "12", "315,000" },
            { "13", "445,000" },
            { "14", "635,000" },
            { "15", "890,000" },
            { "16", "1,300,000" },
            { "17", "1,800,000" },
            { "18", "2,550,000" },
            { "19", "3,600,000" },
            { "20", ">3,600,000" }
        };

        Dictionary<string, string> fast = new Dictionary<string, string>
        {
            { "1", "1,300" },
            { "2", "3,300" },
            { "3", "6,000" },
            { "4", "10,000" },
            { "5", "15,000" },
            { "6", "23,000" },
            { "7", "34,000" },
            { "8", "50,000" },
            { "9", "71,000" },
            { "10", "105,000" },
            { "11", "145,000" },
            { "12", "210,000" },
            { "13", "295,000" },
            { "14", "425,000" },
            { "15", "600,000" },
            { "16", "850,000" },
            { "17", "1,200,000" },
            { "18", "1,700,000" },
            { "19", "2,400,000" },
            { "20", ">2,400,000" }
        };



        public CharacterForm()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Character_TabControl.SelectedIndex = 0;

            //change the default selected values on the combo boxes
            Allignment_comboBox.SelectedIndex = 0;
            characterLevel_comboBox.SelectedIndex = 0;
            size_comboBox.SelectedIndex = 0;
            XP_point_total_comboBox.SelectedIndex = 0;
            gender_ComboBox.SelectedIndex = 0;

            CalculateNextLevel();
            //Creates a default Blank skill
            Skill aSkill = new Skill();
            CreateSkillPanel(aSkill);
        }

        //Create a new character
        //Should probably save and then create the new form
        private void newCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            CharacterForm characterForm = new CharacterForm();
            characterForm.Show();
        }

        //add input to appropriate player info 
        //This is probably going to be changed later on
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            character.characterInfo.characterName = characterName_textBox.Text;
            character.characterInfo.alignment = Allignment_comboBox.Text;
            character.characterInfo.playerName = Player_textBox.Text;
            character.characterInfo.characterLevel = characterLevel_comboBox.Text;
            character.characterInfo.deity = Deity_textBox.Text;
            character.characterInfo.homeland = HomeLand__textBox.Text;
            character.characterInfo.race = Race_textBox.Text;
            character.characterInfo.size = size_comboBox.Text;
            character.characterInfo.gender = gender_ComboBox.SelectedText;
            character.characterInfo.age = Age_textBox.Text;
            character.characterInfo.height = Height_textBox.Text;
            character.characterInfo.weight = Weight_textBox.Text;
            character.characterInfo.hairColor = Hair_textBox.Text;
            character.characterInfo.eyeColor = Eyes_textBox.Text;
            character.characterInfo.className = class_textbox.Text;
            character.characterInfo.currentXp = CurXp_textbox.Text;
            character.characterInfo.nextLevelXp = nextLevel_textbox.Text;
            character.characterInfo.saveCharacterInfo();
        }


        #region CharacterInfo tab functions

        //Change level based off xp
        private void CurXp_textbox_TextChanged(object sender, EventArgs e)
        {
            int curXp_int = 0;
            int nextLevel_int = 0;
            Int32.TryParse(CurXp_textbox.Text.Replace(",", ""), out curXp_int);
            Int32.TryParse(nextLevel_textbox.Text.Replace(",", ""), out nextLevel_int);
            Console.WriteLine("curxp = " + curXp_int);
            Console.WriteLine("next level = " + nextLevel_int);
            if (curXp_int == nextLevel_int)
            {
                //Eventually you can put in a number so large that it'll just reset back to 0
                // and curXp_int == nextLevel_int will be true again which will break the program
                // if you're level 20
                try
                {
                    characterLevel_comboBox.SelectedIndex += 1;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Congrats, you exceeded the max amount of XP and tried to break my program.");
                }
                CalculateNextLevel();
            }
            //Recalculate character level while curXp is greater than next level XP
            else if (curXp_int > nextLevel_int)
            {
                int characterLevel = 0;

                while (curXp_int > nextLevel_int)
                {
                    Int32.TryParse(characterLevel_comboBox.Text, out characterLevel);
                    Console.WriteLine("Character LeVEL " + characterLevel);
                    //Stop the loop if we are greater than or equal to level 20
                    if (characterLevel >= 20)
                    {
                        break;
                    }
                    //add 1 to the level
                    characterLevel_comboBox.SelectedIndex += 1;
                    //subtract 
                    curXp_int -= nextLevel_int;
                    //Recalculate
                    CalculateNextLevel();
                }
            }

        }

        #endregion

        #region Ability Tab functions

        #region Events
        //Update everything that relates to dex
        private void dex_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            dex_abilitymodifier_textbox.Text = CalculateModifier(dex_abilityScore_textbox.Text);


            //change the appropriate text around or whatever
            ac_dexMod_textbox.Text = dex_abilityScore_textbox.Text;

            initiative_dexModifier_textbox.Text = dex_abilitymodifier_textbox.Text.Replace("+", "");

            reflex_abilityMod_textbox.Text = dex_abilityScore_textbox.Text.Replace("+", ""); ;
            CalculateCMD();
        }
        private void con_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            con_abilitymodifier_textbox.Text = CalculateModifier(con_abilityScore_textbox.Text);

            fortitude_abilityMod_textbox.Text = con_abilityScore_textbox.Text.Replace("+", ""); ;
        }

        private void wis_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            wis_abilitymodifier_textbox.Text = CalculateModifier(wis_abilityScore_textbox.Text);
            will_abilityMod_textbox.Text = wis_abilityScore_textbox.Text.Replace("+", ""); ;
        }
        private void str_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            str_abilitymodifier_textbox.Text = CalculateModifier(str_abilityScore_textbox.Text);
            CalculateCMD();
            CalculateCMB();
        }

        private void int_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            int_abilitymodifier_textbox.Text = CalculateModifier(int_abilityScore_textbox.Text);
        }

        private void cha_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            cha_abilitymodifier_textbox.Text = CalculateModifier(cha_abilityScore_textbox.Text);
        }
        private void initiative_dexModifier_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateInitiative();
        }

        private void initiative_miscModifier_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateInitiative();
        }
        private void baseAttackBonus_amount_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateCMD();
            CalculateCMB();
        }
        //Show or hide mod panel
        private void mods_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mods_checkBox.Checked)
            {
                //acMods_Panel.Visible = true;
                acMods_Panel.BringToFront();
            }

            else
            {
                //acMods_Panel.Visible = false;
                acMods_Panel.SendToBack();
            }
        }

        private void savingThrow_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (savingThrow_checkbox.Checked)
            {
                savingThrows_panel.BringToFront();
            }

            else
            {
                savingThrows_panel.SendToBack();
            }
        }

        private void characterLevel_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateNextLevel();
        }

        private void XP_point_total_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateNextLevel();
        }

        private void Menu_Dropdown_label_MouseEnter(object sender, EventArgs e)
        {
            Menu_Dropdown_label.BackColor = Color.LightGray;
        }

        private void Menu_Dropdown_label_MouseLeave(object sender, EventArgs e)
        {
            Color newColor = new Color();
            newColor = Color.FromArgb(1111111);
            Menu_Dropdown_label.BackColor = newColor;
        }

        private void Menu_Dropdown_label_MouseClick(object sender, MouseEventArgs e)
        {
            ctxMenu.Show(Menu_Dropdown_label, 0, Menu_Dropdown_label.Height);
        }

        #endregion

        #region CALCULATIONS
        //I'm sleepy so this code kinda sucks
        //but call this to convert speed to feet
        private string ConvertSpeedToFeet(string speed)
        {
            int convert = 0;
            int x = 0;
            //convert speed string to int
            Int32.TryParse(speed, out convert);
            x = convert / 5;
            return x.ToString();
        }

        //Calculates AC for TotalAc, flatFooted, and Touch
        private void CalculateAC()
        {
            int totalAC = 0;
            int flatFooted = 0;
            int touch = 0;
            /*Convert shit*/
            int armorBonus, shieldBonus, sizeMod, naturalArmor, deflectionMod, miscMod, dexmod = 0;
            Int32.TryParse(ac_armorBonus_textbox.Text, out armorBonus);
            Int32.TryParse(ac_shieldBonus_textbox.Text, out shieldBonus);
            Int32.TryParse(ac_sizeMod_textbox.Text, out sizeMod);
            Int32.TryParse(ac_NaturalArmor_textbox.Text, out naturalArmor);
            Int32.TryParse(ac_deflectionMod_textbox.Text, out deflectionMod);
            Int32.TryParse(ac_miscMod_textbox.Text, out miscMod);
            Int32.TryParse(ac_dexMod_textbox.Text, out dexmod);

            //Possibly not calcualted right?
            totalAC = 10 + armorBonus + shieldBonus + dexmod + sizeMod + naturalArmor + deflectionMod + miscMod;
            flatFooted = 10 + armorBonus;
            touch = 10 + dexmod + sizeMod + deflectionMod + miscMod;

            //Make the labels the correct values
            acAmount_lbl.Text = totalAC.ToString();
            flatFooted_amount_lbl.Text = flatFooted.ToString();
            touchAmount_lbl.Text = touch.ToString();
        }

        //Calculates fortitude saving throw total
        private void CalculateFortSaves()
        {
            int total = 0;
            int baseSave, abilityMod, magicMod, miscMod, tempMod = 0;
            Int32.TryParse(fortitude_baseSave_textbox.Text, out baseSave);
            Int32.TryParse(fortitude_abilityMod_textbox.Text, out abilityMod);
            Int32.TryParse(fortitude_magicMod_textbox.Text, out magicMod);
            Int32.TryParse(fortitude_miscMod_textbox.Text, out miscMod);
            Int32.TryParse(fortitude_tempMod_textbox.Text, out tempMod);
            total = baseSave + abilityMod + magicMod + miscMod + tempMod;
            fortitudeSave_total_textbox.Text = total.ToString();
        }
        //Calculates Reflex saving throw total
        private void CalculateReflexSaves()
        {
            int total = 0;
            int baseSave, abilityMod, magicMod, miscMod, tempMod = 0;
            Int32.TryParse(reflex_baseSave_textbox.Text, out baseSave);
            Int32.TryParse(reflex_abilityMod_textbox.Text, out abilityMod);
            Int32.TryParse(reflex_magicMod_textbox.Text, out magicMod);
            Int32.TryParse(reflex_miscMod_textbox.Text, out miscMod);
            Int32.TryParse(reflex_tempMod_textbox.Text, out tempMod);
            total = baseSave + abilityMod + magicMod + miscMod + tempMod;
            reflexSave_total_textbox.Text = total.ToString();
        }
        //Calculates Will saving throw total
        private void CalculateWillSaves()
        {
            int total = 0;
            int baseSave, abilityMod, magicMod, miscMod, tempMod = 0;
            Int32.TryParse(will_baseSave_textbox.Text, out baseSave);
            Int32.TryParse(will_abilityMod_textbox.Text, out abilityMod);
            Int32.TryParse(will_magicMod_textbox.Text, out magicMod);
            Int32.TryParse(will_miscMod_textbox.Text, out miscMod);
            Int32.TryParse(will_tempMod_textbox.Text, out tempMod);
            total = baseSave + abilityMod + magicMod + miscMod + tempMod;
            willSave_total_textbox.Text = total.ToString();
        }
        //Calculates the ability modifier value
        private string CalculateModifier(string abilityScore)
        {
            int rawScore = 0;
            int output = 0;
            Int32.TryParse(abilityScore, out rawScore);
            output = (rawScore - 10) / 2;
            if (output < 0)
            {
                return output.ToString();
            }
            else
                return "+" + output.ToString();
        }
        //Calculate initiative value 
        private void CalculateInitiative()
        {
            int total = 0;
            int dexMod, miscMod = 0;
            Int32.TryParse(initiative_dexModifier_textbox.Text, out dexMod);
            Int32.TryParse(initiative_miscModifier_textbox.Text, out miscMod);
            total = dexMod + miscMod;
            initiativeTotal_lbl.Text = total.ToString();
        }

        //Calculates CMD value

        private void CalculateCMD()
        {
            //CMD = Base Attack Bonus + strength mod + size mod + dexMod+base mod(10)
            int cmd = 0;
            int baseAttackBonus, strMod, sizeMod = 0, dexMod = 0;
            int baseMod = 10;
            Int32.TryParse(baseAttackBonus_amount_textbox.Text, out baseAttackBonus);
            Int32.TryParse(str_abilitymodifier_textbox.Text, out strMod);
            //Int32.TryParse(, out ); //Don't worry about size mod?
            Int32.TryParse(dex_abilitymodifier_textbox.Text, out dexMod);
            cmd = baseAttackBonus + strMod + sizeMod + dexMod + baseMod;

            CMD_amount_textbox.Text = cmd.ToString();
        }
        //Calculates CMB value
        private void CalculateCMB()
        {
            //CMB = BaseAttackBonus + strength + sizeMod
            int cmb = 0;
            int baseAttackBonus, strMod, sizeMod = 0;
            Int32.TryParse(baseAttackBonus_amount_textbox.Text, out baseAttackBonus);
            Int32.TryParse(str_abilitymodifier_textbox.Text, out strMod);
            //Int32.TryParse(, out ); //Don't worry about size mod?
            cmb = baseAttackBonus + strMod + sizeMod;

            CMB_amount_textbox.Text = cmb.ToString();
        }

        //Calculates the experience needed for the next level
        private void CalculateNextLevel()
        {

            string currentProgressionRate = XP_point_total_comboBox.Text;
            string currentLevel = characterLevel_comboBox.Text;
            string nextLevelXP = "";

            /*Might change this logic to switch statements?*/
            //If we are progressing slow 
            if (currentProgressionRate == "Slow")
            {
                //If slow dict contains current level
                if (slow.ContainsKey(currentLevel))
                {
                    nextLevelXP = slow[currentLevel];
                }
            }
            //If we are progressing medium
            else if (currentProgressionRate == "Medium")
            {
                //If slow dict contains current level
                if (medium.ContainsKey(currentLevel))
                {
                    //next level xp = value at currentLevel key
                    nextLevelXP = medium[currentLevel];
                }
            }

            //If we are progressing fast
            else if (currentProgressionRate == "Fast")
            {
                //If slow dict contains current level
                if (fast.ContainsKey(currentLevel))
                {
                    //next level xp = value at currentLevel key
                    nextLevelXP = fast[currentLevel];
                }
            }

            nextLevel_textbox.Text = nextLevelXP;
        }
        #endregion

        #region OTHER FUNCTIONS
        //Creates a panel, with lots of components inside of it then adds that panel to a list
        /*NEED to do
         * Create skill objects to house all the info for the panel 
         */
        public void CreateSkillPanel(Skill skill)
        {
            MetroFramework.Controls.MetroPanel skill_panel = new MetroFramework.Controls.MetroPanel();
            MetroFramework.Controls.MetroCheckBox classSkill_checkbox = new MetroFramework.Controls.MetroCheckBox();
            MetroFramework.Controls.MetroLabel skillName_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel totalBonus_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel equalSign1_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel abilityType_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel AbilityMod_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel PlusSign1_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel PlusSign2_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel ranks_label = new MetroFramework.Controls.MetroLabel();
            MetroFramework.Controls.MetroLabel MiscMod_label = new MetroFramework.Controls.MetroLabel();

            // 
            // SkillPanelTemplate
            // 
            skill_panel.SuspendLayout();
            skill_panel.Controls.Add(MiscMod_label);
            skill_panel.Controls.Add(PlusSign2_label);
            skill_panel.Controls.Add(ranks_label);
            skill_panel.Controls.Add(PlusSign1_label);
            skill_panel.Controls.Add(AbilityMod_label);
            skill_panel.Controls.Add(abilityType_label);
            skill_panel.Controls.Add(equalSign1_label);
            skill_panel.Controls.Add(totalBonus_label);
            skill_panel.Controls.Add(skillName_label);
            skill_panel.Controls.Add(classSkill_checkbox);
            skill_panel.HorizontalScrollbarBarColor = true;
            skill_panel.HorizontalScrollbarHighlightOnWheel = false;
            skill_panel.HorizontalScrollbarSize = 10;
            skill_panel.Location = new System.Drawing.Point(3, 62);
            skill_panel.Name = "SkillPanelTemplate";
            skill_panel.Size = new System.Drawing.Size(422, 35);
            skill_panel.TabIndex = 2;
            skill_panel.VerticalScrollbarBarColor = true;
            skill_panel.VerticalScrollbarHighlightOnWheel = false;
            skill_panel.VerticalScrollbarSize = 10;
            skill_panel.Theme = MetroFramework.MetroThemeStyle.Dark;
            skill_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // checkBoxTemplate
            // 
            //classSkill_checkbox.AutoSize = true;
            classSkill_checkbox.AutoSize = checkBoxTemplate.AutoSize;
            classSkill_checkbox.Location = checkBoxTemplate.Location;
            classSkill_checkbox.Name = checkBoxTemplate.Name;
            classSkill_checkbox.Size = checkBoxTemplate.Size;
            classSkill_checkbox.TabIndex = checkBoxTemplate.TabIndex;
            classSkill_checkbox.Text = checkBoxTemplate.Text;
            classSkill_checkbox.UseSelectable = checkBoxTemplate.UseSelectable;
            classSkill_checkbox.Theme = MetroFramework.MetroThemeStyle.Dark; 

            // 
            // SkillNameTemplate
            // 
            //skillName_label.AutoSize = true;
            skillName_label.BorderStyle = SkillNameTemplate.BorderStyle;
            skillName_label.Location = SkillNameTemplate.Location;
            skillName_label.Name = SkillNameTemplate.Name;
            skillName_label.Size = SkillNameTemplate.Size;
            skillName_label.TabIndex = SkillNameTemplate.TabIndex;
            skillName_label.Text = SkillNameTemplate.Text;
            skillName_label.TextAlign = SkillNameTemplate.TextAlign;

            skillName_label.Theme = MetroFramework.MetroThemeStyle.Dark;


            // 
            // TotalBonusTemplate
            // 
            totalBonus_label.AutoSize = TotalBonusTemplate.AutoSize;
            totalBonus_label.Location = TotalBonusTemplate.Location;
            totalBonus_label.Name = TotalBonusTemplate.Name;
            totalBonus_label.Size = TotalBonusTemplate.Size;
            totalBonus_label.TabIndex = TotalBonusTemplate.TabIndex;
            totalBonus_label.Text = TotalBonusTemplate.Text;
            totalBonus_label.BorderStyle = TotalBonusTemplate.BorderStyle;
            totalBonus_label.TextAlign = TotalBonusTemplate.TextAlign;

            totalBonus_label.Theme = MetroFramework.MetroThemeStyle.Dark;

            // 
            // EqualSignTemplate
            // 
            //equalSign1_label.AutoSize = true;
            equalSign1_label.AutoSize = EqualSignTemplate.AutoSize;
            equalSign1_label.Location = EqualSignTemplate.Location;
            equalSign1_label.Name = EqualSignTemplate.Name;
            equalSign1_label.Size = EqualSignTemplate.Size;
            equalSign1_label.TabIndex = EqualSignTemplate.TabIndex;
            equalSign1_label.Text = EqualSignTemplate.Text;

            equalSign1_label.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // abilityTypeTemplate
            // 
            abilityType_label.AutoSize = abilityTypeTemplate.AutoSize;
            abilityType_label.Location = abilityTypeTemplate.Location;
            abilityType_label.Name = abilityTypeTemplate.Name;
            abilityType_label.Size = abilityTypeTemplate.Size;
            abilityType_label.TabIndex = abilityTypeTemplate.TabIndex;
            abilityType_label.Text = abilityTypeTemplate.Text;
            abilityType_label.TextAlign = abilityTypeTemplate.TextAlign;

            abilityType_label.Theme = MetroFramework.MetroThemeStyle.Dark;

            // 
            // AbilityModTemplate
            // 
            AbilityMod_label.AutoSize = AbilityModTemplate.AutoSize;
            AbilityMod_label.Location = AbilityModTemplate.Location;
            AbilityMod_label.Name = AbilityModTemplate.Name;
            AbilityMod_label.Size = AbilityModTemplate.Size;
            AbilityMod_label.TabIndex = AbilityModTemplate.TabIndex;
            AbilityMod_label.Text = AbilityModTemplate.Text;
            AbilityMod_label.BorderStyle = AbilityModTemplate.BorderStyle;
            AbilityMod_label.TextAlign = AbilityModTemplate.TextAlign;

            AbilityMod_label.Theme = MetroFramework.MetroThemeStyle.Dark;

            // 
            // PlusSignTemplate1
            // 
            PlusSign1_label.AutoSize = PlusSignTemplate1.AutoSize;
            PlusSign1_label.Location = PlusSignTemplate1.Location;
            PlusSign1_label.Name = PlusSignTemplate1.Name;
            PlusSign1_label.Size = PlusSignTemplate1.Size;
            PlusSign1_label.TabIndex = PlusSignTemplate1.TabIndex;
            PlusSign1_label.Text = PlusSignTemplate1.Text;

            PlusSign1_label.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // PlusSignTemplate2
            // 
            PlusSign2_label.AutoSize = PlusSignTemplate2.AutoSize;
            PlusSign2_label.Location = PlusSignTemplate2.Location;
            PlusSign2_label.Name = PlusSignTemplate2.Name;
            PlusSign2_label.Size = PlusSignTemplate2.Size;
            PlusSign2_label.TabIndex = PlusSignTemplate2.TabIndex;
            PlusSign2_label.Text = PlusSignTemplate2.Text;

            PlusSign2_label.Theme = MetroFramework.MetroThemeStyle.Dark; 
            // 
            // ranksTemplate
            // 
            ranks_label.AutoSize = ranks_Template.AutoSize;
            ranks_label.BorderStyle = ranks_Template.BorderStyle;
            ranks_label.Location = ranks_Template.Location;
            ranks_label.Name = ranks_Template.Name;
            ranks_label.Size = ranks_Template.Size;
            ranks_label.TabIndex = ranks_Template.TabIndex;
            ranks_label.Text = ranks_Template.Text;
            ranks_label.TextAlign = ranks_Template.TextAlign;
            ranks_label.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // MiscModTemplate
            // 
            MiscMod_label.AutoSize = miscMod_template.AutoSize;
            MiscMod_label.BorderStyle = miscMod_template.BorderStyle;
            MiscMod_label.Location = miscMod_template.Location;
            MiscMod_label.Name = miscMod_template.Name;
            MiscMod_label.Size = miscMod_template.Size;
            MiscMod_label.TabIndex = miscMod_template.TabIndex;
            MiscMod_label.Text = miscMod_template.Text;
            MiscMod_label.TextAlign = miscMod_template.TextAlign;

            MiscMod_label.Theme = MetroFramework.MetroThemeStyle.Dark;

            skill_panel.ResumeLayout(false);
            skill_panel.PerformLayout();
            this.Skills_TabPage.Controls.Add(skill_panel);
            // 
            // Set the info
            // 
            skillName_label.Text = skill.skillName;
            totalBonus_label.Text = skill.totalBonus;
            abilityType_label.Text = skill.modType;
            AbilityMod_label.Text = skill.abilityMod;
            ranks_label.Text = skill.ranks;
            MiscMod_label.Text = skill.miscMod;



            list_skillsPanels.Add(skill_panel);
        }

        public void PlacePanels()
        {
            //Don't place the first one 
            if (list_skillsPanels.Count > 1)
            {
                Point newLocation = new Point(list_skillsPanels[list_skillsPanels.Count - 2].Location.X, list_skillsPanels[list_skillsPanels.Count - 2].Location.Y + 41);
                list_skillsPanels[list_skillsPanels.Count - 1].Location = newLocation;
            }
        }


        #endregion

        #region speed Text Change stuff

        private void baseSpeed_feet_textbox_TextChanged(object sender, EventArgs e)
        {
            baseSpeed_squares_lbl.Text = ConvertSpeedToFeet(baseSpeed_feet_textbox.Text);
        }

        private void armorSpeed_feet_textbox_TextChanged(object sender, EventArgs e)
        {
            armorSpeed_squares_lbl.Text = ConvertSpeedToFeet(armorSpeed_feet_textbox.Text);
        }

        private void flySpeed_feet_textbox_TextChanged(object sender, EventArgs e)
        {
            flySpeed_squares_lbl.Text = ConvertSpeedToFeet(flySpeed_feet_textbox.Text);
        }

        private void swimSpeed_feet_textbox_TextChanged(object sender, EventArgs e)
        {
            swimSpeed_squares_lbl.Text = ConvertSpeedToFeet(swimSpeed_feet_textbox.Text);
        }

        private void climbSpeed_feet_textbox_TextChanged(object sender, EventArgs e)
        {
            climbSpeed_squares_lbl.Text = ConvertSpeedToFeet(climbSpeed_feet_textbox.Text);
        }

        private void burrowSpeed_feet_textbox_TextChanged(object sender, EventArgs e)
        {
            burrowSpeed_squares_label.Text = ConvertSpeedToFeet(burrowSpeed_feet_textbox.Text);
        }



        #endregion

        #region AC text change stuff
        private void ac_armorBonus_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateAC();
        }

        private void ac_shieldBonus_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateAC();
        }

        private void ac_dexMod_textbox_TextChanged(object sender, EventArgs e)
        {
            ac_dexMod_textbox.Text = dex_abilitymodifier_textbox.Text.Replace("+", "");
            CalculateAC();
        }

        private void ac_sizeMod_textbox_TextChanged(object sender, EventArgs e)
        {

            CalculateAC();
        }

        private void ac_NaturalArmor_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateAC();
        }

        private void ac_deflectionMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateAC();
        }

        private void ac_miscMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateAC();
        }

        #endregion

        #region Saving throw text change stuff
        /*FORTITUDE*/
        private void fortitude_bseSave_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateFortSaves();
        }

        private void fortitude_abilityMod_textbox_TextChanged(object sender, EventArgs e)
        {
            fortitude_abilityMod_textbox.Text = con_abilitymodifier_textbox.Text.Replace("+", ""); ;
            CalculateFortSaves();
        }

        private void fortitude_magicMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateFortSaves();
        }

        private void fortitude_miscMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateFortSaves();
        }

        private void fortitude_tempMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateFortSaves();
        }


        /*REFLEX*/
        private void reflex_baseSave_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateReflexSaves();
        }

        private void reflex_abilityMod_textbox_TextChanged(object sender, EventArgs e)
        {
            reflex_abilityMod_textbox.Text = dex_abilitymodifier_textbox.Text.Replace("+", "");
            CalculateReflexSaves();
        }

        private void reflex_magicMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateReflexSaves();
        }

        private void reflex_miscMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateReflexSaves();
        }

        private void reflex_tempMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateReflexSaves();
        }


        /*WISDOM*/
        private void will_baseSave_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateWillSaves();
        }

        private void will_abilityMod_textbox_TextChanged(object sender, EventArgs e)
        {
            will_abilityMod_textbox.Text = wis_abilitymodifier_textbox.Text.Replace("+", ""); ;
            CalculateWillSaves();
        }

        private void will_magicMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateWillSaves();
        }

        private void will_miscMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateWillSaves();
        }

        private void will_tempMod_textbox_TextChanged(object sender, EventArgs e)
        {
            CalculateWillSaves();
        }

        #endregion

        #endregion


        private void add_skill_panel_Click(object sender, EventArgs e)
        {
            //Temp info to see if it works 
            Skill aSkill = new Skill();
            //add panel
            CreateSkillPanel(aSkill);
            //rearrange panels
            PlacePanels();
        }
    }
}

