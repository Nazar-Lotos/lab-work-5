using System;
using System.Windows.Forms;

namespace lab_work_5
{
    public partial class fTelevision : Form
    {
        public Television TheTV;
        public fTelevision(Television TV)
        {
            InitializeComponent();
            TheTV = TV;
        }
        public fTelevision(ref Television TV)
        {
            InitializeComponent();
            TheTV = TV;
        }
        private void label1_Click(object sender, EventArgs e){}

        private void btnOk_Click(object sender, EventArgs e)
        {
            TheTV.Brand = tbBrand.Text.Trim();
            TheTV.Model = tbModel.Text.Trim();
            TheTV.ScreenSize = int.Parse(tbScreenSize.Text.Trim());
            TheTV.Resolution = tbResolution.Text.Trim();
            TheTV.IsSmartTV = bool.Parse(tbSmartTV.Text.Trim());
            TheTV.SoundPower = int.Parse(tbSoundPower.Text.Trim());

            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e){DialogResult = DialogResult.Cancel;}

        private void fTelevision_Load(object sender, EventArgs e)
        {
            if(TheTV != null)
            {
                tbBrand.Text = TheTV.Brand;
                tbModel.Text = TheTV.Model;
                tbScreenSize.Text = TheTV.ScreenSize.ToString();
                tbResolution.Text = TheTV.Resolution;
                tbSmartTV.Text = TheTV.IsSmartTV.ToString();
                tbSoundPower.Text = TheTV.SoundPower.ToString();
            }
        }
    }
}
