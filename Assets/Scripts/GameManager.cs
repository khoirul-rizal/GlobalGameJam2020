using UnityEngine;
using UnityEditor;
using UnityEngine.Video;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
      GameState
      0 = CountDown
      1 = GameBegin
      2 = ScoreShow
    */
    public Text timerTxt;
    public CountScore countScore;
    public GameObject[] stageMaster;
    public int currentStage = 0;
    public int gameState = 0;
  	VideoPlayer videoPlayer;
    public VideoClip videoClip;
    [SerializeField]
    float timeLeft = 35f; 
		[SerializeField]
		float time = 0;
    CharacterController characterController;
    [SerializeField]
    GameObject[] objective;

    void Awake()
    {
      characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }
    void StartStage(int stage)
    {
      StageMaster stageMaster = this.stageMaster[stage].GetComponent<StageMaster>();
      timeLeft = stageMaster.time;
      this.stageMaster[stage].SetActive(true);
      stageMaster.SetActive();
      GettingObjective();
    }
    void ResetStage(int stage)
    {
      StageMaster stageMaster = this.stageMaster[stage].GetComponent<StageMaster>();
      timeLeft = stageMaster.time;
      stageMaster.SetDeactive();
      // GettingObjective();
    }

    void GettingObjective()
    {
      objective = GameObject.FindGameObjectsWithTag("Finish");
    }
    void Start()
    {
      // stageMaster = GameObject.FindGameObjectsWithTag("StageMaster");
      StartStage(0);
      CountDown();
    }
    void ResetCountDown()
    {
      gameState = 0;
      time = 0;
      CountDown();
    }
    void GameRules()
    {
      GettingObjective();
      if (timeLeft <= 0)
      {
        timeLeft = 0;
        gameState = 2;
      }
      if (objective.Length == 0 && time > 4)
      {
        gameState = 2;
      }
    }
		void Update()
		{
      GameRules();
      CountDownState();
      GameBegin();
      GameFinish();
		}

    bool isGameFinish = false;
    void GameFinish()
    {
      if(gameState != 2) {
        isGameFinish = false;
        return;
      }
      if(!isGameFinish)
      {
        isGameFinish = true;
        countScore.ShowScore(timeLeft,objective);
        characterController.ResetPlayerState();
      }
     

      if(Input.GetButtonDown("Interact"))
      {
        if(currentStage+1 == stageMaster.Length) Debug.Log("Next Scene");
        else {
          ResetStage(currentStage);
          countScore.HideScore();
          ResetCountDown();
          currentStage++; 
          StartStage(currentStage);
        }
      }
      if(Input.GetButtonDown("Submit"))
      {
        ResetStage(currentStage);
        countScore.HideScore();
        ResetCountDown();
        StartStage(currentStage);
      }

    }
    void GameBegin()
    {
      if (gameState != 1) return;
      characterController.enabled = true;
      timeLeft -= Time.deltaTime;
      timerTxt.text = timeLeft.ToString();
    }
    void CountDownState()
    {
      if (gameState != 0) return;
      characterController.enabled = false;
			time += Time.deltaTime;
			if(time > 4 && gameState == 0) {
        videoPlayer.Stop();
        videoPlayer.targetCameraAlpha = 0;
        gameState = 1;

      }
    }
    void CountDown()
    {
      GameObject camera = GameObject.Find("Main Camera");
      videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
      videoPlayer.targetCameraAlpha = 0.5F;
      videoPlayer.playOnAwake = true;
      videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
      string path = AssetDatabase.GetAssetPath(videoClip);
      videoPlayer.url =  path;
      videoPlayer.frame = 100;
      videoPlayer.isLooping = true;
      videoPlayer.loopPointReached += EndReached;
      videoPlayer.Play();
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }
}
