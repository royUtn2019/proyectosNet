namespace whatSappApi
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMsn = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lbCode = new System.Windows.Forms.Label();
            this.txtCelu = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMsn
            // 
            this.txtMsn.Location = new System.Drawing.Point(59, 48);
            this.txtMsn.Multiline = true;
            this.txtMsn.Name = "txtMsn";
            this.txtMsn.Size = new System.Drawing.Size(166, 70);
            this.txtMsn.TabIndex = 0;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(90, 184);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 1;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Location = new System.Drawing.Point(59, 135);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(43, 13);
            this.lbCode.TabIndex = 2;
            this.lbCode.Text = "+54911";
            // 
            // txtCelu
            // 
            this.txtCelu.Location = new System.Drawing.Point(104, 131);
            this.txtCelu.MaxLength = 8;
            this.txtCelu.Name = "txtCelu";
            this.txtCelu.Size = new System.Drawing.Size(100, 20);
            this.txtCelu.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtCelu);
            this.Controls.Add(this.lbCode);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtMsn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMsn;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.TextBox txtCelu;
    }
}

