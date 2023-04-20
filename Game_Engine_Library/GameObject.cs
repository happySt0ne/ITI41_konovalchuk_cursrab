using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public abstract class GameObject : IDisposable, IDrawable {
        protected double x, y, width, height;

        public GameObject(double x, double y, double width, double height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public abstract void Draw();

        public abstract void Dispose();
    }
}
