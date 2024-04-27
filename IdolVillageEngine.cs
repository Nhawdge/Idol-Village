﻿using IdolVillage.Scenes;
using IdolVillage.Scenes.MainMenu;
using Raylib_cs;

namespace IdolVillage
{
    internal class IdolVillageEngine
    {
        public static IdolVillageEngine Instance = new IdolVillageEngine();

        public Camera2D Camera;
        internal BaseScene ActiveScene;
        internal Font Font;

        public void StartGame()
        {
            //Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_TOPMOST);
            //Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_MAXIMIZED);
            //Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_UNDECORATED);

            Raylib.InitWindow(1280, 768, "Idol Village");

            var monitor = Raylib.GetCurrentMonitor();
            var width = Raylib.GetMonitorWidth(monitor);
            var height = Raylib.GetMonitorHeight(monitor);

            //Raylib.SetWindowSize(width, height);

            Raylib.SetTargetFPS(60);
            Raylib.InitAudioDevice();
            Raylib.SetExitKey(0);

            Font = Raylib.LoadFont("Assets/Kenney/Fonts/Kenney-Mini.ttf");

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

            Raylib.ClearBackground(Color.RayWhite);
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
