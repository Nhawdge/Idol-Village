using Arch.Core;
using System.Numerics;
using VillageIdle.Scenes.Systems;

namespace VillageIdle.Scenes
{
    internal abstract class BaseScene
    {
        public BaseScene()
        {
            Systems.Add(new LoadingSystem(this));
        }

        internal void StopSounds()
        {
            //AudioManager.Instance.StopAllSounds();
        }

        internal Dictionary<string, Action> LoadingTasks = new();

        internal World World = World.Create();

        internal List<GameSystem> Systems = new();


        public Vector2 MapEdge;
        public int TileSize;
    }
}
