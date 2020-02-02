using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{

    public VideoClip videoClip;
    void Start()
    {
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();

        videoPlayer.playOnAwake = false;

        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        videoPlayer.targetCameraAlpha = 0.5F;

        // string path = "Assets/Graphics/MainMenu.mp4";
        // Debug.Log("MainMenu --- "+path);
        videoPlayer.clip = videoClip;

        videoPlayer.frame = 100;

        videoPlayer.isLooping = true;

        videoPlayer.loopPointReached += EndReached;

        videoPlayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }

    void Update()
    {
        if(Input.GetButton("Interact"))
            SceneManager.LoadScene("Level0");
    }
}
