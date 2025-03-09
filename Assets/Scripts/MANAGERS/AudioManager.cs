using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource AudioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayAudioClip(int index)
    {
        AudioSource.clip = audioClips[index];
        AudioSource.Play();
    }

    public void StopAudioClip()
    {
        AudioSource.Stop();
    }

    public void PauseAudioClip()
    {
        AudioSource.Pause();
    }


}
