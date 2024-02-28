using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtComponent : MonoBehaviour
{
    public HPComponent hp;
    public GameObject sprRen;
    public LayerMask layers;
    public string[] tags;

    public float iFramesLeft = 0f;

    void FixedUpdate()
    {
        if (iFramesLeft > 0)
        {
            iFramesLeft -= 1;
            if (iFramesLeft > 0)
            {
                if (sprRen != null)
                {
                    sprRen.SetActive(!sprRen.activeSelf);
                }
            }
            else if (sprRen != null)
            {
                sprRen.SetActive(true);
            }
        }
        else
        {
            if (sprRen != null)
            {
                if (!sprRen.activeSelf)
                {
                    StartCoroutine(BecomeVisible());
                }
            }
        }
    }

    IEnumerator BecomeVisible()
    {
        yield return new WaitForSeconds(1f/30f);
        sprRen.SetActive(true);
    }

    bool CheckTags(Collider2D col)
    {
        foreach (string tag in tags)
        {
            if (col.gameObject.tag == tag)
            {
                return true;
            }
        }
        return false;
    }
    
    //Needs a trigger collider to be present on the game object.
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((hp != null) && CheckTags(col))
        {
            if (col.gameObject.TryGetComponent<HitComponent>(out HitComponent hitbox))
            {
                hp.health -= hitbox.power;
                iFramesLeft = hitbox.iFrames;
            }

            //if (sprRen != null) sprRen.SetActive(false);
        }
    }
}
