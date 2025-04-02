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
        private RelojChecador context;
        int intentos = 0;
        bool noFount = true;

        public MyForm()
        {
            context = new RelojChecador();
            verification = new DPFP.Verification.Verification();
            timer = new Timer();
            timer.Tick += new EventHandler(Form1_Load);
            InitializeComponent();
            isViewPicture();
            timer.Enabled = true;
            this.setSample += sampleFinger;
            this.OnFingerprintCaptured += IsViewFingerPrinters;
            this.isNotConnect += statusFinger;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximunForm();
            Reloj();
            Start();
        }


        public void MaximunForm()
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public void statusFinger(bool connect)
        {
            if (!connect)
            {
                Stop();
                this.Invoke((MethodInvoker)delegate
                {
                    txtEmployeCode.Visible = true;
                    lblEstatus.Text = "Favor de ingresar el codigo de empleado.";
                    txtEmployeCode.Focus();

                });
            }
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
            //pictureBox1.Image = new Bitmap(bitmap, pictureBox1.Size);
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

            if (intentos <= 2)
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

                        if (result.Verified && h.Empleado.Activo == 1)
                        {
                            var id = h.Empleado.Id;

                            DateTime registro = DateTime.Now;
                            DateTime horaLocal = DateTime.Now;

                            context.Registro.Add(new Registro
                            {
                                Fecha = registro.Date,
                                Hora = horaLocal.TimeOfDay,
                                IdEmpleado = id
                            });

                            context.SaveChanges();
                            //MessageBox.Show($"Bienvenido {h.Empleado.Nombre} {h.Empleado.ApellidoPaterno} {h.Empleado.ApellidoMaterno}");
                            //Estatus($"Bienvenido {h.Empleado.Nombre} {h.Empleado.ApellidoPaterno} {h.Empleado.ApellidoMaterno}", Color.Red);
                            //pictureBox1.Image = null;
                            pictureBox1.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\check.png";
                            imageEmploye.ImageLocation = h.Empleado.Foto;
                            intentos = 0;
                            this.Invoke((MethodInvoker)delegate
                            {
                                Estatus($"Bienvenido {h.Empleado.Nombre} {h.Empleado.ApellidoPaterno} {h.Empleado.ApellidoMaterno}", Color.White);

                            });
                            break;
                        }
                        else
                        {
                            pictureBox1.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\Error.png";

                            this.Invoke((MethodInvoker)delegate
                            {
                                Estatus($"No se encontro el usuario.", Color.Red);

                            });
                            break;
                        }

                    }

                    intentos++;
                    return;
                }
            }
            else
            {
                Stop();
                this.Invoke((MethodInvoker)delegate
                {
                    lblEstatus.Text = "Favor de ingresar el codigo de empleado.";
                    txtEmployeCode.Visible = true;
                    txtEmployeCode.Focus();
                });
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

        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult result = MessageBox.Show("Desea salir del sistema?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    e.Cancel = false;
            timer.Stop();
            Application.Exit();
            //}
            //else if (result == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
        }

        private void RegistroCodigo(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                string codigo = txtEmployeCode.Text;


                if (codigo.Trim().Length == 0)
                {
                    pictureBox1.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\Error.png";
                    Estatus("Por favor ingrese su numero de empleado.", Color.Red);
                    return;
                }

                if (!codigo.ToUpper().StartsWith("E"))
                {
                    pictureBox1.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\Error.png";
                    Estatus("El número de empleado comienza con la letra E", Color.Red);
                    return;
                }

                if (codigo.Length < 7 || codigo.Length >= 8)
                {
                    pictureBox1.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\Error.png";
                    Estatus("El codigo de empleado solo debe de tener 7 digitos", Color.Red);
                    return;
                }


                var empleado = context.Empleado.FirstOrDefault(employee => employee.CodigoEmpleado == codigo);

                if (empleado is null || empleado.Activo == 0)
                {
                    pictureBox1.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\Error.png";
                    Estatus($"No se encontro el empleado con el numero {codigo}", Color.Red);
                    return;
                }

                DateTime registro = DateTime.Now;
                DateTime horaLocal = DateTime.Now;

                pictureBox1.ImageLocation = empleado.Foto;

                context.Registro.Add(new Registro
                {
                    Fecha = registro.Date,
                    Hora = horaLocal.TimeOfDay,
                    IdEmpleado = empleado.Id
                });

                context.SaveChanges();
                imageEmploye.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\check.png";
                Estatus($"Bienvenido {empleado.Nombre} {empleado.ApellidoPaterno} {empleado.ApellidoMaterno}", Color.White);
                txtEmployeCode.Text = "";
                intentos = 0;
                txtEmployeCode.Visible = false;
                lblEstatus.Text = "Favor de colocar el dedo en el lector";
            }
        }

        private void Estatus(string messageError, Color color)
        {
            if (lblMessageError.InvokeRequired)
            {
                lblMessageError.Invoke((MethodInvoker)delegate
                {
                    Estatus(messageError, color);
                });
                return;
            }
            lblMessageError.ForeColor = color;
            lblMessageError.Text = messageError;

            var clearMessage = new System.Windows.Forms.Timer();


            clearMessage.Interval = 3000;

            clearMessage.Tick += (sender, e) =>
            {
                lblMessageError.Text = "";
                pictureBox1.ImageLocation = "";
                imageEmploye.ImageLocation = @"C:\Users\elzio\source\repos\RelojChecadorBeta\RelojChecadorBeta\Image\no-image.png";
                clearMessage.Stop();
            };
            clearMessage.Start();
        }
    }
}
