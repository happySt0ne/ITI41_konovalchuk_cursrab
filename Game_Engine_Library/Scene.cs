using Game_Engine_Library.Bonuses;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace Game_Engine_Library {
    public class Scene {
        private List<GameObject> _listToRemove = new List<GameObject>();
        private List<GameObject> _objects = new List<GameObject>();
        private List<Panzar> _panzars = new List<Panzar>();
        private List<Panzar> _addEffects = new List<Panzar>();
        private double _planeSpawnCooldown = Constants.PLANE_SPAWN_MAX_COOLDOWN;
        private Bonus _bonusTriedToCreate;
        private Plane _plane;

        public Scene() {
            _objects.Add(new Background(-1, 1, 2, 2));
            _objects.Add(new Panzar("left"));
            _objects.Add(new Panzar("right"));
            _objects.Add(new Wall(-0.1, -0.5, 0.2, 0.5));
            _objects.Add(new Wall(-0.999, 1, 0.0001, 2));
            _objects.Add(new Wall(0.999, 1, 0.0001, 2));

            GetPanzarsList();
        }

        public void GetPanzarsInfo(out double health1, out double health2, out int ammo1, out int ammo2, out double cooldown1, out double cooldown2) {
            health1 = _panzars[0].Health;
            health2 = _panzars[1].Health;
            ammo1 = _panzars[0].Ammo;
            ammo2 = _panzars[1].Ammo;
            cooldown1 = Math.Round(_panzars[0].Cooldown, 1);
            cooldown2 = Math.Round(_panzars[1].Cooldown, 1);
        }

        /// <summary>
        /// Добавляет все танки в список танков.
        /// </summary>
        private void GetPanzarsList() {
            _panzars.Clear();

            foreach (GameObject obj in _objects) { 
                if (obj is Panzar panzar) {
                    _panzars.Add(panzar);
                }
            }
        }

        /// <summary>
        /// Проверяет, не следует ли закончить игру.
        /// </summary>
        /// <returns>-1 если победил правый, 1 если победил левый, 0 если иргра продолжается.</returns>
        private int CheckEndGame() {
            if (_panzars[0].Health <= 0) return -1;
            if (_panzars[1].Health <= 0) return 1;
            return 0;
        }

        /// <summary>
        /// Обновление логики и перересовка всех объектов сцены.
        /// </summary>
        public void Update(out int endGame) {
            TryCreateBonus();
            TryCreatePlane();
            TryDeletePlane();
            endGame = CheckEndGame();
            _listToRemove.ForEach(x => _objects.Remove(x));
            _listToRemove.Clear();
            AddBullet();

            foreach (GameObject obj in _objects) {
                obj.Update();
                CheckSceneCollision(obj);
            }
        }

        private void TryCreateBonus() {
            if (Plane.IsAlive && !Plane.Dropped) {
                _plane = _objects.Single(x => x is Plane) as Plane;
                _bonusTriedToCreate = Plane.DropBonus((_plane.Points[0].Item1 + _plane.Points[1].Item1) / 2, _plane.Points[3].Item2);

                if (_bonusTriedToCreate != null) _objects.Add(_bonusTriedToCreate);
            }
        }

        private void TryCreatePlane() {
            if (_planeSpawnCooldown <= 0) { 
                _planeSpawnCooldown = Constants.PLANE_SPAWN_MAX_COOLDOWN;
                _objects.Add(new Plane());
                return;
            }

            _planeSpawnCooldown -= Constants.TIMER_INTERVAL_SECONDS;
        }

        private void TryDeletePlane() {
            if (!Plane.IsAlive && _objects.Any(x => x is Plane)) _listToRemove.Add(_objects.Single(x => x is Plane));
        }

        /// <summary>
        /// Проверяет коллизию всех объектов сцены с объектом obj.
        /// </summary>
        /// <param name="obj">Объект, с которым проверяется столкновение остальных объектов сцены.</param>
        private void CheckSceneCollision(GameObject obj) {
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

        /// <summary>
        /// Обработка столкновения танка и какого либо объекта в зависимости от типа этого объекта.
        /// </summary>
        /// <param name="panzar">Танк, который столкнулся с объектом.</param>
        /// <param name="collisionedObject">Объект, который столкнулся с танком.</param>
        private void PanzarCollisionActions(Panzar panzar, GameObject collisionedObject) {
            switch (collisionedObject.GetType().ToString()) { 
                case "Game_Engine_Library.Bullet":
                    panzar.Health -= ((Bullet)collisionedObject).Damage;

                    ((Bullet)collisionedObject).Explode();
                    _listToRemove.Add(collisionedObject);
                    break;

                case "Game_Engine_Library.Wall":
                    panzar.Touched = true;
                    break;

                case "Game_Engine_Library.Bonuses.HealBonus":
                    panzar = new HealEffect(panzar);
                    _listToRemove.Add(collisionedObject);
                    break;

                case "Game_Engine_Library.Bonuses.AmmoBonus":
                    panzar = new AmmoEffect(panzar);
                    _listToRemove.Add(collisionedObject);
                    break;

                case "Game_Engine_Library.Bonuses.ReduceCooldownBonus":
                    //_objects[_objects.IndexOf(panzar)] = new ReduceCooldownEffect(panzar);
                    //panzar = new ReduceCooldownEffect(panzar);
                    _listToRemove.Add(collisionedObject);
                    break;

                default:
                    break;
            }
        }
        
        /// <summary>
        /// Обработка столкновения ракеты и какого либо объекта в зависимости от типа этого объекта.
        /// </summary>
        /// <param name="bullet">Ракета, которая столкнулась с объектом.</param>
        /// <param name="collisionedObject">Объект, который столкеулся с ракетой.</param>
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
                    _objects.Add(new Bullet(panzar.BulletPosition.Item1, 
                                            panzar.BulletPosition.Item2, 
                                            panzar.MuzzleDirection, 
                                            panzar.Side));
                }
            }
        }
    }
}
