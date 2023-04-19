using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameEngine_Library {
    public class GameObject : IDisposable {
        protected int x, y, width, height;

        public GameObject(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void Dispose() {

        }
    }
}
