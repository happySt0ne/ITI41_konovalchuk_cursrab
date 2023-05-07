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
        public double x, y, width, height;

        /// <summary>
        /// Инициализация прямоугольного collision box.
        /// </summary>
        /// <param name="x">Координата X верхнего левого угла прямоугольника.</param>
        /// <param name="y">Координата Y верхнего левого угла прямоугольника.</param>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника.</param>
        public Collision(double x, double y, double width, double height) { 
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Проверка столкновения двух объектов.
        /// </summary>
        /// <param name="rect">Объект, с которым проверяется столкновение.</param>
        /// <returns>true, если объекты столкнулись</returns>
        public bool IntersectsWith(Collision rect) {
            if (rect.x < x + width && x < rect.x + rect.width && rect.y > y - height) {
                return y > rect.y - rect.height;
            }

            return false;
        }

        /// <summary>
        /// Передвигает левый верхний угол Collision box в точку (x, y).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveCollisionBoxTo(double x, double y) {
            this.x = x;
            this.y = y;
        }
    }
}
