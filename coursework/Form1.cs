using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Game_Engine_Library;
using System.Runtime.Remoting.Messaging;

namespace coursework
{
    public partial class Form1 : Form {
        Scene scene;

        public Form1() {
            InitializeComponent();
            timer1.Start();
            timer1.Interval = (int)(Constants.TIMER_INTERVAL_SECONDS * 1000);
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void glControl1_Load(object sender, EventArgs e) {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            scene = new Scene();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e) {
            glControl1.SwapBuffers();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            scene.Update(out int endGame);
            ShowPanzarsInfo();
            glControl1.Refresh();
            ParseEndGame(endGame);
        }

        private void ParseEndGame(int endGame) {
            if (endGame == 0) return;

            timer1.Stop();
            MessageBox.Show(endGame == -1 ? "Победил правый игрок" : "Победил левый игрок");
            Application.Exit();
        }

        private void ShowPanzarsInfo() {
            scene.GetPanzarsInfo(out double health1, out double health2, out int ammo1, out int ammo2, out double cooldown1, out double cooldown2);
            Cooldown1.Text = String.Format("Перезарядка: {0:0.0}", cooldown1);
            Cooldown2.Text = String.Format("Перезарядка: {0:0.0}", cooldown2);

            Ammo1.Text = $"Боезапас: {ammo1}";
            Ammo2.Text = $"Боезапас: {ammo2}";
            
            HealthBar1.Value = (int)health1 * 100 / Constants.PANZAR_MAX_HP;
            HealthBar2.Value = (int)health2 * 100 / Constants.PANZAR_MAX_HP;
        }
    }
}
