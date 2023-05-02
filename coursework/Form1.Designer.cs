namespace coursework
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.glControl1 = new OpenTK.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.HealthBar1 = new System.Windows.Forms.ProgressBar();
            this.HealthBar2 = new System.Windows.Forms.ProgressBar();
            this.Ammo1 = new System.Windows.Forms.Label();
            this.Ammo2 = new System.Windows.Forms.Label();
            this.Cooldown1 = new System.Windows.Forms.Label();
            this.Cooldown2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.glControl1.ForeColor = System.Drawing.Color.Transparent;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(813, 474);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // HealthBar1
            // 
            this.HealthBar1.ForeColor = System.Drawing.Color.Red;
            this.HealthBar1.Location = new System.Drawing.Point(12, 12);
            this.HealthBar1.Name = "HealthBar1";
            this.HealthBar1.Size = new System.Drawing.Size(120, 23);
            this.HealthBar1.TabIndex = 2;
            this.HealthBar1.Tag = "";
            this.HealthBar1.Value = 100;
            // 
            // HealthBar2
            // 
            this.HealthBar2.BackColor = System.Drawing.SystemColors.Control;
            this.HealthBar2.Location = new System.Drawing.Point(682, 12);
            this.HealthBar2.Name = "HealthBar2";
            this.HealthBar2.Size = new System.Drawing.Size(120, 23);
            this.HealthBar2.TabIndex = 3;
            this.HealthBar2.Value = 100;
            // 
            // Ammo1
            // 
            this.Ammo1.AutoSize = true;
            this.Ammo1.BackColor = System.Drawing.Color.Transparent;
            this.Ammo1.ForeColor = System.Drawing.Color.Black;
            this.Ammo1.Location = new System.Drawing.Point(12, 54);
            this.Ammo1.Name = "Ammo1";
            this.Ammo1.Size = new System.Drawing.Size(44, 16);
            this.Ammo1.TabIndex = 4;
            this.Ammo1.Text = "label1";
            // 
            // Ammo2
            // 
            this.Ammo2.AutoSize = true;
            this.Ammo2.BackColor = System.Drawing.Color.Transparent;
            this.Ammo2.ForeColor = System.Drawing.Color.Black;
            this.Ammo2.Location = new System.Drawing.Point(679, 54);
            this.Ammo2.Name = "Ammo2";
            this.Ammo2.Size = new System.Drawing.Size(44, 16);
            this.Ammo2.TabIndex = 5;
            this.Ammo2.Text = "label2";
            // 
            // Cooldown1
            // 
            this.Cooldown1.AutoSize = true;
            this.Cooldown1.BackColor = System.Drawing.Color.Transparent;
            this.Cooldown1.ForeColor = System.Drawing.Color.Black;
            this.Cooldown1.Location = new System.Drawing.Point(12, 38);
            this.Cooldown1.Name = "Cooldown1";
            this.Cooldown1.Size = new System.Drawing.Size(44, 16);
            this.Cooldown1.TabIndex = 6;
            this.Cooldown1.Text = "label3";
            // 
            // Cooldown2
            // 
            this.Cooldown2.AutoSize = true;
            this.Cooldown2.BackColor = System.Drawing.Color.Transparent;
            this.Cooldown2.ForeColor = System.Drawing.Color.Black;
            this.Cooldown2.Location = new System.Drawing.Point(679, 38);
            this.Cooldown2.Name = "Cooldown2";
            this.Cooldown2.Size = new System.Drawing.Size(44, 16);
            this.Cooldown2.TabIndex = 7;
            this.Cooldown2.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(815, 475);
            this.Controls.Add(this.Cooldown2);
            this.Controls.Add(this.Cooldown1);
            this.Controls.Add(this.Ammo2);
            this.Controls.Add(this.Ammo1);
            this.Controls.Add(this.HealthBar2);
            this.Controls.Add(this.HealthBar1);
            this.Controls.Add(this.glControl1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar HealthBar1;
        private System.Windows.Forms.ProgressBar HealthBar2;
        private System.Windows.Forms.Label Ammo1;
        private System.Windows.Forms.Label Ammo2;
        private System.Windows.Forms.Label Cooldown1;
        private System.Windows.Forms.Label Cooldown2;
    }
}

