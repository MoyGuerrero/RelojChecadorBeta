namespace RelojChecadorBeta
{
    partial class RegisterEmployed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigoEmpleado = new System.Windows.Forms.TextBox();
            this.btnCapturarHuella = new System.Windows.Forms.Button();
            this.picRegisterHuella = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtByte = new System.Windows.Forms.TextBox();
            this.lblMensajeError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picRegisterHuella)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo del empleado";
            // 
            // txtCodigoEmpleado
            // 
            this.txtCodigoEmpleado.Location = new System.Drawing.Point(224, 35);
            this.txtCodigoEmpleado.Name = "txtCodigoEmpleado";
            this.txtCodigoEmpleado.Size = new System.Drawing.Size(202, 20);
            this.txtCodigoEmpleado.TabIndex = 1;
            // 
            // btnCapturarHuella
            // 
            this.btnCapturarHuella.Location = new System.Drawing.Point(454, 35);
            this.btnCapturarHuella.Name = "btnCapturarHuella";
            this.btnCapturarHuella.Size = new System.Drawing.Size(110, 23);
            this.btnCapturarHuella.TabIndex = 2;
            this.btnCapturarHuella.Text = "Capturar Huella";
            this.btnCapturarHuella.UseVisualStyleBackColor = true;
            this.btnCapturarHuella.Click += new System.EventHandler(this.btnCapturarHuella_Click);
            // 
            // picRegisterHuella
            // 
            this.picRegisterHuella.Location = new System.Drawing.Point(224, 111);
            this.picRegisterHuella.Name = "picRegisterHuella";
            this.picRegisterHuella.Size = new System.Drawing.Size(110, 107);
            this.picRegisterHuella.TabIndex = 3;
            this.picRegisterHuella.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(187, 248);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(205, 23);
            this.lblStatus.TabIndex = 4;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(224, 388);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(107, 28);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtByte
            // 
            this.txtByte.Location = new System.Drawing.Point(221, 422);
            this.txtByte.Name = "txtByte";
            this.txtByte.Size = new System.Drawing.Size(205, 20);
            this.txtByte.TabIndex = 6;
            this.txtByte.Visible = false;
            // 
            // lblMensajeError
            // 
            this.lblMensajeError.AutoSize = true;
            this.lblMensajeError.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeError.ForeColor = System.Drawing.Color.White;
            this.lblMensajeError.Location = new System.Drawing.Point(12, 317);
            this.lblMensajeError.Name = "lblMensajeError";
            this.lblMensajeError.Size = new System.Drawing.Size(0, 24);
            this.lblMensajeError.TabIndex = 7;
            // 
            // RegisterEmployed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(181)))));
            this.ClientSize = new System.Drawing.Size(596, 450);
            this.Controls.Add(this.lblMensajeError);
            this.Controls.Add(this.txtByte);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picRegisterHuella);
            this.Controls.Add(this.btnCapturarHuella);
            this.Controls.Add(this.txtCodigoEmpleado);
            this.Controls.Add(this.label1);
            this.Name = "RegisterEmployed";
            this.Text = "RegisterEmployed";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegisterEmployed_FormClosed);
            this.Load += new System.EventHandler(this.RegisterEmployed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picRegisterHuella)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigoEmpleado;
        private System.Windows.Forms.Button btnCapturarHuella;
        private System.Windows.Forms.PictureBox picRegisterHuella;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtByte;
        private System.Windows.Forms.Label lblMensajeError;
    }
}