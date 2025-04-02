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
            this.lblEstatus = new System.Windows.Forms.Label();
            this.txtEmployeCode = new System.Windows.Forms.TextBox();
            this.lblMessageError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageEmploye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReloj
            // 
            this.lblReloj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(181)))));
            resources.ApplyResources(this.lblReloj, "lblReloj");
            this.lblReloj.ForeColor = System.Drawing.Color.White;
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
            // lblEstatus
            // 
            resources.ApplyResources(this.lblEstatus, "lblEstatus");
            this.lblEstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(229)))), ((int)(((byte)(252)))));
            this.lblEstatus.Name = "lblEstatus";
            // 
            // txtEmployeCode
            // 
            this.txtEmployeCode.AcceptsTab = true;
            this.txtEmployeCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtEmployeCode, "txtEmployeCode");
            this.txtEmployeCode.Name = "txtEmployeCode";
            this.txtEmployeCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegistroCodigo);
            // 
            // lblMessageError
            // 
            resources.ApplyResources(this.lblMessageError, "lblMessageError");
            this.lblMessageError.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageError.Name = "lblMessageError";
            // 
            // MyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(181)))));
            this.Controls.Add(this.lblMessageError);
            this.Controls.Add(this.txtEmployeCode);
            this.Controls.Add(this.lblEstatus);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageEmploye);
            this.Controls.Add(this.lblReloj);
            this.Name = "MyForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageEmploye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReloj;
        private System.Windows.Forms.PictureBox imageEmploye;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblEstatus;
        private System.Windows.Forms.TextBox txtEmployeCode;
        private System.Windows.Forms.Label lblMessageError;
    }
}

