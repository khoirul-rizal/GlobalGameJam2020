using UnityEngine;
using UnityEditor;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    /*
      GameState
      0 = CountDown
      1 = GameBegin
      2 = ScoreShow
    */
    public int gameState = 0;
  	VideoPlayer videoPlayer;
    public VideoClip videoClip;
    [SerializeField]
    float timeLeft = 35f; 
		[SerializeField]
		float time = 0;
    void Start()
    {
      CountDown();
    }
		void Update()
		{
      CountDownState();
      GameBegin();
		}
    void GameBegin()
    {
      timeLeft -= Time.deltaTime;
    }
    void CountDownState()
    {
			time += Time.deltaTime;
			if(time > 5.5 && gameState == 0) {
        videoPlayer.targetCameraAlpha = 0;
        gameState = 1;
      }
    }
    void CountDown()
    {
      GameObject camera = GameObject.Find("Main Camera");
      videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
      videoPlayer.playOnAwake = true;
      videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
      videoPlayer.targetCameraAlpha = 0.5F;
      string path = AssetDatabase.GetAssetPath(videoClip);
      videoPlayer.url =  path;
      videoPlayer.frame = 100;
      videoPlayer.isLooping = false;
      videoPlayer.loopPointReached += EndReached;
      videoPlayer.Play();
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }
}
