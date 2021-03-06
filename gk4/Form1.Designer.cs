
namespace gk4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.whitheboardBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.StartStop_Button = new System.Windows.Forms.Button();
            this.CameraButtonStationary = new System.Windows.Forms.Button();
            this.CameraStaringButton = new System.Windows.Forms.Button();
            this.CameraFolowingButton = new System.Windows.Forms.Button();
            this.Cameras = new System.Windows.Forms.GroupBox();
            this.ShadingOptionBox = new System.Windows.Forms.GroupBox();
            this.PhongShadingButton = new System.Windows.Forms.Button();
            this.NoneShadingButton = new System.Windows.Forms.Button();
            this.ConstantShadingButton = new System.Windows.Forms.Button();
            this.GouraudShadingButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.whitheboardBox)).BeginInit();
            this.Cameras.SuspendLayout();
            this.ShadingOptionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // whitheboardBox
            // 
            this.whitheboardBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.whitheboardBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.whitheboardBox.Location = new System.Drawing.Point(166, -1);
            this.whitheboardBox.Name = "whitheboardBox";
            this.whitheboardBox.Size = new System.Drawing.Size(785, 630);
            this.whitheboardBox.TabIndex = 0;
            this.whitheboardBox.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StartStop_Button
            // 
            this.StartStop_Button.Location = new System.Drawing.Point(12, 12);
            this.StartStop_Button.Name = "StartStop_Button";
            this.StartStop_Button.Size = new System.Drawing.Size(148, 36);
            this.StartStop_Button.TabIndex = 1;
            this.StartStop_Button.Text = "Start";
            this.StartStop_Button.UseVisualStyleBackColor = true;
            this.StartStop_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // CameraButtonStationary
            // 
            this.CameraButtonStationary.Location = new System.Drawing.Point(6, 37);
            this.CameraButtonStationary.Name = "CameraButtonStationary";
            this.CameraButtonStationary.Size = new System.Drawing.Size(136, 29);
            this.CameraButtonStationary.TabIndex = 2;
            this.CameraButtonStationary.Text = "Nieruchoma";
            this.CameraButtonStationary.UseVisualStyleBackColor = true;
            this.CameraButtonStationary.Click += new System.EventHandler(this.CameraButtonStationary_Click);
            // 
            // CameraStaringButton
            // 
            this.CameraStaringButton.Location = new System.Drawing.Point(6, 72);
            this.CameraStaringButton.Name = "CameraStaringButton";
            this.CameraStaringButton.Size = new System.Drawing.Size(136, 29);
            this.CameraStaringButton.TabIndex = 3;
            this.CameraStaringButton.Text = "Wpatrująca się";
            this.CameraStaringButton.UseVisualStyleBackColor = true;
            this.CameraStaringButton.Click += new System.EventHandler(this.CameraStaringButton_Click);
            // 
            // CameraFolowingButton
            // 
            this.CameraFolowingButton.Location = new System.Drawing.Point(6, 107);
            this.CameraFolowingButton.Name = "CameraFolowingButton";
            this.CameraFolowingButton.Size = new System.Drawing.Size(136, 29);
            this.CameraFolowingButton.TabIndex = 4;
            this.CameraFolowingButton.Text = "Podążająca";
            this.CameraFolowingButton.UseVisualStyleBackColor = true;
            this.CameraFolowingButton.Click += new System.EventHandler(this.CameraFolowingButton_Click);
            // 
            // Cameras
            // 
            this.Cameras.Controls.Add(this.CameraButtonStationary);
            this.Cameras.Controls.Add(this.CameraFolowingButton);
            this.Cameras.Controls.Add(this.CameraStaringButton);
            this.Cameras.Location = new System.Drawing.Point(12, 96);
            this.Cameras.Name = "Cameras";
            this.Cameras.Size = new System.Drawing.Size(148, 154);
            this.Cameras.TabIndex = 5;
            this.Cameras.TabStop = false;
            this.Cameras.Text = "Kamery";
            // 
            // ShadingOptionBox
            // 
            this.ShadingOptionBox.Controls.Add(this.PhongShadingButton);
            this.ShadingOptionBox.Controls.Add(this.NoneShadingButton);
            this.ShadingOptionBox.Controls.Add(this.ConstantShadingButton);
            this.ShadingOptionBox.Controls.Add(this.GouraudShadingButton);
            this.ShadingOptionBox.Location = new System.Drawing.Point(12, 256);
            this.ShadingOptionBox.Name = "ShadingOptionBox";
            this.ShadingOptionBox.Size = new System.Drawing.Size(148, 186);
            this.ShadingOptionBox.TabIndex = 6;
            this.ShadingOptionBox.TabStop = false;
            this.ShadingOptionBox.Text = "Rodzaj oświetlania";
            // 
            // PhongShadingButton
            // 
            this.PhongShadingButton.Location = new System.Drawing.Point(6, 141);
            this.PhongShadingButton.Name = "PhongShadingButton";
            this.PhongShadingButton.Size = new System.Drawing.Size(136, 29);
            this.PhongShadingButton.TabIndex = 10;
            this.PhongShadingButton.Text = "Phonga";
            this.PhongShadingButton.UseVisualStyleBackColor = true;
            this.PhongShadingButton.Click += new System.EventHandler(this.PhongShadingButton_Click);
            // 
            // NoneShadingButton
            // 
            this.NoneShadingButton.Location = new System.Drawing.Point(6, 36);
            this.NoneShadingButton.Name = "NoneShadingButton";
            this.NoneShadingButton.Size = new System.Drawing.Size(136, 29);
            this.NoneShadingButton.TabIndex = 7;
            this.NoneShadingButton.Text = "Brak";
            this.NoneShadingButton.UseVisualStyleBackColor = true;
            this.NoneShadingButton.Click += new System.EventHandler(this.NoneShadingButton_Click);
            // 
            // ConstantShadingButton
            // 
            this.ConstantShadingButton.Location = new System.Drawing.Point(6, 71);
            this.ConstantShadingButton.Name = "ConstantShadingButton";
            this.ConstantShadingButton.Size = new System.Drawing.Size(136, 29);
            this.ConstantShadingButton.TabIndex = 8;
            this.ConstantShadingButton.Text = "Stałe";
            this.ConstantShadingButton.UseVisualStyleBackColor = true;
            this.ConstantShadingButton.Click += new System.EventHandler(this.ConstantShadingButton_Click);
            // 
            // GouraudShadingButton
            // 
            this.GouraudShadingButton.Location = new System.Drawing.Point(6, 106);
            this.GouraudShadingButton.Name = "GouraudShadingButton";
            this.GouraudShadingButton.Size = new System.Drawing.Size(136, 29);
            this.GouraudShadingButton.TabIndex = 9;
            this.GouraudShadingButton.Text = "Gourauda";
            this.GouraudShadingButton.UseVisualStyleBackColor = true;
            this.GouraudShadingButton.Click += new System.EventHandler(this.GouraudShadingButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(951, 627);
            this.Controls.Add(this.ShadingOptionBox);
            this.Controls.Add(this.Cameras);
            this.Controls.Add(this.StartStop_Button);
            this.Controls.Add(this.whitheboardBox);
            this.Name = "Form1";
            this.Text = "3D";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.whitheboardBox)).EndInit();
            this.Cameras.ResumeLayout(false);
            this.ShadingOptionBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox whitheboardBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button StartStop_Button;
        private System.Windows.Forms.Button CameraButtonStationary;
        private System.Windows.Forms.Button CameraStaringButton;
        private System.Windows.Forms.Button CameraFolowingButton;
        private System.Windows.Forms.GroupBox Cameras;
        private System.Windows.Forms.GroupBox ShadingOptionBox;
        private System.Windows.Forms.Button PhongShadingButton;
        private System.Windows.Forms.Button NoneShadingButton;
        private System.Windows.Forms.Button ConstantShadingButton;
        private System.Windows.Forms.Button GouraudShadingButton;
    }
}

