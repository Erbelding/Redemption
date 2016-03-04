using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
namespace RedemptionEngine
{
    public static class GameAudio
    {
        //Attributes
        public static Dictionary<string, SoundEffect> Effects = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        private static float effectVolume = .5f;
        private static float musicVolume = .5f;

        public static float EffectVolume
        {
            get { return effectVolume; }
            set { effectVolume = MathHelper.Clamp(value, 0, 1); }
        }

        public static float MusicVolume
        {
            get { return musicVolume; }
            set { musicVolume = MathHelper.Clamp(value, 0, 1); }
        }

        public static void AddEffect(string name, SoundEffect effect)
        {
            //Add effect
            Effects.Add(name, effect);
        }

        public static void AddSong(string name, Song song)
        {
            //Add song
            Songs.Add(name, song);
        }

        public static void PlayEffect(string effect)
        {
            SoundEffectInstance effectInstance = Effects[effect].CreateInstance();

            effectInstance.Volume *= effectVolume;

            effectInstance.Play();
        }

       

        public static void PlaySong(string song, bool loop)
        {
            //If song exists
            if (Songs.ContainsKey(song))
            {
                if (loop)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(Songs[song]);
                }
                else
                {
                    MediaPlayer.IsRepeating = false;
                    MediaPlayer.Play(Songs[song]);
                }
            }
        }

        
    }
}
