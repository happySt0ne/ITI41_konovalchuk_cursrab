using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace Game_Engine_Library {
    public class Scene {
        const int BULLET_DAMAGE = 100;
        private List<GameObject> _listToRemove = new List<GameObject>();
        private List<GameObject> _objects = new List<GameObject>();
        private List<Panzar> _panzars = new List<Panzar>();

        public Scene() {
            _objects.Add(new Panzar(-0.5, 0, "left"));
            _objects.Add(new Panzar(0.5, 0, "right"));
            _objects.Add(new Wall(-0.1, 0.2, 0.2, 0.7));
            _objects.Add(new Wall(-0.999, 1, 0.0001, 2));
            _objects.Add(new Wall(0.999, 1, 0.0001, 2));
            GetPanzarsList();
        }

        /// <summary>
        /// Добавляет все танки в список танков.
        /// </summary>
        private void GetPanzarsList() {
            foreach (GameObject obj in _objects) { 
                if (obj is Panzar panzar) {
                    _panzars.Add(panzar);
                }
            }
        }

        /// <summary>
        /// Обновление логики и перересовка всех объектов сцены.
        /// </summary>
        public void Update(ref string text, out bool endGame) {
            if (_panzars.Any(x => x.Health <= 0)) {
                endGame = true;
                return;
            } else endGame = false;

            GL.Clear(ClearBufferMask.ColorBufferBit);
            _listToRemove.ForEach(x => _objects.Remove(x));
            _listToRemove.Clear();
            

            foreach (GameObject obj in _objects) {
                obj.Update();
                
                foreach (GameObject obj2 in _objects.Where(x => x != obj)) {
                    if (obj.Collision.IntersectsWith(obj2.Collision)) {
                        switch (obj.GetType().ToString()) {
                            case "Game_Engine_Library.Panzar":
                                PanzarCollisionActions(obj as Panzar, obj2);
                                break;
                            case "Game_Engine_Library.Bullet":
                                BulletCollisionActions(obj as Bullet, obj2);
                                break;
                            default:
                                break;
                        }
                    } 
                }
            }

            text = _panzars[0].Health.ToString();
            AddBullet();
        }

        private void PanzarCollisionActions(Panzar panzar, GameObject collisionedObject) {
            switch (collisionedObject.GetType().ToString()) {
                case "Game_Engine_Library.Bullet":
                    panzar.Health -= ((Bullet)collisionedObject).Damage;

                    ((Bullet)collisionedObject).Explode();
                    _listToRemove.Add(collisionedObject);
                    break;

                case "Game_Engine_Library.Wall":
                    panzar.touched = true;
                    break;

                default:
                    break;
            }
        }
        
        private void BulletCollisionActions(Bullet bullet, GameObject collisionedObject) {
            switch (collisionedObject.GetType().ToString()) {
                case "Game_Engine_Library.Bullet":
                    bullet.Explode();
                    ((Bullet)collisionedObject).Explode();
                    
                    _listToRemove.Add(bullet);
                    _listToRemove.Add(collisionedObject);
                    break;

                case "Game_Engine_Library.Wall":
                    _listToRemove.Add(bullet);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Добавляет пули на сцену при выстреле.
        /// </summary>
        /// <param name="text"></param>
        private void AddBullet() {
            foreach (Panzar panzar in _panzars) {
                if (panzar.Shooted) {
                    _objects.Add(new Bullet(panzar.bulletPosition.Item1, 
                                            panzar.bulletPosition.Item2, 
                                            panzar._muzzleDirection, 
                                            panzar.Side,
                                            BULLET_DAMAGE));
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
