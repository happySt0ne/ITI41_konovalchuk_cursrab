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
        public Scene() {
            AddObject(new Panzar(-0.5, 0, "left"));
            AddObject(new Panzar(0.5, 0, "right"));
            AddObject(new Wall(-0.1, 0.2, 0.2, 0.7));
            AddObject(new Wall(-0.999, 1, 0.0001, 2));
            AddObject(new Wall(0.999, 1, 0.0001, 2));
        }

        public void AddObject(GameObject gameObject) =>_objects.Add(gameObject);

        /// <summary>
        /// Обновление логики и перересовка всех объектов сцены.
        /// </summary>
        public void Update(ref string text) {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (GameObject obj in _objects) {
                obj.Update();
                if (obj is Panzar panzar1 && panzar1.Shooted) text = "shooted";

                foreach (GameObject obj2 in _objects.Where(x => x != obj)) {
                    if (obj.Collision.IntersectsWith(obj2.Collision) && obj is Panzar panzar) {
                        panzar.touched = true;
                    } 
                }
            }
        }

        /// <summary>
        /// Для взаимодействия с определённым танком исходя из его положения на экране (справа или слева)
        /// </summary>
        /// <param name="side">Сторона сил искомого танка. ("right" и "left")</param>
        /// <returns>Ссылку на танк заданной стороны.</returns>
        public Panzar GetPanzarBySide(string side) {
            foreach (GameObject obj in _objects) {
                if (obj is Panzar panzar && panzar.Side == side) {
                    return panzar;
                }
            }

            return null;
        }
    }
}
