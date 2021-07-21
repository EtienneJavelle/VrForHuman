using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Etienne {
    public static class Extensions {

        #region List

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static T Last<T>(this List<T> list) {
            return list[list.Count - 1];
        }

        #endregion

        #region string

        /// <summary>
        /// Remove everything afer the last specified char
        /// </summary>
        /// <param name="c">Char to remove from</param>
        /// <param name="keepChar">Do you want to keep the char</param>
        /// <returns></returns>
        public static string RemoveAfter(this string input, char c, bool keepChar = false) {
            int index = input.LastIndexOf(c);
            if(index > 0) {
                return input.Substring(0, keepChar ? index + 1 : index);
            }

            return input;
        }

        public static string[] RemoveAt(this string[] array, int index) {
            return array.Where(o => o != array[index]).ToArray();
        }

        #endregion

        #region Vector3

        /// <summary>
        /// Get a direction from start to end
        /// </summary>
        /// <param name="start">The start of the direction</param>
        /// <param name="end">The end of the direction</param>
        /// <returns>end - start</returns>
        public static Vector3 Direction(this Vector3 start, Vector3 end) {
            return end - start;
        }

        /// <summary>
        /// Get a direction from start to end
        /// </summary>
        /// <param name="start">The start of the direction</param>
        /// <param name="end">The end of the direction</param>
        /// <returns>end - start</returns>
        public static Vector3 Direction(this Transform start, Transform end) {
            return start.position.Direction(end.position);
        }
        #endregion

        #region Audio

        public static AudioSource SetSoundToSource(this AudioSource source, Sound sound) {
            source.clip = sound.Clip;
            source.loop = sound.Parameters.Loop;
            source.pitch = sound.Parameters.Pitch;
            source.volume = sound.Parameters.Volume;
            return source;
        }

        public static AudioSource FindFreeAudiosource(this Component instance, List<AudioSource> audioSources) {
            foreach(AudioSource audioSource in audioSources) {
                if(!audioSource.isPlaying) {
                    return audioSource;
                }
            }
            AudioSource newAudioSource = instance.gameObject.AddComponent<AudioSource>();
            newAudioSource.playOnAwake = false;
            audioSources.Add(newAudioSource);
            return audioSources.Last();
        }
        #endregion
    }
}