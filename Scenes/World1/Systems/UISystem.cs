using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;
using System.Numerics;
using VillageIdle.Extensions;
using VillageIdle.Scenes.World1.Components;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1.Systems
{
    internal class UISystem : GameSystem
    {
        internal override void Update(World world) { }
        internal override void UpdateNoCamera(World world)
        {
            var query = new QueryDescription().WithAny<Interactable>();

            world.Query(in query, (entity) =>
            {
                var interactable = entity.Get<Interactable>();
                if (interactable.IsSelected)
                {
                    var patch = new NPatchInfo
                    {
                        Left = 10,
                        Top = 10,
                        Right = 10,
                        Bottom = 10,
                        Layout = NPatchLayout.NinePatch
                    };

                    var backgroundTexture = TextureManager.Instance.GetTexture(TextureKey.BrownBox);

                    patch.Source = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
                    Raylib.DrawTextureNPatch(backgroundTexture, patch, new Rectangle(0, 0, 250, Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);

                    var TitleTexture = TextureManager.Instance.GetTexture(TextureKey.BlueBox);


                    var text = "Pavekstan";
                    var rect = new Rectangle(10, 10, 230, 40);
                    var size = Raylib.MeasureTextEx(VillageIdleEngine.Instance.Font, text, 24, 0);
                    var position = new Vector2((int)(rect.X + (rect.Width / 2) - (size.X / 2)), (int)rect.Y + rect.Height / 2 - (size.Y / 2));

                    Raylib.DrawTextureNPatch(TitleTexture, patch, rect, Vector2.Zero, 0f, Color.White);
                    Raylib.DrawTextEx(VillageIdleEngine.Instance.Font, "Pavekstan", position, 24, 0f, Color.Black);
                }
            });
        }
    }
}
