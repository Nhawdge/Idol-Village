using Raylib_cs;
using System.Numerics;
using VillageIdle.Extensions;
using VillageIdle.Scenes.Components;
using VillageIdle.Scenes.World1.Systems;
using VillageIdle.Utilities;

namespace VillageIdle.Scenes.World1
{
    internal class World1Scene : BaseScene
    {
        public World1Scene()
        {
            var options = new List<SpriteKey> { SpriteKey.Grass1, SpriteKey.Grass2 };
            base.LoadingTasks.Add("Build Map", () =>
            {
                //Raylib.SetRandomSeed((uint)DateTime.Now.Ticks);
                var noiseMap = Raylib.GenImagePerlinNoise(1000, 1000, Random.Shared.Next(), Random.Shared.Next(), Random.Shared.NextSingle());
                for (int x = 0; x < 100; x++)
                {
                    for (int y = 0; y < 100; y++)
                    {
                        var tile = new Render(TextureKey.MedievalSpriteSheet);
                        tile.SetSource(SpriteSheetStore.Instance.GetTileSheetSource(options.GetRandom()));
                        tile.Color = Color.White;
                        tile.Position.X = x * 128;
                        tile.Position.Y = y * 128;
                        World.Create(tile, new GroundLayer());

                        var red = Raylib.GetImageColor(noiseMap, x * 10, y * 10).R;
                        var blue = Raylib.GetImageColor(noiseMap, x * 10, y * 10).B;

                        //Console.WriteLine($"Red Value: {red} at {x},{y}");
                        if (red > 120)
                        {
                            var treeVersion = red switch
                            {
                                > 150 => SpriteKey.Tree3,
                                > 140 => SpriteKey.Tree2,
                                _ => SpriteKey.Tree4
                            };
                            var treeRender = new Render(TextureKey.MedievalSpriteSheet);
                            treeRender.Position = new Vector2(x * 128, y * 128);
                            treeRender.SetSource(SpriteSheetStore.Instance.GetDecorSheetSource(treeVersion));
                            World.Create(treeRender, new StructureLayer());
                            if (blue > 120)
                            {
                                treeRender = new Render(TextureKey.MedievalSpriteSheet);
                                treeRender.Position = new Vector2(x * 128, y * 128);
                                treeRender.SetSource(SpriteSheetStore.Instance.GetDecorSheetSource(treeVersion));
                                World.Create(treeRender, new StructureLayer());
                            }
                        }
                    }
                }

                var center = new Render(TextureKey.MedievalSpriteSheet);
                center.SetSource(SpriteSheetStore.Instance.GetStructureSheetSource(SpriteKey.CityCenter));
                center.Color = Color.White;
                center.Position.X = 50 * 128;
                center.Position.Y = 50 * 128;
                center.OriginPos = Render.OriginAlignment.LeftTop;
                World.Create(center, new StructureLayer(), new Interactable());

                //var treeRender = new Render(TextureKey.MedievalSpriteSheet);
                //treeRender.Position = new Vector2(100, 100);
                //treeRender.SetSource(SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Tree));

                //World.Create(treeRender, new StructureLayer());
            });



            base.LoadingTasks.Add("Loading", () =>
            {
                Systems.Add(new RenderSystem());
                Systems.Add(new CameraSystem());
                Systems.Add(new InteractionSystem());
                Systems.Add(new UISystem());
                Systems.Add(new RecruitmentSystem());
                Systems.Add(new UnitMovementSystem());
                Systems.Add(new ProductionSystem());
            });

            base.LoadingTasks.Add("Ready", () =>
            {
                VillageIdleEngine.Instance.Camera.Target.X = 50 * 128;
                VillageIdleEngine.Instance.Camera.Target.Y = 50 * 128;
            });
        }
    }
}
