using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using VillageIdle.Scenes;
using VillageIdle.Scenes.MainMenu;

namespace VillageIdle
{
    internal class VillageIdleEngine
    {
        public static VillageIdleEngine Instance = new VillageIdleEngine();

        public Camera2D Camera;
        internal BaseScene ActiveScene;

        public void StartGame()
        {
            //Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_TOPMOST);
            //Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_MAXIMIZED);
            //Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_UNDECORATED);

            Raylib.InitWindow(1280, 768, "Village Idle");

            var monitor = Raylib.GetCurrentMonitor();
            var width = Raylib.GetMonitorWidth(monitor);
            var height = Raylib.GetMonitorHeight(monitor);

            //Raylib.SetWindowSize(width, height);

            Raylib.SetTargetFPS(60);
            Raylib.InitAudioDevice();
            Raylib.SetExitKey(0);

            //if (SettingsManager.Instance.Settings[SettingsManager.SettingKeys.Fullscreen] == 1)
            //{
            //    Raylib.ToggleFullscreen();
            //}

            Camera = new Camera2D
            {
                Zoom = 1.0f,
                Offset = new System.Numerics.Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
            };

            ActiveScene = new MainMenuScene();

            while (!Raylib.WindowShouldClose())
            {
                GameLoop();
            }
        }

        public void GameLoop()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(Camera);

            Raylib.ClearBackground(Color.Black);
            for (int i = 0; i < ActiveScene.Systems.Count; i++)
            {
                ActiveScene.Systems[i].Update(ActiveScene.World);
            }

            Raylib.EndMode2D();

            for (int i = 0; i < ActiveScene.Systems.Count; i++)
            {
                ActiveScene.Systems[i].UpdateNoCamera(ActiveScene.World);
            }
            Raylib.EndDrawing();
        }
    }
}
