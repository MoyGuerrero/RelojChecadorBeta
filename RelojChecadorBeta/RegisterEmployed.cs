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
        private RelojChecadorBetaEntities1 context;
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
                lblStatus.Text = String.Format("Hola Mundo {0}", Enrollment.FeaturesNeeded);
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

            if (codigoEmpleado.Trim() == "" || !codigoEmpleado.StartsWith("E"))
            {
                MessageBox.Show("Favor de ingresar el codigo del empleado valido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            guardarHuella(codigoEmpleado, streanFingerPrinters);
        }

        private void guardarHuella(string codigoEmpleado, byte[] streanFingerPrinters)
        {

            try
            {
                var isExistDeployed = context.Empleado.Where(e => e.Activo == 1).FirstOrDefault(e => e.CodigoEmpleado == codigoEmpleado);

                if (isExistDeployed is null)
                {
                    MessageBox.Show($"El Empleado con el numero de empleado {codigoEmpleado} no existe o esta dado de baja.");
                    return;
                }

                var isExistPrinterFingers = context.Huella.Any(e => e.Huella1 == streanFingerPrinters);


                if (isExistPrinterFingers)
                {
                    MessageBox.Show("El registro de la huella ya existe. Favor de intentar con otro dedo.","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                context.Huella.Add(new Huella
                {
                    Huella1 = streanFingerPrinters,
                    IdEmpleado = isExistDeployed.Id
                });

                context.SaveChanges();

                MessageBox.Show($"El registro del empleado con codigo {codigoEmpleado} se ha guardado con éxito.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RegisterEmployed_Load(object sender, EventArgs e)
        {

            context = new RelojChecadorBetaEntities1();
        }
    }
}
