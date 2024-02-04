using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField] 
    private AudioSource gameMusic;
    
    [SerializeField] 
    private AudioSource sfx;

    void Start()
    {
        gameMusic.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        sfx.PlayOneShot(sound);
    }

}
