namespace ManuBot
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TestBtn = new System.Windows.Forms.Button();
            this.UpdaterTimer = new System.Windows.Forms.Timer(this.components);
            this.ClientNumberLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GamesDoneLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.shutdownCbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(97, 79);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(75, 23);
            this.TestBtn.TabIndex = 0;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // UpdaterTimer
            // 
            this.UpdaterTimer.Enabled = true;
            this.UpdaterTimer.Interval = 500;
            // 
            // ClientNumberLabel
            // 
            this.ClientNumberLabel.AutoSize = true;
            this.ClientNumberLabel.Location = new System.Drawing.Point(12, 9);
            this.ClientNumberLabel.Name = "ClientNumberLabel";
            this.ClientNumberLabel.Size = new System.Drawing.Size(76, 13);
            this.ClientNumberLabel.TabIndex = 1;
            this.ClientNumberLabel.Text = "Client Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Games done:";
            // 
            // GamesDoneLbl
            // 
            this.GamesDoneLbl.AutoSize = true;
            this.GamesDoneLbl.Location = new System.Drawing.Point(89, 31);
            this.GamesDoneLbl.Name = "GamesDoneLbl";
            this.GamesDoneLbl.Size = new System.Drawing.Size(29, 13);
            this.GamesDoneLbl.TabIndex = 3;
            this.GamesDoneLbl.Text = "NaN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Shutdown on finish:";
            // 
            // shutdownCbox
            // 
            this.shutdownCbox.AutoSize = true;
            this.shutdownCbox.Checked = true;
            this.shutdownCbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shutdownCbox.Location = new System.Drawing.Point(119, 48);
            this.shutdownCbox.Name = "shutdownCbox";
            this.shutdownCbox.Size = new System.Drawing.Size(15, 14);
            this.shutdownCbox.TabIndex = 5;
            this.shutdownCbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 114);
            this.Controls.Add(this.shutdownCbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GamesDoneLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClientNumberLabel);
            this.Controls.Add(this.TestBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.Timer UpdaterTimer;
        private System.Windows.Forms.Label ClientNumberLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label GamesDoneLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox shutdownCbox;
    }
}

