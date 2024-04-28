using IdolVillage.Extensions;
using IdolVillage.Scenes.Components;
using IdolVillage.Scenes.World1.Systems;
using IdolVillage.Utilities;
using Raylib_cs;
using System.Numerics;

namespace IdolVillage.Scenes.World1
{
    internal class World1Scene : BaseScene
    {
        public World1Scene()
        {
            var options = new List<SpriteKey> { SpriteKey.Grass1, SpriteKey.Grass2 };
            LoadingTasks.Add("Build Map", () =>
            {
                //Raylib.SetRandomSeed((uint)DateTime.Now.Ticks);
                var noiseMap = Raylib.GenImagePerlinNoise(1000, 1000, Random.Shared.Next(), Random.Shared.Next(), Random.Shared.NextSingle());
                for (int x = 0; x < 100; x++)
                {
                    for (int y = 0; y < 100; y++)
                    {
                        var tile = new Sprite(TextureKey.MapTiles, "Assets/Art/Tiles", 2);
                        tile.SpriteWidth = 64;
                        tile.SpriteHeight = 64;

                        tile.Color = Color.White;
                        tile.Position.X = x * 128;
                        tile.Position.Y = y * 128;
                        World.Create(tile, new GroundLayer());

                        var red = Raylib.GetImageColor(noiseMap, x * 10, y * 10).R;
                        var blue = Raylib.GetImageColor(noiseMap, x * 10, y * 10).B;

                        //Console.WriteLine($"Red Value: {red} at {x},{y}");
                        if (red > 1200) //intentionally broken
                        {
                            var treeVersion = red switch
                            {
                                > 150 => SpriteKey.Tree3,
                                > 140 => SpriteKey.Tree2,
                                _ => SpriteKey.Tree4
                            };
                            var treeRender = new Render(TextureKey.MedievalSpriteSheet);
                            treeRender.Position = new Vector2(x * 128 + Random.Shared.Next(0, 128), y * 128 + Random.Shared.Next(0, 128));
                            treeRender.SetSource(SpriteSheetStore.Instance.GetDecorSheetSource(treeVersion));
                            World.Create(treeRender, new StructureLayer());
                            if (blue > 120)
                            {
                                treeRender = new Render(TextureKey.MedievalSpriteSheet);
                                treeRender.Position = new Vector2(x * 128 + Random.Shared.Next(0, 128), y * 128 + Random.Shared.Next(0, 128));
                                treeRender.SetSource(SpriteSheetStore.Instance.GetDecorSheetSource(treeVersion));
                                World.Create(treeRender, new StructureLayer());
                            }
                        }
                    }
                }

                var center = new Sprite(TextureKey.ProductionStructures, "Assets/Art/ProductionStructures",2);
                center.Play("villagecenter-idle");
                center.SpriteWidth = 64;
                center.SpriteHeight = 64;
                center.Color = Color.White;
                center.Position.X = 50 * 128;
                center.Position.Y = 50 * 128;
                World.Create(center, new StructureLayer(), new Interactable()
                {
                    Name = "Pavekstan Village",
                    ShowResearch = true,
                    ShowUnits = true,
                    ShowProducers = true,
                    ShowResources = true,
                    ShowAssignments = false,
                    ShowCosts = false,
                });

                //var treeRender = new Render(TextureKey.MedievalSpriteSheet);
                //treeRender.Position = new Vector2(100, 100);
                //treeRender.SetSource(SpriteSheetStore.Instance.GetDecorSheetSource(SpriteKey.Tree));

                //World.Create(treeRender, new StructureLayer());
            });



            LoadingTasks.Add("Loading", () =>
            {
                Systems.Add(new RenderSystem());
                Systems.Add(new InteractionSystem());
                Systems.Add(new UISystem());
                Systems.Add(new RecruitmentSystem());
                Systems.Add(new UnitMovementSystem());
                Systems.Add(new ProductionSystem());
                Systems.Add(new BeliefSystem());
                Systems.Add(new CameraSystem());
                Systems.Add(new ToolTipSystem()); // Always last plz
            });

            LoadingTasks.Add("Ready", () =>
            {
                IdolVillageEngine.Instance.Camera.Target.X = 50 * 128;
                IdolVillageEngine.Instance.Camera.Target.Y = 50 * 128;
            });
        }
    }
}
