using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelojChecadorBeta
{
    public partial class Form1 : Form
    {
        private Timer timer;
        public Form1()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(Form1_Load);
            InitializeComponent();
            timer.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximunForm();
            Reloj();
            isViewPicture();
        }


        public void MaximunForm()
        {
            this.WindowState = FormWindowState.Maximized;
        }


        public void Reloj()
        {
            lblReloj.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        public void isViewPicture()
        {
            try
            {
                imageEmploye.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\no-image.png";
                imageEmploye.SizeMode = PictureBoxSizeMode.StretchImage;
                // Crear un borde redondeado para el PictureBox
                int borderRadius = 20; // Radio de los bordes redondeados
                var graphicsPath = new GraphicsPath();
                graphicsPath.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
                graphicsPath.AddArc(imageEmploye.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
                graphicsPath.AddArc(imageEmploye.Width - borderRadius, imageEmploye.Height - borderRadius, borderRadius, borderRadius, 0, 90);
                graphicsPath.AddArc(0, imageEmploye.Height - borderRadius, borderRadius, borderRadius, 90, 90);
                graphicsPath.CloseAllFigures();

                // Asignar la región recortada al PictureBox
                imageEmploye.Region = new Region(graphicsPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
