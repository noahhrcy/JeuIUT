using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DoorTriggerSalle1 : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called");

        if (other.CompareTag("Player") && ElementalInventory.Instance.contains("Cle", 1, Color.yellow))
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Video", "porte_anim.mov");
            videoPlayer.url = videoPath;

            videoPlayer.Play();
            videoPlayer.loopPointReached += LoadSalle1Scene; // S'abonner à l'événement de fin de vidéo
        }
        else if (other.CompareTag("Player") && !ElementalInventory.Instance.contains("Cle", 1, Color.yellow))
        {
            Debug.Log("Vous n'avez pas la clé nécessaire.");
        }
    }

    void LoadSalle1Scene(VideoPlayer vp)
    {

        SceneManager.LoadScene("Salle 1");
    }
}
