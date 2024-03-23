using Raylib_CsLo;

namespace VillageIdle.Utilities
{
    internal class TextureManager
    {
        internal static TextureManager Instance { get; } = new();
        internal Dictionary<TextureKey, Texture> TextureStore { get; set; } = new();
        internal Dictionary<string, Texture> TextureCache { get; set; } = new();

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

        internal Texture GetTexture(TextureKey key)
        {
            if (TextureStore.Count <= 0)
            {
                LoadTextures();
            }
            return TextureStore[key];
        }
        internal Texture? GetCachedTexture(string key)
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
