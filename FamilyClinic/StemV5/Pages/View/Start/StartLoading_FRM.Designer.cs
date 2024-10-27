namespace StemV5
{
    partial class StartLoading_FRM
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartLoading_FRM));
            this.progressBar_StartLoading_FRM = new System.Windows.Forms.ProgressBar();
            this.BE_StartLoading_FRM = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.Timer_StartLoading_FRM = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar_StartLoading_FRM
            // 
            this.progressBar_StartLoading_FRM.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressBar_StartLoading_FRM.Location = new System.Drawing.Point(0, 301);
            this.progressBar_StartLoading_FRM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar_StartLoading_FRM.Name = "progressBar_StartLoading_FRM";
            this.progressBar_StartLoading_FRM.Size = new System.Drawing.Size(467, 27);
            this.progressBar_StartLoading_FRM.TabIndex = 3;
            this.progressBar_StartLoading_FRM.UseWaitCursor = true;
            // 
            // BE_StartLoading_FRM
            // 
            this.BE_StartLoading_FRM.ElipseRadius = 25;
            this.BE_StartLoading_FRM.TargetControl = this;
            // 
            // Timer_StartLoading_FRM
            // 
            this.Timer_StartLoading_FRM.Tick += new System.EventHandler(this.Timer_StartLoading_FRM_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::StemV5.Properties.Resources.startlogo;
            this.pictureBox1.Location = new System.Drawing.Point(68, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(326, 234);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(186, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 28);
            this.label10.TabIndex = 22;
            this.label10.Text = "Since 2010";
            this.label10.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(355, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 24);
            this.label1.TabIndex = 23;
            this.label1.Text = "Version 1.5";
            this.label1.UseWaitCursor = true;
            // 
            // panelContainer
            // 
            this.panelContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelContainer.BackColor = System.Drawing.Color.LightBlue;
            this.panelContainer.Controls.Add(this.pictureBox1);
            this.panelContainer.Controls.Add(this.label1);
            this.panelContainer.Controls.Add(this.label10);
            this.panelContainer.Controls.Add(this.progressBar_StartLoading_FRM);
            this.panelContainer.Location = new System.Drawing.Point(49, 68);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(467, 318);
            this.panelContainer.TabIndex = 24;
            this.panelContainer.UseWaitCursor = true;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 25;
            this.bunifuElipse1.TargetControl = this.panelContainer;
            // 
            // StartLoading_FRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(571, 458);
            this.Controls.Add(this.panelContainer);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "StartLoading_FRM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartLoading";
            this.UseWaitCursor = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StartLoading_FRM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar_StartLoading_FRM;
        private Bunifu.Framework.UI.BunifuElipse BE_StartLoading_FRM;
        private System.Windows.Forms.Timer Timer_StartLoading_FRM;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelContainer;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
    }
}

