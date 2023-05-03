using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {

    /// <summary>
    /// Класс, содеражащий специфические математические функции.
    /// </summary>
    public static class GameMath {
        /// <summary>
        /// Поворот объекта, описанного точками. 
        /// </summary>
        /// <typeparam name="T">Тип, показывающий как хранятся точки.</typeparam>
        /// <param name="array">Массив точек.</param>
        /// <param name="startIndex">Индекс массива, с которого нужно начать поворот.</param>
        /// <param name="endIndex">Индекс массива, которым нужно закончить поворот.</param>
        /// <param name="angle">Угол поворота объекта в градусах.</param>
        /// <param name="bazePoint">Точка, относительно которой производитяс поворот.</param>>
        public static void Rotate (List<(double, double)> array, int startIndex, int endIndex, double angle, (double, double) bazePoint) {
            //Перевод из градусов в радианы.
            angle = angle * Math.PI / 180;
            double delta_x, delta_y;
            double new_x, new_y;
            
            for (int i = startIndex; i < endIndex; i++) {
                delta_x = array[i].Item1 - bazePoint.Item1;
                delta_y = array[i].Item2 - bazePoint.Item2;

                new_x = delta_x * Math.Cos(angle) - delta_y * Math.Sin(angle);
                new_y = delta_y * Math.Cos(angle) + delta_x * Math.Sin(angle);

                array[i] = (new_x + bazePoint.Item1, new_y + bazePoint.Item2);
            }
        }

        /// <summary>
        /// Перенос точки на angle градусов вокруг bazePoint.
        /// </summary>
        /// <param name="bazePoint">Точка, вокруг которой поворачивается rotatePoint.</param>
        /// <param name="rotatePoint">Точка, которую нужно повернуть.</param>
        /// <param name="angle">Угол, на который нужно повернуть точку.</param>
        /// <returns>Новые координаты поворачиваемой точки.</returns>
        public static (double, double) Rotate((double, double) bazePoint, (double, double) rotatePoint, double angle) {
            angle = angle * Math.PI / 180;

            var delta_x = rotatePoint.Item1 - bazePoint.Item1;
            var delta_y = rotatePoint.Item2 - bazePoint.Item2;

            var new_x = delta_x * Math.Cos(angle) - delta_y * Math.Sin(angle);
            var new_y = delta_y * Math.Cos(angle) + delta_x * Math.Sin(angle);

            return (new_x + bazePoint.Item1, new_y + bazePoint.Item2);
        }

        /// <summary>
        /// Находит гипотенузу.
        /// </summary>
        /// <param name="firstCathet">Катет прямоугольного треугольника.</param>
        /// <param name="secondCathet">Катет прямоугольного треугольника.</param>
        /// <returns>Гипотенуза прямоугольного треугольника.</returns>
        public static double FindHypotenuse (double firstCathet, double secondCathet) =>
                                Math.Sqrt(Math.Pow(firstCathet, 2) + Math.Pow(secondCathet, 2));
    }
}
