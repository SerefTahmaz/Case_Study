using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The audio manager
// I took sounds from freesound.org
public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    static AudioSource audioSourcelong;
    // Gets whether or not the audio manager has been initialized
    public static bool Initialized
    {
        get { return initialized; }
    }


    // Initializes the audio manager
    public static void Initialize(AudioSource[] sources)
    {
        initialized = true;
        audioSource = sources[0];
        audioSourcelong = sources[1];

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.MenuButtonClick,
            Resources.Load<AudioClip>("MenuButtonClick"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.PlayerSlide,
            Resources.Load<AudioClip>("PlayerSlide"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.SpikeTrap,
            Resources.Load<AudioClip>("SpikeTrap"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.CoinPickup,
            Resources.Load<AudioClip>("CoinPickup"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.LightObstacleImpact,
            Resources.Load<AudioClip>("LightObstacleImpact"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.BallCollected,
            Resources.Load<AudioClip>("BallCollected"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.GameoverSound,
            Resources.Load<AudioClip>("GameoverSound"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.LevelStartSound,
            Resources.Load<AudioClip>("LevelStartSound"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.TeleportSound,
            Resources.Load<AudioClip>("TeleportSound"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.LavaSound,
            Resources.Load<AudioClip>("LavaSound"));

        //Add audio enum name and corresponding audioclip to dictionary
        audioClips.Add(AudioClipName.MainMenuMusic,
            Resources.Load<AudioClip>("MainMenuMusic"));
    }


    // Plays the audio clip with the given name
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    // Plays the long audio clip with the given name
    // "Long" meaning looping
    public static void PlayLong(AudioClipName name)
    {
        audioSourcelong.loop = true;
        audioSourcelong.PlayOneShot(audioClips[name]);
    }

    // Stops the long audio clip
    public static void StopLong()
    {
        audioSourcelong.Stop();
    }

}
