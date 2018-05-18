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
    public partial class StartForm : MetroFramework.Forms.MetroForm
    {
        clsResize _form_resize;

        public StartForm()
        {
            InitializeComponent();
            _form_resize = new clsResize(this);
            this.Load += _Load;
            this.Resize += _Resize;
        }

        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            _form_resize._resize();
        }

        private void newChar_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            CharacterForm characterForm = new CharacterForm();
            characterForm.Show();
        }
    }
}
