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
        public static void Rotate (List<(double, double)> array, int startIndex, int endIndex, double angle, 
                                                                        (double, double) bazePoint) {
            //Перевод из градусов в радианы.
            angle = angle * Math.PI / 180;
            double delta_x, delta_y;
            double new_x, new_y;
            // Функция работает неправильно. Помни что (0, 0) в центре канваса. Можешь попробовать способ Аксдэна. 
            for (int i = startIndex; i <= endIndex; i++) {
                delta_x = array[i].Item1 - bazePoint.Item1;
                delta_y = array[i].Item2 - bazePoint.Item2;

                new_x = delta_x * Math.Cos(angle) - delta_y * Math.Sin(angle);
                new_y = delta_y * Math.Cos(angle) + delta_x * Math.Sin(angle);

                array[i] = (new_x + bazePoint.Item1, new_y + bazePoint.Item2);
            }
        }
    }
}
