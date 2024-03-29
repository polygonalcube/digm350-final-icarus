using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public HPComponent hp;
    public MoveComponent mover;
    public MoveComponent moverDeath;

    public float movVal;
    public float movValDeath = 1f;

    bool firstJump = true;

    Vector3 globalStart;
    Vector3 localStart;

    public bool hasWon = false;
    public float jumpDiv = 0f;

    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] sprites;

    GameObject arrow;

    void Start()
    {
        globalStart = transform.position;
        localStart = transform.localPosition;

        jumpDiv = hp.maxHealth / 2f;

        arrow = GameObject.Find("Arrow");
    }
    
    void Update()
    {
        if (GameManager.gm.gameState == GameManager.GameState.Title)
        {
            transform.position = globalStart;
            hp.health = hp.maxHealth;

            arrow.transform.localScale = new Vector3((Mathf.Sin(Time.time * 6f + 0f) * 0.5f + 0f), arrow.transform.localScale.y, arrow.transform.localScale.z);
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Await)
        {
            transform.localPosition = localStart;
            mover.speed = Vector2.zero;
            firstJump = true;

            arrow.transform.localScale = new Vector3((Mathf.Sin(Time.time * 6f + 0f) * 0.5f + 0f), arrow.transform.localScale.y, arrow.transform.localScale.z);
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            movValDeath = 1f;
            Movement();
            Animation();

            arrow.SetActive(false);
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Death)
        {
            Die();
            sr.sprite = sprites[0];
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Win)
        {
            hp.health = hp.maxHealth;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            SunDamage();
        }
    }

    void Movement()
    {
        if (firstJump)
        {
            movVal = 1f;
            firstJump = false;
        }
        else movVal = (Input.GetButtonDown("Jump")) ? 1f : 0f;
        mover.jumpHgt = hp.health / jumpDiv;
        Vector2 movResult = mover.Move(Vector2.up * movVal);
        transform.position += (Vector3)movResult;
    }

    void Animation()
    {
        sr.sprite = (mover.speed.y > 0f) ? sprites[1] : sprites[0];
    }

    void SunDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up);

        if (hit.collider == null)
        {
            hp.health -= 1;
        }
        else if (hit.collider.gameObject.tag == "Kill")
        {
            hp.health -= 1;
        }
    }

    void Die()
    {
        Vector2 movResult = moverDeath.Move(Vector2.up * movValDeath);
        transform.position += (Vector3)movResult;
        movValDeath = 0f;
    }

    //Needs a trigger collider to be present on the game object.
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Win") && (GameManager.gm.gameState == GameManager.GameState.Game) && (hp.health > 0))
        {
            GameManager.gm.gameState = GameManager.GameState.Win;
            hasWon = true;
        }
    }
}
