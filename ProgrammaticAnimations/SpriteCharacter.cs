using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCharacter : MonoBehaviour
{
    [System.Serializable]
    public struct minMaxFloat { public float min; public float max; }

    public Color customColor = Color.white;

    public GameObject spriteObj;
    public GameObject snoreSpewer;
    public GameObject angerMark;

    public float defaultScale;
    private float curScale;

    public float walkSpeed;
    public minMaxFloat wanderDirectionChangeRate;
    public minMaxFloat sleepBreakRate;
    public minMaxFloat sleepBreakLength;
    public bool sleepRandomDirection;
    public float waveHelloLength;
    public float spriteSwitchRate;

    public Sprite[] walkLeftSprites;
    public Sprite[] runLeftSprites;
    public Sprite[] sleepSprites;
    public Sprite[] waveHelloSprites;
    public Sprite[] angrySprites;
    public Sprite[] leftBattleSprites;
    public Sprite[] rightBattleSprites;
    public Sprite[] leftAttackSprites;
    public Sprite[] rightAttackSprites;

    public Sprite[] currentSpriteArray;

    public bool wander;
    public bool isLeft;
    public bool running;
    public bool walkLeft;
    public bool walkRight;
    public bool sleeping;
    public bool waveHello;
    public bool beAngry;
    public bool battling;

    private float lastFrameChange;
    private float nextDirectionChange;
    private float nextSleepBreak;
    private float wakeupTime;
    private float stopWavingTime;

    private int currentFrame;

    // Start is called before the first frame update
    void Start()
    {
        lastFrameChange = Time.fixedTime;
        nextDirectionChange = Time.fixedTime + Random.Range(wanderDirectionChangeRate.min, wanderDirectionChangeRate.max);
        nextSleepBreak = Time.fixedTime + Random.Range(sleepBreakRate.min, sleepBreakRate.max);
        currentSpriteArray = waveHelloSprites;
        waveHello = true;
        stopWavingTime = Time.fixedTime + 3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        // Update current Sprite
        if (Time.fixedTime - lastFrameChange > spriteSwitchRate / (running.GetHashCode() + 1))
        {
            currentFrame++;
            if (currentFrame >= currentSpriteArray.Length)
            {
                currentFrame = 0;
            }
            UpdateSprite(currentSpriteArray[currentFrame]);
        }

        // Check if it's time to start sleeping
        if(nextSleepBreak < Time.fixedTime && !sleeping && !beAngry && !battling)
        {
            StartSleeping();
        }

        // Check if it's time to stop waving
        else if (waveHello)
        {
            if (Time.fixedTime > stopWavingTime)
            {
                ChangeDirection();
            }
        }

        // Check if it's time to wake up
        else if (sleeping)
        {
            if (wakeupTime < Time.fixedTime)
            {
                StopSleeping();
            }
        }

        // Check if we need to change directions
        else if (wander)
        {
            // Move the character
            float runSpeed = running ? 2 : 1;
            transform.Translate(new Vector3(
                walkSpeed * (-walkLeft.GetHashCode() + walkRight.GetHashCode()) * runSpeed * Time.deltaTime, 0, 0));

            if (screenPos.x < 25) // If character is close to left side of screen
            {
                if (walkLeft)
                {
                    if (running)
                    {
                        StartRunRight();
                    }
                    else
                    {
                        StartWalkRight();
                    }
                }
            }
            else if (screenPos.x >= Screen.width - 25) // If character is close to right side of screen
            {
                if (walkRight)
                {
                    if (running)
                    {
                        StartRunLeft();
                    }
                    else
                    {
                        StartWalkLeft();
                    }
                }
            }
            else if (Time.fixedTime > nextDirectionChange)  // If it's time to change directions randomly
            {
                ChangeDirection();
            }
        }
    }

    public void StartWandering()
    {
        ResetStates();
        nextSleepBreak = Time.fixedTime + Random.Range(sleepBreakRate.min, sleepBreakRate.max);
        ChangeDirection();
    }

    public void Attack()
    {
        if(isLeft)
        {
            LeftAttack();
        }
        else
        {
            RightAttack();
        }
    }

    public void LeftAttack()
    {
        ResetStates();
        spriteObj.GetComponent<SpriteRenderer>().flipX = true;
        battling = true;
        isLeft = true;
        currentSpriteArray = leftAttackSprites;
        UpdateSprite(leftAttackSprites[0]);
        StartCoroutine(StopAttacking());
    }

    public void LeftBattling()
    {
        ResetStates();
        spriteObj.GetComponent<SpriteRenderer>().flipX = true;
        battling = true;
        isLeft = true;
        currentSpriteArray = leftBattleSprites;
        UpdateSprite(leftBattleSprites[0]);
    }

    public void RightAttack()
    {
        ResetStates();
        battling = true;
        currentSpriteArray = rightAttackSprites;
        UpdateSprite(rightAttackSprites[0]);
        StartCoroutine(StopAttacking());
    }

    public void RightBattling()
    {
        ResetStates();
        battling = true;
        currentSpriteArray = rightBattleSprites;
        UpdateSprite(rightBattleSprites[0]);
    }

    IEnumerator StopAttacking()
    {
        yield return new WaitForSeconds(.5f);
        StopAttacking(isLeft);
    }

    private void StopAttacking(bool left)
    {
        if(left)
        {
            currentSpriteArray = leftBattleSprites;
            UpdateSprite(leftBattleSprites[0]);
        }
        else
        {
            currentSpriteArray = rightBattleSprites;
            UpdateSprite(rightBattleSprites[0]);
        }
    }

    private void GetInked()
    {
        if(!battling)
        {
            StopSleeping();
            StartAngry();
            spriteObj.GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(ReduceInk());
        }
    }

    IEnumerator ReduceInk()
    {
        yield return new WaitForSeconds(12f);
        while (spriteObj.GetComponent<SpriteRenderer>().color != customColor)
        {
            yield return new WaitForSeconds(0.1f);
            Color temp = new Color(
                spriteObj.GetComponent<SpriteRenderer>().color.r + 0.02f,
                spriteObj.GetComponent<SpriteRenderer>().color.g + 0.02f,
                spriteObj.GetComponent<SpriteRenderer>().color.b + 0.02f);
            if (temp.r > customColor.r)
            {
                temp.r = customColor.r;
            }
            if (temp.g > customColor.g)
            {
                temp.g = customColor.g;
            }
            if (temp.b > customColor.b)
            {
                temp.b = customColor.b;
            }
            spriteObj.GetComponent<SpriteRenderer>().color = temp;
        }
        StopAngry();
    }

    private void StartSleeping()
    {
        ResetStates();
        sleeping = true;
        wakeupTime = Time.fixedTime + Random.Range(sleepBreakLength.min, sleepBreakLength.max);
        UpdateSprite(sleepSprites[0]);
        if(sleepRandomDirection)
        {
            if(Random.Range(0,2) > 0)
            {
                spriteObj.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        currentSpriteArray = sleepSprites;
        if(snoreSpewer)
        {
            snoreSpewer.GetComponent<Spew>().alwaysOn = true;
        }
    }

    private void StopSleeping()
    {
        ResetStates();
        wander = true;
        sleeping = false;
        ChangeDirection();
        nextSleepBreak = Time.fixedTime + Random.Range(sleepBreakRate.min, sleepBreakRate.max);
    }

    private void StartWavingHello()
    {
        ResetStates();
        waveHello = true;
        stopWavingTime = Time.fixedTime + waveHelloLength;
        UpdateSprite(waveHelloSprites[0]);
        currentSpriteArray = waveHelloSprites;
    }

    private void StartAngry()
    {
        ResetStates();
        if (angerMark)
        {
            angerMark.SetActive(true);
        }
        beAngry = true;
        UpdateSprite(angrySprites[0]);
        currentSpriteArray = angrySprites;
    }

    private void StopAngry()
    {
        ResetStates();
        wander = true;
        ChangeDirection();
    }

    private void StartWalkLeft()
    {
        ResetStates();
        wander = true;
        walkLeft = true;
        currentFrame = 0;
        UpdateSprite(walkLeftSprites[currentFrame]);
        currentSpriteArray = walkLeftSprites;
    }

    private void StartWalkRight()
    {
        ResetStates();
        spriteObj.GetComponent<SpriteRenderer>().flipX = true;
        wander = true;
        walkRight = true;
        currentFrame = 0;
        UpdateSprite(walkLeftSprites[currentFrame]);
        currentSpriteArray = walkLeftSprites;
    }

    private void StartRunLeft()
    {
        ResetStates();
        wander = true;
        running = true;
        walkLeft = true;
        currentFrame = 0;
        UpdateSprite(runLeftSprites[currentFrame]);
        currentSpriteArray = runLeftSprites;
    }

    private void StartRunRight()
    {
        ResetStates();
        spriteObj.GetComponent<SpriteRenderer>().flipX = true;
        wander = true;
        running = true;
        walkRight = true;
        currentFrame = 0;
        UpdateSprite(runLeftSprites[currentFrame]);
        currentSpriteArray = runLeftSprites;
    }

    private void ChangeDirection()
    {
        nextDirectionChange = Time.fixedTime + Random.Range(wanderDirectionChangeRate.min, wanderDirectionChangeRate.max);

        int newDirection = Random.Range(0, 4);

        switch (newDirection)
        {
            case 0: // Walk Left
                if (!walkLeft || running)
                {
                    StartWalkLeft();
                }
                break;
            case 1: // Walk Right
                if (!walkRight || running)
                {
                    StartWalkRight();
                }
                break;
            case 2: // Run Left
                if (!walkLeft || !running)
                {
                    StartRunLeft();
                }
                break;
            case 3: // Run Right
                if (!walkRight || !running)
                {
                    StartRunRight();
                }
                break;
            default:
                break;
        }
    }

    private void ResetStates()
    {
        spriteObj.GetComponent<SpriteRenderer>().flipX = false;
        if (snoreSpewer)
        {
            snoreSpewer.GetComponent<Spew>().alwaysOn = false;
        }
        if (angerMark)
        {
            angerMark.SetActive(false);
        }
        wander = false;
        running = false;
        walkLeft = false;
        walkRight = false;
        sleeping = false;
        beAngry = false;
        waveHello = false;
        isLeft = false;
        battling = false;
    }

    private void UpdateSprite(Sprite nextFrame)
    {
        spriteObj.GetComponent<SpriteRenderer>().sprite = nextFrame;
        lastFrameChange = Time.fixedTime;
    }

    public void SetCustomColor(Color newColor)
    {
        customColor = newColor;
        spriteObj.GetComponent<SpriteRenderer>().color = customColor;
    }
}
