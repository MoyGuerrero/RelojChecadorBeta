using DPFP.Processing;
using DPFP;
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
using System.IO;
using RelojChecadorBeta.Model;
using System.Data.Entity;

namespace RelojChecadorBeta
{
    public partial class MyForm : RelojChecadorBeta.DigitalPersonal.DigitalPersonal
    {
        private Timer timer;
        private bool isView = true;
        private DPFP.Verification.Verification verification;
        private DPFP.Template template1;
        private RelojChecadorBetaEntities1 context;

        public MyForm()
        {
            context = new RelojChecadorBetaEntities1();
            verification = new DPFP.Verification.Verification();
            timer = new Timer();
            timer.Tick += new EventHandler(Form1_Load);
            InitializeComponent();
            timer.Enabled = true;
            this.setSample += sampleFinger;
            this.OnFingerprintCaptured += IsViewFingerPrinters;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximunForm();
            Reloj();
            isViewPicture();
            Start();
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

        public void IsViewFingerPrinters(Bitmap bitmap)
        {
            pictureBox1.Image = new Bitmap(bitmap, pictureBox1.Size);
        }

        private void imageEmploye_DoubleClick(object sender, EventArgs e)
        {
            timer.Stop();
            Stop();
            this.Hide();
            RegisterEmployed register = new RegisterEmployed();
            register.ShowDialog();
        }

        public void correrChecador(bool iniciar)
        {
            if (!iniciar)
            {
                timer.Start();
                Start();
                this.Show();
            }
        }

        public void sampleFinger(DPFP.Sample Sample)
        {
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            if (features != null)
            {
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                var listaHuellaConEmpleados = context.Huella.Include(finger => finger.Empleado).ToList();

                DPFP.Template template = new DPFP.Template();
                Stream stream;
                foreach (var h in listaHuellaConEmpleados)
                {
                    stream = new MemoryStream(h.Huella1);
                    template = new DPFP.Template(stream);

                    verification.Verify(features, template, ref result);

                    if (result.Verified)
                    {
                        var id = h.Empleado.Id;
                        DateTime hora1 = new DateTime(1, 1, 1, 20, 33, 0);
                        DateTime registro = DateTime.Now;
                        TimeSpan diferencia = TimeSpan.Zero;
                        context.Registro.Add(new Registro
                        {
                            Registro1 = registro,
                            IdEmpleado = id
                        });
                        if (registro > hora1)
                        {
                            diferencia = registro - hora1;
                        }
                        context.SaveChanges();
                        MessageBox.Show("Se Encontro" + id + "  Minutos: " + diferencia.TotalMinutes, "Bien", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
            }
        }

        private FeatureSet ExtractFeatures(Sample sample, DataPurpose enrollment)
        {
            DPFP.Processing.FeatureExtraction extraction = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet feature = new DPFP.FeatureSet();
            extraction.CreateFeatureSet(sample, enrollment, ref feedback, ref feature);
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return feature;
            else
                return null;

        }

        private void MyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.FormClosed();
        }
    }
}
