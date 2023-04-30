using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Game_Engine_Library {
    public class Scene {
        private List<GameObject> _objects = new List<GameObject>();
        private List<Panzar> panzars = new List<Panzar>();

        public Scene() {
            AddObject(new Panzar(-0.5, 0, "left"));
            AddObject(new Panzar(0.5, 0, "right"));
            AddObject(new Wall(-0.1, 0.2, 0.2, 0.7));
            AddObject(new Wall(-0.999, 1, 0.0001, 2));
            AddObject(new Wall(0.999, 1, 0.0001, 2));
            GetPanzarsList();
        }

        public void AddObject(GameObject gameObject) =>_objects.Add(gameObject);

        /// <summary>
        /// Добавляет все танки в список танков.
        /// </summary>
        private void GetPanzarsList() {
            foreach (GameObject obj in _objects) { 
                if (obj is Panzar panzar) {
                    panzars.Add(panzar);
                }
            }
        }

        /// <summary>
        /// Обновление логики и перересовка всех объектов сцены.
        /// </summary>
        public void Update(ref string text) {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (GameObject obj in _objects) {
                obj.Update();
                
                foreach (GameObject obj2 in _objects.Where(x => x != obj)) {
                    if (obj.Collision.IntersectsWith(obj2.Collision) && obj is Panzar panzar) {
                        panzar.touched = true;
                    } 
                }
            }

            AddBullet(ref text);
        }

        /// <summary>
        /// Добавляет пули на сцену при выстреле.
        /// </summary>
        /// <param name="text"></param>
        private void AddBullet(ref string text) {
            foreach (Panzar panzar in panzars) {
                if (panzar.Shooted) {
                    AddObject(new Bullet(panzar.bulletPosition.Item1, panzar.bulletPosition.Item2, panzar._muzzleDirection, panzar.Side));
                    text = panzar.bulletPosition.Item1.ToString() + " " + panzar.bulletPosition.Item2.ToString();
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
