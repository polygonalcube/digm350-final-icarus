using DG.Tweening;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    GameObject menu;

    void Start()
    {
        menu = GameObject.Find("Menu");
        StartCoroutine(Menu());
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && GameManager.gm.gameState != GameManager.GameState.Game)
        {
            if (GameManager.gm.gameState != GameManager.GameState.Death) GameManager.gm.gameState++;
            else GameManager.gm.gameState = GameManager.GameState.Title;
        }
    }

    IEnumerator Menu()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Await);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 1500f), 1f, false);
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Game);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 3000f), 1f, false);
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Death);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 4500f), 0.5f, false);
            yield return new WaitUntil(() => GameManager.gm.gameState == GameManager.GameState.Title);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 5250f), 0.5f, false);
            yield return new WaitUntil(() => menu.GetComponent<RectTransform>().anchoredPosition.y > 5249f);
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, -1000f), 0.5f, false).From();
            menu.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0f, 0f), 1f, false);
        }
    }
}
