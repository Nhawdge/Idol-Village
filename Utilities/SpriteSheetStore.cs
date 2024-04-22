using Raylib_cs;
using System.Xml.Linq;

namespace VillageIdle.Utilities
{
    internal class SpriteSheetStore
    {
        internal static SpriteSheetStore Instance { get; } = new();
        internal Dictionary<string, Rectangle> SpriteStore { get; set; } = new();
        internal Dictionary<string, Texture2D> TextureCache { get; set; } = new();

        private SpriteSheetStore()
        {
            LoadSprites();
        }

        private void LoadSprites()
        {
            var Xml = XDocument.Load("Assets/Kenney/RTS-Medieval/Spritesheet/medievalRTS_spritesheet@2.xml");
            foreach (var child in Xml.Root.Descendants())
            {
                var rectangle = new Rectangle();
                rectangle.X = int.Parse(child.Attribute("x").Value);
                rectangle.Y = int.Parse(child.Attribute("y").Value);
                rectangle.Width = int.Parse(child.Attribute("width").Value);
                rectangle.Height = int.Parse(child.Attribute("height").Value);

                SpriteStore.Add(child.Attribute("name").Value, rectangle);
            }
            //SpriteStore.Add(
        }

        internal Rectangle GetSpriteSheetSource(string key)
        {
            if (SpriteStore.Count <= 0)
            {
                LoadSprites();
            }
            return SpriteStore[key.ToString()];
        }
        internal Rectangle GetTileSheetSource(SpriteKey key)
        {
            return GetSpriteSheetSource($"medievalTile_{((int)key).ToString("00")}.png");
        }
        internal Rectangle GetStructureSheetSource(SpriteKey key)
        {
            return GetSpriteSheetSource($"medievalStructure_{((int)key).ToString("00")}.png");
        }
        internal Rectangle GetDecorSheetSource(SpriteKey key)
        {
            return GetSpriteSheetSource($"medievalEnvironment_{((int)key).ToString("00")}.png");
        }
        internal Rectangle GetUnitSheetSource(SpriteKey key)
        {
            return GetSpriteSheetSource($"medievalUnit_{((int)key).ToString("00")}.png");
        }
    }

    internal enum SpriteKey
    {
        None,
        Woman = 24,
        Man = 1,
        Tree = 1,
        Tree2 = 2,
        Tree3 = 3,
        Tree4 = 4,
        Tree5 = 5,
        Tent = 7,
        CityCenter = 12,
        Bushes = 19,
        BigFarm = 55,
        BigFarmHarvest = 56,
        Grass1 = 57,
        Grass2 = 58,
    }
}
