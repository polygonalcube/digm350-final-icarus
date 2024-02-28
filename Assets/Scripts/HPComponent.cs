//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class HPComponent : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public bool immediateDeath = true;

    void Update()
    {
        if (health <= 0 && immediateDeath) Destroy(this.gameObject);
    }
}
