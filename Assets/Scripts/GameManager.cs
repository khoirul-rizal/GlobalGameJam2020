using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /*
      GameState
      0 = CountDown
      1 = GameBegin
      2 = ScoreShow
    */
    public Text timerTxt;
    public GameObject pauseGO;
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

    public void PauseQuit()
    {
      SceneManager.LoadScene(0);
    }
    public void PauseRestart()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseUnpause()
    {
      Debug.Log("You Unpause");
      pauseGO.SetActive(false);
      gameState = 1;
      characterController.enabled = true;
    }
    void PauseRule()
    {
      if(Input.GetButtonDown("Pause"))
      {
        Debug.Log("You PRess it");
        
        if(gameState == 4){
          PauseUnpause();
        }
        else if(gameState == 1){
          Debug.Log("You CHange");
          pauseGO.SetActive(true);
          gameState = 4;
          characterController.enabled = false;
        }
      }
    }
		void Update()
		{
      PauseRule();
      if(gameState == 4) return;
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
        if(currentStage+1 == stageMaster.Length) {
          if(SceneManager.GetActiveScene().buildIndex == 5) SceneManager.LoadScene(0);
          else SceneManager.LoadScene("Level"+SceneManager.GetActiveScene().buildIndex);
        }
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
      timerTxt.text = (Mathf.Round(timeLeft * 10f) / 10f).ToString();
    }
    void CountDownState()
    {
      if (gameState != 0) return;
      timerTxt.text = "00:00";
      characterController.enabled = false;
			time += Time.deltaTime;
			if(time > 4 && gameState == 0 ) {
        videoPlayer.Stop();
        videoPlayer.targetCameraAlpha = 0;
        gameState = 1;

      }
    }
    void CountDown()
    {
      GameObject camera = GameObject.Find("Main Camera");
      if(GetComponent<UnityEngine.Video.VideoPlayer>() != null) Destroy(GetComponent<UnityEngine.Video.VideoPlayer>());
      videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
      videoPlayer.targetCameraAlpha = 0.5F;
      videoPlayer.playOnAwake = true;
      videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
      videoPlayer.clip =  videoClip;
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
