using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameObject menu;
    GameObject progress;
    Slider candle;
    Image flash;

    float attemptTime;

    [SerializeField] string[] messages;

    void Start()
    {
        candle = GameObject.Find("Candle").GetComponent<Slider>();
        candle.maxValue = GameManager.gm.FindPlayerScript().hp.maxHealth;

        flash = GameObject.Find("Flash").GetComponent<Image>();
        flash.color = new Color(1f, 1f, 1f, 0f);

        menu = GameObject.Find("Menu");
        StartCoroutine(Menu());

        progress = GameObject.Find("Progress");
    }

    void Update()
    {
        candle.value = GameManager.gm.FindPlayerScript().hp.health;
        flash.color -= new Color(0f, 0f, 0f, Time.deltaTime * 5f);
        if (GameManager.gm.gameState == GameManager.GameState.Game) attemptTime += Time.deltaTime;
        
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
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 5250f), 0.5f, false);
            yield return new WaitUntil(() => menu.GetComponent<RectTransform>().anchoredPosition.y > 5249f);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, -1000f), 0.5f, false).From();
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 0f), 1f, false);
        }
    }

    IEnumerator Progress()
    {
        int i = 0;
        while (true)
        {
            yield return new WaitUntil(() => attemptTime > ((GameObject.Find("Main Camera").GetComponent<CameraLogic>().secondsToWin / 5f) * ((float)i + 1f)));
            progress.GetComponent<TextMeshProUGUI>().text = messages[i];
            progress.GetComponent<RectTransform>().DOAnchorPos(new Vector3(2000f, -270f), 6f, false).From();
            progress.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-2000f, -270f), 4f, false).SetEase(Ease.Linear);
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
