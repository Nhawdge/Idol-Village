using Raylib_cs;

namespace VillageIdle.Utilities
{
    internal class TextureManager
    {
        internal static TextureManager Instance { get; } = new();
        internal Dictionary<TextureKey, Texture2D> TextureStore { get; set; } = new();
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
    }
}
