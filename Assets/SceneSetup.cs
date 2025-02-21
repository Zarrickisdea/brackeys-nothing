using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public Player player;
    public AudioManager audioManager;

    void Start()
    {
        player.AddObserver(audioManager);
    }
}