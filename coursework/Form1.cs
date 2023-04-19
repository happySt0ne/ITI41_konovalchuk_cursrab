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
        public Form1() {
            InitializeComponent();  
        }

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        private void glControl1_Paint(object sender, PaintEventArgs e) {
            GL.PointSize(30);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(0, 0);

            GL.Vertex2(0.1, 0);
            GL.Vertex2(0.1, 0.1);
            GL.End();
        }

        private void glControl1_Load(object sender, EventArgs e) {
            
        }
    }
}
