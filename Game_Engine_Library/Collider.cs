using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Game_Engine_Library {
    /// <summary>
    /// Класс для проверки столкновения объектов.
    /// </summary>
    public class Collision {
        private RectangleF _collisionBox;

        /// <summary>
        /// Инициализация прямоугольного collision box.
        /// </summary>
        /// <param name="x">Координата X верхнего левого угла прямоугольника.</param>
        /// <param name="y">Координата Y верхнего левого угла прямоугольника.</param>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника.</param>
        public Collision(double x, double y, double width, double height) { 
            _collisionBox = new RectangleF(Convert.ToSingle(x),
                                           Convert.ToSingle(y),
                                           Convert.ToSingle(width),
                                           Convert.ToSingle(height));
            new RectangleF();
        }

        /// <summary>
        /// Проверка столкновения двух объектов.
        /// </summary>
        /// <param name="compareObject">Объект, с которым проверяется столкновение.</param>
        /// <returns>true, если объекты столкнулись</returns>
        public bool IsIntersected(Collision compareObject) =>
            _collisionBox.IntersectsWith(compareObject._collisionBox);

        /// <summary>
        /// Передвигает левый верхний угол Collision box в точку (x, y).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveCollisionBoxTo(double x, double y) {
            _collisionBox.X = Convert.ToSingle(x);
            _collisionBox.Y = Convert.ToSingle(y);
        }
    }
}
