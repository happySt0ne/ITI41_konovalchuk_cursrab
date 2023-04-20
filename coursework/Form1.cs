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
        public Form1() {
            InitializeComponent();
            scene = new Scene();
            scene.AddObject(new Panzar(-0.5, 0, 0.1, 0.1, "left"));
            scene.AddObject(new Panzar(0.5, 0, 0.1, 0.1, "right"));
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
            scene.Draw();
            scene.GetPanzarBySide("left").Move();
            scene.GetPanzarBySide("right").Move();
            glControl1.Refresh();
        }

        //private void glControl1_KeyDown(object sender, KeyEventArgs e) {
        //    label1.Text = e.KeyCode.ToString();

        //    switch (e.KeyCode) {
        //        case Keys.A:
        //        case Keys.D:
        //            scene.GetPanzarBySide("left").Move(e.KeyCode);
        //            break;

        //        case Keys.J:
        //        case Keys.L:
        //            scene.GetPanzarBySide("right").Move(e.KeyCode);
        //            break;
        //    }
        //}
    }
}
