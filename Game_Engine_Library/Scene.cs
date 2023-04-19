using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public class Scene {
        /// <summary>
        /// Список объектов сцены.
        /// </summary>
        private List<GameObject> _objects = new List<GameObject>();

        public void AddObject(GameObject gameObject) =>_objects.Add(gameObject);
        
        public void Draw () {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (GameObject obj in _objects) {
                obj.Draw();
            }
        }
    }
}
