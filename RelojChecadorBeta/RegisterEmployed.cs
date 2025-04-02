using DPFP;
using DPFP.Processing;
using RelojChecadorBeta.DigitalPersonal;
using RelojChecadorBeta.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelojChecadorBeta
{
    public partial class RegisterEmployed : RelojChecadorBeta.DigitalPersonal.DigitalPersonal
    {
        private Enrollment Enrollment;
        public event Action<bool> ReiniciarEvento;
        private Template template;
        private RelojChecador context;
        private Empleado empleado;
        public RegisterEmployed()
        {
            InitializeComponent();
            Enrollment = new DPFP.Processing.Enrollment();
            lblStatus.Visible = false;
            picRegisterHuella.Visible = false;
            btnGuardar.Visible = false;
            this.setSample += sampleFinger;
            this.OnFingerprintCaptured += IsViewFingerPrinters;
        }

        private void btnCapturarHuella_Click(object sender, EventArgs e)
        {
            btnCapturarHuella.Enabled = false;
            txtCodigoEmpleado.Enabled = false;
            string codigo = txtCodigoEmpleado.Text.ToUpper().Trim();

            if (codigo.Length == 0)
            {
                clearMessage("El campo codigo empleado no puede estar vacio.", Color.Red);
                return;
            }

            if (!codigo.StartsWith("E"))
            {
                clearMessage("El codigo empleado debe iniciar con la letra E", Color.Red);
                return;
            }

            if (codigo.Length < 7 || codigo.Length >= 8)
            {

                clearMessage("El codigo empleado debe tener 7 caracteres", Color.Red);
                return;
            }

            empleado = context.Empleado.FirstOrDefault(emp => emp.CodigoEmpleado == codigo);


            if (empleado is null || empleado.Activo == 0)
            {
                clearMessage("EL usuario no existe o esta dado de baja", Color.Red);
                return;
            }
            lblStatus.Visible = true;
            picRegisterHuella.Visible = true;
            UpdateStatus();
            Start();
        }

        public void IsViewFingerPrinters(Bitmap bitmap)
        {
            picRegisterHuella.Image = new Bitmap(bitmap, picRegisterHuella.Size);
        }

        public void sampleFinger(DPFP.Sample Sample)
        {
            DPFP.FeatureSet feature = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if (feature != null) try
                {
                    Enrollment.AddFeatures(feature);
                }
                finally
                {
                    UpdateStatus();
                    switch (Enrollment.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:
                            MessageBox.Show("Huellas capturadas con éxito.");
                            IsViewButton();
                            template = Enrollment.Template;
                            //MessageBox.Show("Listo!");
                            Stop();
                            break;
                        case DPFP.Processing.Enrollment.Status.Failed:
                            Enrollment.Clear();
                            Stop();
                            UpdateStatus();
                            Start();
                            break;
                    }
                }
        }

        private void UpdateStatus()
        {
            if (lblStatus.InvokeRequired)  // Si el control fue creado en otro hilo
            {
                lblStatus.Invoke(new Action(UpdateStatus));  // Ejecuta en el hilo principal
            }
            else
            {
                lblStatus.Text = String.Format("Favor de color tu dedo al lector {0} veces", Enrollment.FeaturesNeeded);
            }
        }


        private void IsViewButton()
        {
            if (btnGuardar.InvokeRequired)
            {
                btnGuardar.Invoke(new Action(IsViewButton));
            }
            else
            {
                btnGuardar.Visible = true;
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

        private void RegisterEmployed_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Stop();
            MyForm myForm = new MyForm();
            myForm.correrChecador(false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Stop();
            byte[] streanFingerPrinters = template.Bytes;
            txtByte.Text = BitConverter.ToString(streanFingerPrinters);
            //MessageBox.Show(txtCodigoEmpleado.Text);

            var codigoEmpleado = txtCodigoEmpleado.Text;
            guardarHuella(codigoEmpleado, streanFingerPrinters);
        }

        private void guardarHuella(string codigoEmpleado, byte[] streanFingerPrinters)
        {

            try
            {

                var isExistPrinterFingers = context.Huella.Any(e => e.Huella1 == streanFingerPrinters);


                if (isExistPrinterFingers)
                {
                    MessageBox.Show("El registro de la huella ya existe. Favor de intentar con otro dedo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                context.Huella.Add(new Huella
                {
                    Huella1 = streanFingerPrinters,
                    IdEmpleado = empleado.Id
                });

                context.SaveChanges();

                txtCodigoEmpleado.Enabled = true;
                btnCapturarHuella.Enabled = true;
                txtCodigoEmpleado.Text = "";

                MessageBox.Show($"El registro del empleado con codigo {codigoEmpleado} se ha guardado con éxito.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RegisterEmployed_Load(object sender, EventArgs e)
        {

            context = new RelojChecador();
        }

        private void clearMessage(string message, Color color)
        {

            lblMensajeError.Text = message;
            lblMensajeError.ForeColor = color;
            var clearMessage = new System.Windows.Forms.Timer();


            clearMessage.Interval = 3000;

            clearMessage.Tick += (sender, e) =>
            {
                btnCapturarHuella.Enabled = true;
                txtCodigoEmpleado.Enabled = true;
                txtCodigoEmpleado.Focus();
                lblMensajeError.Text = "";
                clearMessage.Stop();
            };
            clearMessage.Start();
        }
    }
}
