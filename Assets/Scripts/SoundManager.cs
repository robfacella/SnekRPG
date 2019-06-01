using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    //Overkill, but easily expandable sound management.
    public enum SFX
    {
        pop,
    }
    public static void playSound(SFX sound)
    {
        GameObject sGameObject = new GameObject("Sound");
        AudioSource aSource = sGameObject.AddComponent<AudioSource>();
        aSource.PlayOneShot(GetAudio(sound));
    }
    private static AudioClip GetAudio(SFX sound)
    {
        foreach(GameAssets.SFXLinker pair in GameAssets.instance.soundsArray)
        {
            if (pair.sound == sound)
                return pair.aClip;
        }
        Debug.LogError("Couldn't find sound " + sound);
        return null;
    }
}
