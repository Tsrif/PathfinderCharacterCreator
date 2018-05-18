namespace PathfinderApp
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.newChar_Button = new MetroFramework.Controls.MetroButton();
            this.loadChar_Button = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(72, 216);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(334, 19);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Please create a new character or load in an existing one";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // newChar_Button
            // 
            this.newChar_Button.AutoSize = true;
            this.newChar_Button.BackColor = System.Drawing.Color.White;
            this.newChar_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("newChar_Button.BackgroundImage")));
            this.newChar_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.newChar_Button.Location = new System.Drawing.Point(95, 267);
            this.newChar_Button.Name = "newChar_Button";
            this.newChar_Button.Size = new System.Drawing.Size(75, 78);
            this.newChar_Button.TabIndex = 6;
            this.newChar_Button.UseSelectable = true;
            this.newChar_Button.Click += new System.EventHandler(this.newChar_Button_Click);
            // 
            // loadChar_Button
            // 
            this.loadChar_Button.AutoSize = true;
            this.loadChar_Button.BackColor = System.Drawing.Color.White;
            this.loadChar_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loadChar_Button.BackgroundImage")));
            this.loadChar_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loadChar_Button.Location = new System.Drawing.Point(313, 267);
            this.loadChar_Button.Name = "loadChar_Button";
            this.loadChar_Button.Size = new System.Drawing.Size(75, 78);
            this.loadChar_Button.TabIndex = 7;
            this.loadChar_Button.UseSelectable = true;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 450);
            this.Controls.Add(this.loadChar_Button);
            this.Controls.Add(this.newChar_Button);
            this.Controls.Add(this.metroLabel1);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Name = "StartForm";
            this.Text = "Pathfinder Character Creator";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton loadChar_Button;
        private MetroFramework.Controls.MetroButton newChar_Button;
    }
}