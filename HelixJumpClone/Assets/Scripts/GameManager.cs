using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public AudioSource musicController;
    public AudioClip padDestroySound;


    public GameObject player;
    public Transform rod;
    public ParticleSystem winParticle;
    public ParticleSystem gameOverParticle;

    [HideInInspector]
    public Vector3 playerStartingPoint = new(0.72f, 0.49f, -0.79f);

    private LevelGenerator levelGenerator;
    public GameObject currentLevel;

    public Image gameOverScreen;
    public Image startScreen;
    public Image nextLevelScreen;
    public TextMeshProUGUI scoresText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI totalScoresText;

    public bool isGameActive;
    public bool isGameOver;
    public bool isNextLevel;

    [HideInInspector]
    public int scores;
    private int totalScores;
    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        levelGenerator = GameObject.Find("Level").GetComponent<LevelGenerator>();
        musicController = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameOver();
        CheckForNextLevel();
        ScoreCounter();
        LevelCounter();
        TotalScoreCounter();
    }
    void CheckForGameOver()
    {
        if (isGameOver)
        {
            gameOverScreen.gameObject.SetActive(true);
        }
    }
    void CheckForNextLevel()
    {
        if (isNextLevel)
        {
            nextLevelScreen.gameObject.SetActive(true);
            winParticle.Play();
        }
    }
    public void GameStart()
    {
        isGameActive = true;
        isGameOver = false;
        isNextLevel = false;
        startScreen.gameObject.SetActive(false);
        scoresText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        totalScoresText.gameObject.SetActive(true);
        levelGenerator.RodBuilder();
        currentLevel = GameObject.Find("Level");
        musicController.Play();
    }
    public void GameRestart()
    {
        
        player.transform.position = playerStartingPoint;
        isGameActive = true;
        scores = 0;
        rod.transform.rotation = new Quaternion(0, 0, 0, 0);

        levelGenerator.DestroyPads();
        levelGenerator.RodBuilder();
        gameOverScreen.gameObject.SetActive(false);
        isGameOver = false;
    }
    public void NextLevel()
    {
        isGameActive = true;
        isGameOver = false;
        level += 1;
        totalScores += scores;
        scores = 0;
        
        nextLevelScreen.gameObject.SetActive(false);
        levelGenerator.DestroyPads();
        levelGenerator.RodBuilder();
        player.transform.position = playerStartingPoint;

        isNextLevel = false;
        winParticle.Stop();
    }
    void ScoreCounter()
    {
        scoresText.text = $"Scores\n{scores}";
    }
    void LevelCounter()
    {
        levelText.text = $"Level\n{level}";
    }
    void TotalScoreCounter()
    {
        totalScoresText.text = $"Total Scores\n{totalScores}";
    }
}
