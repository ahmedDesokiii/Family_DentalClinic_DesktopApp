namespace StemV5
{
    partial class UserControlDay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ldlDays = new System.Windows.Forms.Label();
            this.bunifuElipse4 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.SuspendLayout();
            // 
            // ldlDays
            // 
            this.ldlDays.AutoSize = true;
            this.ldlDays.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold);
            this.ldlDays.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ldlDays.Location = new System.Drawing.Point(3, 2);
            this.ldlDays.Name = "ldlDays";
            this.ldlDays.Size = new System.Drawing.Size(27, 19);
            this.ldlDays.TabIndex = 2;
            this.ldlDays.Text = "00";
            this.ldlDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuElipse4
            // 
            this.bunifuElipse4.ElipseRadius = 10;
            this.bunifuElipse4.TargetControl = this;
            // 
            // UserControlDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.ldlDays);
            this.Name = "UserControlDay";
            this.Size = new System.Drawing.Size(48, 41);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ldlDays;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse4;
    }
}
