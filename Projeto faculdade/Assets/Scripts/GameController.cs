using TMPro;

using UnityEngine;


public class GameController : MonoBehaviour
{

    public Lumberjack lumberjack;
    public Bee bee;

    public ForestHeart ForestHeartClass;

    public Personagem personagem;

    public int TotalScore;
    [SerializeField] private static int PointstoWin = 3;

    public int TotalTries = 1;
    public TMP_Text ScoreText;
    public Canvas Endgame;
    public TMP_Text TriesText;

    public TMP_Text EndTriesText;

    public GameObject ForestHeartCathc;

    public GameObject ForestHeartNotCathc;

    public TMP_Text ForestHeartText;

    public bool skipAddTries = false;
    public void Start()
    {
        if (!bee.gameObject.activeSelf)
        {
            Destroy(bee.gameObject);
        }

        if (!lumberjack.gameObject.activeSelf)
        {
            Destroy(lumberjack.gameObject);
        }


    }

    public void UpdateScoreText()
    {
        ScoreText.text = TotalScore.ToString() + "/3";
    }

    public void CheckEndGame()
    {
        if (TotalScore >= PointstoWin)
        {
            Endgame.gameObject.SetActive(true);
            personagem.canMove = false;
            Time.timeScale = 0f;
            EndTriesText.text = "Em " + TotalTries.ToString() + "X tentativas";
            if (ForestHeartClass.Gotcha == true)
            {
                ForestHeartText.gameObject.SetActive(true);
            }

        }
    }

    public void AddPoints(int points)
    {
        TotalScore += points;
        UpdateScoreText();
        CheckEndGame();
    }

    public void CatchForestHeart()
    {
        ForestHeartCathc.gameObject.SetActive(true);
        ForestHeartNotCathc.gameObject.SetActive(false);
    }

    public void UpdateTriesText()
    {
        TriesText.text = TotalTries.ToString() + "X";
    }

    public void AddTries()
    {
        TotalTries++;
        UpdateTriesText();
    }

    public void DeathReset()
    {
        TotalScore = 0;
        UpdateScoreText();
        ForestHeartCathc.gameObject.SetActive(false);
        ForestHeartNotCathc.gameObject.SetActive(true);
        personagem.transform.position = personagem.InitialPosition;
        personagem.animator.SetBool("Deathing", false);
        personagem.animator.Play("Idle");
        personagem.spriteRenderer.flipX = false;
        personagem.facingright = true;
        ForestHeartClass.transform.position = ForestHeartClass.InitialPosition;
        if (bee != null)
        {
            bee.transform.position = bee.initialposition;
            bee.Lastposition = bee.initialposition;
        }
        if (lumberjack != null){
             lumberjack.gameObject.SetActive(true);
            lumberjack.playerDead = false;
            lumberjack.agent.isStopped=false;
            lumberjack.animator.SetBool("Dying",false);
            lumberjack.animator.Play("Walk");
             Collider2D[] colliders = lumberjack.GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = true;
        }
           
        }
        ForestHeartClass.Gotcha = false;
        ForestHeartText.gameObject.SetActive(false);
        Transform gemsPai = GameObject.Find("Gem").transform; // Nome do objeto pai
        foreach (Transform child in gemsPai)
        {
            child.gameObject.SetActive(true);
            Gens gens = child.GetComponent<Gens>();

            if (gens != null)
            {
                child.position = gens.initialposition;
                gens.go = false;
            }
        }

        personagem.animator.SetBool("Deathing", false);
        personagem.canJump = true;
        personagem.canMove = true;
        personagem.canGlideAgain = true;
        personagem.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        Debug.Log(TotalTries);
        if (!skipAddTries)
        {
            AddTries();
        }
        skipAddTries = false;

    }

}
