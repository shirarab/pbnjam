using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField] 
    private AudioSource gameMusic;

    void Start()
    {
        gameMusic.Play();
    }
}
