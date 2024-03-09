using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameObject menu;
    GameObject progress;
    Slider progressBar;
    Slider candle;
    Image flash;

    public float attemptTime;

    [SerializeField] string[] messages;

    GameObject bar;

    public float messageDuration;

    void Start()
    {
        candle = GameObject.Find("Candle").GetComponent<Slider>();
        candle.maxValue = GameManager.gm.FindPlayerScript().hp.maxHealth;
        GameObject.Find("Candle").GetComponent<RectTransform>().anchoredPosition = new Vector2(60f, 250f);

        flash = GameObject.Find("Flash").GetComponent<Image>();
        flash.color = new Color(1f, 1f, 1f, 0f);

        menu = GameObject.Find("Menu");
        StartCoroutine(Menu());

        progress = GameObject.Find("Progress");
        progress.SetActive(true);

        progressBar = GameObject.Find("Progress Bar").GetComponent<Slider>();
        bar = GameObject.Find("Progress Bar");
        bar.SetActive(true);
    }

    void Update()
    {
        candle.value = GameManager.gm.FindPlayerScript().hp.health;
        flash.color -= new Color(0f, 0f, 0f, Time.deltaTime * 5f);
        if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            attemptTime += Time.deltaTime;
            progressBar.maxValue = GameObject.Find("Main Camera").GetComponent<CameraLogic>().secondsToWin;
            progressBar.value = attemptTime;
        } 
        
        if (Input.GetButtonDown("Jump"))
        {
            if ((GameManager.gm.gameState == GameManager.GameState.Title) && (menu.GetComponent<RectTransform>().anchoredPosition.y == 0f))
                GameManager.gm.gameState++;
            else if (GameManager.gm.gameState == GameManager.GameState.Await)
                GameManager.gm.gameState++;
            else if ((GameManager.gm.gameState == GameManager.GameState.Death) && (GameManager.gm.FindPlayer().transform.position.y <= -5.5f))
                GameManager.gm.gameState = GameManager.GameState.Title;
        }
        else if ((GameManager.gm.gameState == GameManager.GameState.Game) && (GameManager.gm.FindPlayerScript().hp.health <= 0))
            GameManager.gm.gameState = GameManager.GameState.Death;
        else if (GameManager.gm.FindPlayerScript().hasWon)
        {
            StopCoroutine(Menu());
            StopCoroutine(Progress());
            StartCoroutine(Win());
            GameManager.gm.FindPlayerScript().hasWon = false;
        }
            
        if (Input.GetKey("escape")) Application.Quit();

        // A/B testing structure.
        /*
        if (GameManager.gm.isBuildA)
        {
            GameObject.Find("Candle").GetComponent<RectTransform>().anchoredPosition = new Vector2(60f, 250f);
            progress.SetActive(true);
            bar.SetActive(false);
        }
        else 
        {
            GameObject.Find("Candle").GetComponent<RectTransform>().anchoredPosition = new Vector2(1860f, 825f);
            progress.SetActive(false);
            bar.SetActive(true);
        }
        */
    }

    IEnumerator Menu()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Await);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 1500f), 1f, false);
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Game);
            StartCoroutine(Progress());
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 3000f), 1f, false);
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Death);
            StopCoroutine(Progress());
            attemptTime = 0f;
            flash.color = new Color(1f, 1f, 1f, 1f);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 4500f), 0.5f, false);
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Title);
            GameManager.gm.RandomizeClouds();
            progressBar.value = attemptTime;
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 5250f), 0.5f, false);
            yield return new WaitUntil(() => menu.GetComponent<RectTransform>().anchoredPosition.y > 5249f);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, -1000f), 0.5f, false).From();
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 0f), 1f, false);
        }
    }

    IEnumerator Progress()
    {
        int i = 0;
        while (i < messages.Length)
        {
            yield return new WaitUntil(() => attemptTime > ((GameObject.Find("Main Camera").GetComponent<CameraLogic>().secondsToWin / (float)(messages.Length + 1)) 
            * ((float)i + 1f)));
            progress.GetComponent<TextMeshProUGUI>().text = messages[i];
            progress.GetComponent<RectTransform>().DOAnchorPos(new Vector3(2000f, -270f), 1f, false).From();
            progress.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-2000f, -270f), messageDuration, false).SetEase(Ease.Linear);
            i++;
        }
    }

    IEnumerator Win()
    {
        menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 5625f), 0.5f, false).From();
        menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 6000f), 0.5f, false);
        yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Title);
    }
}
