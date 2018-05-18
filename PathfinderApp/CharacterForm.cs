using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PathfinderApp
{
    public partial class CharacterForm : MetroFramework.Forms.MetroForm
    {
        Character character = new Character();

        public CharacterForm()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            this.StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroStyleExtender1.SetApplyMetroTheme(MenuStrip, true);
            this.Character_TabControl.SelectedIndex = 0;

            //change the default selected values on the combo boxes
            Allignment_comboBox.SelectedIndex = 0;
            characterLevel_comboBox.SelectedIndex = 0;
            size_comboBox.SelectedIndex = 0;
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
            character.characterInfo.gender = Gender_textBox.Text;
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
            if (CurXp_textbox.Text == nextLevel_textbox.Text)
            {
                characterLevel_comboBox.SelectedIndex += 1;
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
            
            initiative_dexModifier_textbox.Text = dex_abilitymodifier_textbox.Text;

            reflex_abilityMod_textbox.Text = dex_abilityScore_textbox.Text;
            CalculateCMD();
        }
        private void con_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            con_abilitymodifier_textbox.Text = CalculateModifier(con_abilityScore_textbox.Text);

            fortitude_abilityMod_textbox.Text = con_abilityScore_textbox.Text;
        }

        private void wis_abilityScore_textbox_TextChanged(object sender, EventArgs e)
        {
            //calculate ability mod
            wis_abilitymodifier_textbox.Text = CalculateModifier(wis_abilityScore_textbox.Text);
            will_abilityMod_textbox.Text = wis_abilityScore_textbox.Text;
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

        private void CalculateFortSaves() {
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

        private void CalculateReflexSaves() {
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

        private void CalculateWillSaves() {
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

        private string CalculateModifier(string abilityScore) {
            int rawScore = 0;
            int output = 0;
            Int32.TryParse(abilityScore, out rawScore);
            output = (rawScore - 10) / 2;
            if (output < 0) {
                return output.ToString();
            }
            else 
            return "+" + output.ToString();
        }

        private void CalculateInitiative() {
            int total = 0;
            int dexMod, miscMod = 0;
            Int32.TryParse(initiative_dexModifier_textbox.Text, out dexMod);
            Int32.TryParse(initiative_miscModifier_textbox.Text, out miscMod);
            total = dexMod + miscMod;
            initiativeTotal_lbl.Text = total.ToString();
        }

        private void CalculateCMD() {
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

        private void CalculateCMB() {
            //CMB = BaseAttackBonus + strength + sizeMod
            int cmb = 0;
            int baseAttackBonus, strMod, sizeMod = 0;
            Int32.TryParse(baseAttackBonus_amount_textbox.Text, out baseAttackBonus);
            Int32.TryParse(str_abilitymodifier_textbox.Text, out strMod);
            //Int32.TryParse(, out ); //Don't worry about size mod?
            cmb = baseAttackBonus + strMod + sizeMod;

            CMB_amount_textbox.Text = cmb.ToString();
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
            ac_dexMod_textbox.Text = dex_abilitymodifier_textbox.Text;
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
            fortitude_abilityMod_textbox.Text = con_abilitymodifier_textbox.Text;
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
            reflex_abilityMod_textbox.Text = dex_abilitymodifier_textbox.Text;
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
            will_abilityMod_textbox.Text = wis_abilitymodifier_textbox.Text;
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

        
    }
}
