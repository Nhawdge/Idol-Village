using Arch.Core;
using Arch.Core.Extensions;
using Raylib_cs;

namespace VillageIdle.Scenes.MainMenu.Systems
{
    internal class MenuMusicSystem : GameSystem
    {
        internal override void Update(World world)
        {
            //var singletonEntity = world.QueryFirst<Singleton>();
            //var singleton = singletonEntity.Get<Singleton>();

            //var music = AudioManager.Instance.GetMusic(singleton.Music);
            //if (Raylib.IsMusicStreamPlaying(music))
            //{
            //    Raylib.UpdateMusicStream(music);
            //    Raylib.SetMasterVolume(SettingsManager.Instance.Settings[SettingsManager.SettingKeys.MainVolume] / 100f);
            //    Raylib.SetMusicVolume(music, SettingsManager.Instance.Settings[SettingsManager.SettingKeys.MusicVolume] / 100f);
            //}
            //else
            //{
            //    Raylib.PlayMusicStream(AudioManager.Instance.GetMusic(singleton.Music));
            //}
        }
    }
}
