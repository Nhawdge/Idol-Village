using Raylib_cs;

namespace IdolVillage.Utilities
{
    internal class TextureManager
    {
        internal static TextureManager Instance { get; } = new();

        internal Dictionary<TextureKey, Texture2D> TextureStore { get; set; } = new();
        internal Dictionary<TextureKey, NPatchInfo> NPatchInfos { get; set; } = new();


        internal Dictionary<string, Texture2D> TextureCache { get; set; } = new();

        private TextureManager()
        {
            LoadTextures();
        }

        private void LoadTextures()
        {
            TextureStore.Add(TextureKey.Empty, Raylib.LoadTexture(""));
            TextureStore.Add(TextureKey.BrownBox, Raylib.LoadTexture("Assets/Kenney/UI-Adventure-Pack/PNG/panel_brown.png"));
            TextureStore.Add(TextureKey.BlueBox, Raylib.LoadTexture("Assets/Kenney/UI-Adventure-Pack/PNG/panel_blue.png"));
            TextureStore.Add(TextureKey.BeigeBox, Raylib.LoadTexture("Assets/Kenney/UI-Adventure-Pack/PNG/panel_beige.png"));
            TextureStore.Add(TextureKey.ArrowSilverRight, Raylib.LoadTexture("Assets/Kenney/UI-Adventure-Pack/PNG/arrowSilver_right.png"));
            TextureStore.Add(TextureKey.ArrowSilverDown, Raylib.LoadTexture("Assets/Kenney/UI-Adventure-Pack/PNG/arrowSilver_down.png"));
            TextureStore.Add(TextureKey.ArrowSilverLeft, Raylib.LoadTexture("Assets/Kenney/UI-Adventure-Pack/PNG/arrowSilver_left.png"));
            TextureStore.Add(TextureKey.ArrowSilverUp, Raylib.LoadTexture("Assets/Kenney/UI-Adventure-Pack/PNG/arrowSilver_up.png"));

            TextureStore.Add(TextureKey.MedievalSpriteSheet, Raylib.LoadTexture("Assets/Kenney/RTS-Medieval/Spritesheet/medievalRTS_spritesheet@2.png"));
            TextureStore.Add(TextureKey.ProductionStructures, Raylib.LoadTexture("Assets/Art/ProductionStructures.png"));
            TextureStore.Add(TextureKey.MapTiles, Raylib.LoadTexture("Assets/Art/Tiles.png"));
            TextureStore.Add(TextureKey.Units, Raylib.LoadTexture("Assets/Art/Units.png"));

            TextureStore.Add(TextureKey.MainLogo, Raylib.LoadTexture("Assets/Logo.png"));
            TextureStore.Add(TextureKey.RaylibLogo, Raylib.LoadTexture("Assets/raylib_logo_animation.png"));

            var mostPatchInfos = new NPatchInfo
            {
                Left = 10,
                Top = 10,
                Right = 10,
                Bottom = 10,
                Layout = NPatchLayout.NinePatch
            };

            NPatchInfos.Add(TextureKey.BlueBox, mostPatchInfos);
            NPatchInfos.Add(TextureKey.BrownBox, mostPatchInfos);
            NPatchInfos.Add(TextureKey.BeigeBox, mostPatchInfos);

        }

        internal Texture2D GetTexture(TextureKey key)
        {
            if (TextureStore.Count <= 0)
            {
                LoadTextures();
            }
            return TextureStore[key];
        }
        internal Texture2D? GetCachedTexture(string key)
        {
            if (TextureCache.TryGetValue(key, out var texture))
                return texture;
            return null;
        }
    }

    internal enum TextureKey
    {
        Empty,
        BrownBox,
        BlueBox,
        BeigeBox,
        MedievalSpriteSheet,
        ArrowBlue,
        ArrowSilverRight,
        ArrowSilverUp,
        ArrowSilverLeft,
        ArrowSilverDown,
        MainLogo,
        RaylibLogo,
        ProductionStructures,
        MapTiles,
        Units,
    }
}
