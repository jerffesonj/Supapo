using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [Header("Status")]
    public Status actualStatus;

    [Header("HP Script")]
    [SerializeField] HPScript playerHpScript;
    [SerializeField] HPScript enemyHpScript;

    [Header("Punch Script")]
    [SerializeField] PunchScript playerPunchScript;
    [SerializeField] PunchScript enemyPunchScript;

    [Header("PunchIA")]
    [SerializeField] PunchIA punchIa;

    [Header("Animator Script")]
    [SerializeField] AnimationScript playerAnim;
    [SerializeField] AnimationScript enemyAnim;

    [Header("Canvas Script")]
    [SerializeField] CanvasScript canvasScript;

    [Header("Canvas Transform")]
    [SerializeField] Transform GameCanvas;

    [Header("GameObjects")]
    [SerializeField] GameObject cameraAnimation;
    [SerializeField] GameObject cameraMain;

    public bool roundTextBool;
    public bool winTextBool;

    public int totalTime;

    public float countDown;

    public bool pause;
    public float pausetimer;

    public float timeFloat;
    public int timeInt;

    public int comboPlayer;
    public int comboEnemy;

    public int maxComboPlayer;
    public int maxComboEnemy;

    public float hpRestantePlayer;
    public float hpRestanteEnemy;

    public int playerRoundWon;
    public int enemyRoundWon;

    public bool playerRoundWonBool;
    public bool enemyRoundWonBool;

    bool damageBool = false;

    bool instantiateOnce = false;

    public enum Status
    {
        Apresentation,
        BeforeRound1,
        StartRound1,
        OnRound1,
        EndRound1,
        BeforeRound2,
        StartRound2,
        OnRound2,
        EndRound2,
        BeforeFinalRound,
        StartFinalRound,
        OnFinalRound,
        EndFinalRound,
        GoToGameOver
    }

    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            SoundGameplayController.instance.audioSource.loop = true;
            SoundGameplayController.instance.audioSource.clip = SoundGameplayController.instance.bgSound1;
            SoundGameplayController.instance.audioSource.volume = 0.05f;
            SoundGameplayController.instance.audioSource.Play();

            SoundGameplayController.instance.audioSource1.loop = true;
            SoundGameplayController.instance.audioSource1.clip = SoundGameplayController.instance.bgSound2;
            SoundGameplayController.instance.audioSource1.volume = 0.05f;
            SoundGameplayController.instance.audioSource1.Play();
        }

        actualStatus = Status.Apresentation;

    }
    // Update is called once per frame
    void Update()
    {
        PauseUnPauseMusic();

        if (pausetimer >= 1)
        {
            pausetimer = 1;
        }

        CheckMaxCombos();

        CheckActualStatus();
        

        timeInt = totalTime - Mathf.RoundToInt(timeFloat);
        canvasScript.timeText.text = timeInt.ToString();
    }


    void CheckActualStatus()
    {
        switch (actualStatus)
        {
            case Status.Apresentation:
                StartCoroutine(CameraAnimation(3));
                break;

            case Status.BeforeRound1:
                {
                    StartCoroutine(BeforeRound());
                }
                break;

            case Status.StartRound1:
                if (!roundTextBool)
                {
                    roundTextBool = true;
                    StartCoroutine(CountDownStartRound());
                }
                break;

            case Status.OnRound1:
                if (timeInt > 0)
                {
                    timeFloat += Time.deltaTime;
                }
                Timer();
                CheckWinner(Status.EndRound1);
                break;

            case Status.EndRound1:
                if (!winTextBool)
                {
                    winTextBool = true;
                    StartCoroutine(WinRound());
                }
                break;

            case Status.BeforeRound2:
                StartCoroutine(BeforeRound());
                break;

            case Status.StartRound2:
                if (!roundTextBool)
                {
                    roundTextBool = true;
                    StartCoroutine(CountDownStartRound());
                }
                break;

            case Status.OnRound2:
                if (timeInt > 0)
                {
                    timeFloat += Time.deltaTime;
                }
                Timer();
                CheckWinner(Status.EndRound2);
                break;

            case Status.EndRound2:
                if (!winTextBool)
                {
                    winTextBool = true;
                    StartCoroutine(WinRound());
                }
                break;

            case Status.BeforeFinalRound:
                StartCoroutine(BeforeRound());
                break;
            case Status.StartFinalRound:
                if (!roundTextBool)
                {
                    roundTextBool = true;
                    StartCoroutine(CountDownStartRound());
                }
                break;

            case Status.OnFinalRound:
                if (timeInt > 0)
                {
                    timeFloat += Time.deltaTime;
                }
                Timer();
                CheckWinner(Status.EndFinalRound);
                break;

            case Status.EndFinalRound:
                if (!winTextBool)
                {
                    winTextBool = true;
                    StartCoroutine(WinRound());
                }
                break;

            case Status.GoToGameOver:
                SceneManager.LoadScene("GameOver");
                break;
        }
    }

    IEnumerator CountDownStartRound()
    {
        damageBool = false;

        StartCoroutine(FillHP(PlayerHpScript));
        StartCoroutine(FillHP(EnemyHpScript));
        StartCoroutine(FillStamina(PlayerHpScript));
        StartCoroutine(FillStamina(EnemyHpScript));
        StartCoroutine(FillStun(PlayerHpScript));
        StartCoroutine(FillStun(EnemyHpScript));

        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.recharge, 0.5f);

        SoundGameplayController.instance.fxAudioSource.pitch = 1.2f;

        GameObject cloneRound = Instantiate(canvasScript.countDown, GameCanvas);

        TMP_Text cloneRoundText = cloneRound.GetComponentInChildren<TMP_Text>();

        //print("countDown");
        if (actualStatus == Status.StartRound1)
        {
            cloneRoundText.text = "Round 1";
            //print("goR11");
        }
        if (actualStatus == Status.StartRound2)
        {
            //print("goR12");
            cloneRoundText.text = "Round 2";

        }
        if (actualStatus == Status.StartFinalRound)
        {
            //print("gofinalround");
            cloneRoundText.text = "Round Final";
            //SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.recharge);
        }
        yield return new WaitForSeconds(0.9f);

        cloneRoundText.text = "3";
        yield return new WaitForSeconds(1f);

        cloneRoundText.text = "2";
        yield return new WaitForSeconds(1f);

        cloneRoundText.text = "1";
        yield return new WaitForSeconds(1f);

        cloneRoundText.text = "Lutem!";

        SoundGameplayController.instance.fxAudioSource.pitch = 1f;

        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.bellStart, 0.3f);
        yield return new WaitForSeconds(1f);

        Destroy(cloneRound.gameObject);

        damageBool = true;

        if (actualStatus == Status.StartRound1)
        {
            punchIa.actualDifficulty = PunchIA.Difficulty.Easy;
            actualStatus = Status.OnRound1;
            //print("goR11");
        }
        if (actualStatus == Status.StartRound2)
        {
            punchIa.actualDifficulty = PunchIA.Difficulty.Hard;
            //print("goR12");
            actualStatus = Status.OnRound2;
        }
        if (actualStatus == Status.StartFinalRound)
        {
            punchIa.actualDifficulty = PunchIA.Difficulty.Medium;
            //print("gofinalround");
            actualStatus = Status.OnFinalRound;
        }
    }

    IEnumerator BeforeRound()
    {
        playerRoundWonBool = false;
        enemyRoundWonBool = false;
        winTextBool = false;
        //print("corr");

        yield return new WaitForSeconds(0.1f);

        if (actualStatus == Status.BeforeRound1)
        {
            actualStatus = Status.StartRound1;
        }
        else if (actualStatus == Status.BeforeRound2)
        {
            actualStatus = Status.StartRound2;
        }
        else if (actualStatus == Status.BeforeFinalRound)
        {
            if (playerRoundWon >= 2 || enemyRoundWon >= 2)
            {

                actualStatus = Status.GoToGameOver;
            }
            else
            {
                actualStatus = Status.StartFinalRound;
            }
        }
    }

    IEnumerator WinRound()
    {
        EndStunCoroutine();

        SoundGameplayController.instance.fxAudioSource.PlayOneShot(SoundGameplayController.instance.bellEnd, 0.2f);

        InformationGameOverScene.instance.HpLeft += PlayerHpScript.currentHp;
        //hpRestanteEnemy += enemyHpScript.currentHp;
        InformationGameOverScene.instance.TimeLeft = timeInt;

        roundTextBool = false;
        if (PlayerHpScript.currentHp <= 0)
        {
            PlayerHpScript.currentHp = 0.01f;
        }
        if (EnemyHpScript.currentHp <= 0)
        {
            EnemyHpScript.currentHp = 0.01f;
        }

        //playerHpScript.currentStamina = playerHpScript.maxStamina;
        //enemyHpScript.currentStamina = enemyHpScript.maxStamina;
        //playerHpScript.currentStun = 0;
        //enemyHpScript.currentStun = 0;
        playerPunchScript.punchIndex = 0;
        enemyPunchScript.punchIndex = 0;
        playerPunchScript.atacou = false;
        enemyPunchScript.atacou = false;

        yield return new WaitForSeconds(0.5f);

        GameObject cloneWinRound = Instantiate(canvasScript.winRound, GameCanvas);

        TMP_Text cloneWinRoundText = cloneWinRound.GetComponentInChildren<TMP_Text>();
        //print("win");
        timeFloat = 0;

        if (actualStatus == Status.EndRound1)
        {
            damageBool = false;
            if (playerRoundWon == 1)
            {
                WinText(cloneWinRoundText);
            }
            else if (enemyRoundWon == 1)
            {
                LoseText(cloneWinRoundText);
            }
        }

        if (actualStatus == Status.EndRound2)
        {
            damageBool = false;
            if (playerRoundWon == 1 && playerRoundWonBool)
            {
                WinText(cloneWinRoundText);
            }
            else if (enemyRoundWon == 1 && enemyRoundWonBool)
            {
                LoseText(cloneWinRoundText);
            }
            if (playerRoundWon == 2)
            {
                WinText(cloneWinRoundText);

                InformationGameOverScene.instance.PlayerWin = true;
                StartCoroutine(GameOverAnimWin(5f));
            }
            else if (enemyRoundWon == 2)
            {
                LoseText(cloneWinRoundText);
                InformationGameOverScene.instance.PlayerWin = false;

                StartCoroutine(GameOverAnimLose(5f));
            }
        }
        if (actualStatus == Status.EndFinalRound)
        {
            damageBool = false;
            if (playerRoundWon == 2 && playerRoundWonBool)
            {
                WinText(cloneWinRoundText);

                InformationGameOverScene.instance.PlayerWin = true;

                StartCoroutine(GameOverAnimWin(5f));
            }
            else if (enemyRoundWon == 2 && enemyRoundWonBool)
            {
                LoseText(cloneWinRoundText);

                InformationGameOverScene.instance.PlayerWin = false;

                StartCoroutine(GameOverAnimLose(5f));
            }
        }

        yield return new WaitForSeconds(2f);
        Destroy(cloneWinRound.gameObject);
        yield return new WaitForSeconds(0.5f);

        if (actualStatus == Status.EndRound1)
        {
            actualStatus = Status.BeforeRound2;
        }
        else if (actualStatus == Status.EndRound2)
        {
            actualStatus = Status.BeforeFinalRound;
        }
        else if (actualStatus == Status.EndFinalRound)
        {

        }
    }

    public void CheckWinner(Status round)
    {
        if (enemyRoundWonBool)
        {
            enemyRoundWon += 1;
            //enemyRoundWonBool = false;
            actualStatus = round;
            EndStunCoroutine();
            return;
        }

        if (playerRoundWonBool)
        {
            print("foipora");
            playerRoundWon += 1;
            //playerRoundWonBool = false;
            actualStatus = round;
            EndStunCoroutine();
            return;
        }
    }

    public void Timer()
    {
        if (timeInt <= 0)
        {
            //print("tempocabo");
            if (PlayerHpScript.currentHp > EnemyHpScript.currentHp)
            {
                playerRoundWonBool = true;
                //print("jogador");
            }
            else if (EnemyHpScript.currentHp > PlayerHpScript.currentHp)
            {
                enemyRoundWonBool = true;
                //print("inimigo");
            }
            else
            {
                print("draw");
            }
        }
    }
    void WinText(TMP_Text cloneText)
    {
        cloneText.text = "Você venceu!";
        canvasScript.playerRound1Win.enabled = true;
        playerAnim.Animator.SetTrigger("win");
        enemyAnim.Animator.SetTrigger("lose");
    }

    void LoseText(TMP_Text cloneText)
    {
        cloneText.text = "Você perdeu!";
        canvasScript.enemyRound1Win.enabled = true;
        //print("perd1");
        playerAnim.Animator.SetTrigger("lose");
        enemyAnim.Animator.SetTrigger("win");
    }

    IEnumerator FillHP(HPScript hpScript)
    {
        while (hpScript.currentHp < hpScript.maxHP)
        {
            hpScript.currentHp += (hpScript.maxHP / 60);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
    IEnumerator FillStamina(HPScript hpScript)
    {
        while (hpScript.currentStamina < hpScript.maxStamina)
        {
            hpScript.currentStamina += (hpScript.maxStamina / 60);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
    IEnumerator FillStun(HPScript hpScript)
    {
        while (hpScript.currentStun > 0)
        {
            hpScript.currentStun -= (hpScript.maxStun / 60);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator CameraAnimation(float seconds)
    {
        if (!instantiateOnce)
        {
            CameraMovementScript camMove = cameraAnimation.GetComponent<CameraMovementScript>();

            CameraMovement(camMove.transform, camMove.BeforeRoundPosition, camMove);

            yield return new WaitForSeconds(seconds);

            cameraAnimation.GetComponent<Camera>().enabled = false;
            cameraMain.GetComponent<Camera>().enabled = true;

            instantiateOnce = false;
            actualStatus = Status.BeforeRound1;
        }
    }

    IEnumerator GameOverAnimWin(float seconds)
    {
        if (!instantiateOnce)
        {
            enemyPunchScript.stayDown = true;

            CameraMovementScript camMove = cameraAnimation.GetComponent<CameraMovementScript>();
            CameraMovement(camMove.WinRoundStartPosition, camMove.WinRoundPosition, camMove);

            yield return new WaitForSeconds(seconds);

            //cameraAnimation.GetComponent<Camera>().enabled = false;
            //cameraMain.GetComponent<Camera>().enabled = false;

            instantiateOnce = false;
            actualStatus = Status.GoToGameOver;
        }
    }

    IEnumerator GameOverAnimLose(float seconds)
    {
        if (!instantiateOnce)
        {
            playerPunchScript.stayDown = true;

            CameraMovementScript camMove = cameraAnimation.GetComponent<CameraMovementScript>();
            CameraMovement(camMove.LoseRoundStartPosition, camMove.LoseRoundPosition, camMove);

            yield return new WaitForSeconds(seconds);
            //cameraAnimation.GetComponent<Camera>().enabled = false;
            //cameraMain.GetComponent<Camera>().enabled = true;

            instantiateOnce = false;
            actualStatus = Status.GoToGameOver;
        }
    }

    void CameraMovement(Transform cameraStartLocation, Transform cameraEndPosition, CameraMovementScript cameraMov)
    {
        instantiateOnce = true;

        cameraAnimation.transform.position = cameraStartLocation.position;

        cameraAnimation.GetComponent<Camera>().enabled = true;
        cameraMain.GetComponent<Camera>().enabled = false;
        
        cameraMov.MoveCamera(cameraEndPosition.position, 1.5f);
    }

    void EndStunCoroutine()
    {
        IEnumerator co = EnemyHpScript.SlowTime();
        IEnumerator co2 = PlayerHpScript.SlowTime();
        Time.timeScale = 1f;
        SoundGameplayController.instance.audioSource.pitch = 1;
        enemyPunchScript.stunned = false;
        playerPunchScript.stunned = false;
        StopCoroutine(co);
        StopCoroutine(co2);
    }

    void PauseUnPauseMusic()
    {
        if (pause)
        {
            pausetimer = 0;
            SoundGameplayController.instance.audioSource.Pause();
            SoundGameplayController.instance.audioSource1.Pause();
            SoundGameplayController.instance.fxAudioSource.Pause();
            SoundGameplayController.instance.fxAudioSource2.Pause();
        }
        else
        {
            pausetimer += Time.deltaTime;
            SoundGameplayController.instance.audioSource.UnPause();
            SoundGameplayController.instance.audioSource1.UnPause();
            SoundGameplayController.instance.fxAudioSource.UnPause();
            SoundGameplayController.instance.fxAudioSource2.UnPause();
        }
    }

    void CheckMaxCombos()
    {
        if (comboPlayer >= InformationGameOverScene.instance.ComboMax)
        {
            InformationGameOverScene.instance.ComboMax = comboPlayer;
        }

        if (comboEnemy >= maxComboEnemy)
        {
            maxComboPlayer = comboPlayer;
        }
    }

    public HPScript PlayerHpScript { get { return playerHpScript; } set { playerHpScript = value; } }
    public HPScript EnemyHpScript { get { return enemyHpScript; } set { enemyHpScript = value; } }

    public bool DamageBool { get { return damageBool; } }
}
