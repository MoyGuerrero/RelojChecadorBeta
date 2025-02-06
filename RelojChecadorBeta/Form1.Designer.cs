namespace RelojChecadorBeta
{
    partial class MyForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyForm));
            this.lblReloj = new System.Windows.Forms.Label();
            this.imageEmploye = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageEmploye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReloj
            // 
            this.lblReloj.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblReloj, "lblReloj");
            this.lblReloj.Name = "lblReloj";
            // 
            // imageEmploye
            // 
            this.imageEmploye.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageEmploye, "imageEmploye");
            this.imageEmploye.Name = "imageEmploye";
            this.imageEmploye.TabStop = false;
            this.imageEmploye.DoubleClick += new System.EventHandler(this.imageEmploye_DoubleClick);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // MyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageEmploye);
            this.Controls.Add(this.lblReloj);
            this.Name = "MyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageEmploye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblReloj;
        private System.Windows.Forms.PictureBox imageEmploye;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

