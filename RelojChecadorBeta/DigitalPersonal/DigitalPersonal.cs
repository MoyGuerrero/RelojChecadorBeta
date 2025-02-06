using DPFP;
using DPFP.Capture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelojChecadorBeta.DigitalPersonal
{

    public class DigitalPersonal : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture capture;
        private Bitmap bitMap;
        public event Action<Bitmap> OnFingerprintCaptured;
        public event Action<DPFP.Sample> setSample;


        public DigitalPersonal()
        {
            Init();
        }

        protected virtual void Init()
        {
            try
            {
                capture = new DPFP.Capture.Capture();
                if (capture == null)
                {
                    MessageBox.Show("No se pudo iniciar la operación de captura");
                    return;
                }

                capture.EventHandler = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar la operación de captura\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        protected void Start()
        {
            try
            {
                if (capture == null)
                {
                    MessageBox.Show("No se pudo iniciar");
                    return;
                }

                capture.StartCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void Stop()
        {
            try
            {
                if (capture == null)
                {
                    MessageBox.Show("No se pudo Terminar");
                    return;
                }

                capture.StopCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public Bitmap GetBitMap()
        {
            return bitMap;
        }


        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            DPFP.Capture.SampleConversion convertion = new DPFP.Capture.SampleConversion();
            bitMap = null;
            convertion.ConvertToPicture(Sample, ref bitMap);
            OnFingerprintCaptured.Invoke(bitMap);
            setSample.Invoke(Sample);
            //MessageBox.Show("OnComplete");
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            //MessageBox.Show("OnFingerGone");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            //MessageBox.Show("OnFingerTouch");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            //MessageBox.Show("OnReaderConnect");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            //MessageBox.Show("OnReaderDisconnect");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
            //MessageBox.Show("OnSampleQuality");
        }
    }
}
