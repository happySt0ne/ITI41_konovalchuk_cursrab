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

namespace coursework
{
    public partial class Form1 : Form {
        Scene scene;
        string text;
        public Form1() {
            InitializeComponent();
            scene = new Scene();
            text = "";
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void glControl1_Load(object sender, EventArgs e) {
            // Цвет бэкграунда.
            GL.ClearColor(0.564f, 0.713f, 0.572f, 1);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e) {
            // Зарисовка бэкграунда.
            glControl1.SwapBuffers();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            label1.Text = text;
            scene.Update(ref text, out bool endGame);
            if (endGame) {
                timer1.Stop();
                MessageBox.Show("Игра окончена");
            }
            glControl1.Refresh();
        }
    }
}
