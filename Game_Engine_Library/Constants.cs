using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Library {
    public static class Constants {
        public const double PANZARS_WIDTH = 0.3;
        public const double PANZARS_HEIGHT = 0.3;
        public const double PANZARS_SPEED = 0.005;
        public const double HEIGHT_TO_CREATE_PANZARS = -0.7;
        public const double PANZAR_X_COORDINATE_SPAWNPOINT = 0.5;

        public const int MUZZLE_ROTATION_SPEED = 5;
        public const double MAX_COOLDOWN = 3;
        public const int START_AMMO = 40;

        public const double BULLETS_X_START_SPEED = 0.07;
        public const double BULLETS_Y_START_SPEED = 0.07;
        public const double GRAVITY_SCALE = -0.006;
        public const double BULLETS_HEIGHT = 0.07;
        public const double BULLETS_WIDTH = 0.07;
        public const int BULLET_DAMAGE = 10;

        public const double PLANE_X_COORDINATE_SPAWN = -1.3;
        public const double PLANE_Y_COORDINATE_SPAWN = 0.7;
        public const double PLANE_HEIGHT = 0.3;
        public const double PLANE_WIDTH = 0.4;
        public const double PLANE_X_SPEED = 0.015;
        public const double PLANE_SPAWN_MAX_COOLDOWN = 4;

        public const string PANZAR_MUZZLE_TEXTURE_PATH = @"../../../Game_Engine_Library/Resources/PanzarMuzzle.bmp";
        public const string PANZAR_TURRET_TEXTURE_PATH = @"../../../Game_Engine_Library/Resources/PanzarTurret.bmp";
        public const string PANZAR_TRACK_TEXTURE_PATH = @"../../../Game_Engine_Library/Resources/PanzarTrack.bmp";
        public const string BULLET_TEXTURE_PATH = @"../../../Game_Engine_Library/Resources/Bullet.bmp";
        public const string WALL_TEXTURE_PATH = @"../../../Game_Engine_Library/Resources/Mountain.bmp";
        public const string BACKGROUND_TEXTURE_PATH = @"../../../Game_Engine_Library/Resources/Background2.bmp";
        public const string PLANE_TEXTURE_PATH = @"../../../Game_Engine_Library/Resources/Plane.bmp";
    } 
}
