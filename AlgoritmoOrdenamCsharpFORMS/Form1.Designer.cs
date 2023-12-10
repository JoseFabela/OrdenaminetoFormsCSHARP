namespace AlgoritmoOrdenamCsharpFORMS
{
    partial class Form1
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
            this.listBoxUnits = new System.Windows.Forms.ListBox();
            this.comboBoxAlgorithms = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // listBoxUnits
            // 
            this.listBoxUnits.FormattingEnabled = true;
            this.listBoxUnits.ItemHeight = 16;
            this.listBoxUnits.Location = new System.Drawing.Point(26, 12);
            this.listBoxUnits.Name = "listBoxUnits";
            this.listBoxUnits.Size = new System.Drawing.Size(505, 388);
            this.listBoxUnits.TabIndex = 0;
            // 
            // comboBoxAlgorithms
            // 
            this.comboBoxAlgorithms.FormattingEnabled = true;
            this.comboBoxAlgorithms.Location = new System.Drawing.Point(561, 80);
            this.comboBoxAlgorithms.Name = "comboBoxAlgorithms";
            this.comboBoxAlgorithms.Size = new System.Drawing.Size(227, 24);
            this.comboBoxAlgorithms.TabIndex = 1;
           // this.comboBoxAlgorithms.SelectedIndexChanged += new System.EventHandler(this.SortingAlgorithmComboBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxAlgorithms);
            this.Controls.Add(this.listBoxUnits);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxUnits;
        private System.Windows.Forms.ComboBox comboBoxAlgorithms;
    }
}

