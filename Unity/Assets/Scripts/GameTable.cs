using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    // public transform cup_one;
    // public transform cup_two;
    // public transform cup_three;
    // public transform ball;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    CameraZoom camera;
    [SerializeField] GameObject cam;
    Cups cup_one;
    [SerializeField] GameObject c1;
    Cups cup_two;
    [SerializeField] GameObject c2;
    Cups cup_three;
    [SerializeField] GameObject c3;
    Ball ball;
    [SerializeField] GameObject ballObject;
    enum cupPosition{cp1, cp2, cp3}
    cupPosition positionOne = cupPosition.cp1;
    cupPosition positionTwo = cupPosition.cp2;
    cupPosition positionThree = cupPosition.cp3;
    bool clickable = false;

    // Start is called before the first frame update
    void Start()
    {
        cup_one = c1.GetComponent<Cups>();
        cup_two = c2.GetComponent<Cups>();
        cup_three = c3.GetComponent<Cups>();
        ball = ballObject.GetComponent<Ball>();
        camera = cam.GetComponent<CameraZoom>();
        int ran = Random.Range(1, 4);
        StartCoroutine(level01(ran));
    }

    // Update is called once per frame
    void Update()
    {
        if(clickable == true) {
            StartCoroutine(userInput());
        }
    }

    // takes 
    IEnumerator userInput()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("userInput1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("userInput2");
                switch(hit.transform.name)
                {
                    case "CupOne":
                        Debug.Log("userInput CupOne");
                        cup_one.Up();
                        StartCoroutine(endGame(Ball.cups.one));
                    break;
                    case "CupTwo":
                        Debug.Log("userInput CupTwo");
                        cup_two.Up();
                        StartCoroutine(endGame(Ball.cups.two));
                    break;
                    case "CupThree":
                        Debug.Log("userInput CupThree");
                        cup_three.Up();
                        StartCoroutine(endGame(Ball.cups.three));
                    break;
                    default:
                        Debug.Log("userInput Default");
                    break;
                }
            }  
        }
        
        if (Input.GetKey("1")) {
            Debug.Log("userInput keypress 1");
            cup_one.Up();
            StartCoroutine(endGame(Ball.cups.one));
        }else if(Input.GetKey("2")) {
            Debug.Log("userInput keypress 2");
            cup_two.Up();
            StartCoroutine(endGame(Ball.cups.two));
        }else if(Input.GetKey("3")){
            Debug.Log("userInput keypress 3");
            cup_three.Up();
           StartCoroutine(endGame(Ball.cups.three));
        }
        yield return new WaitForSeconds(0.3f);
    }

    // takes 5f seconds
    IEnumerator level01(int which)
    {
        Debug.Log("Level 01");
        StartCoroutine(startGame(which));
        yield return new WaitForSeconds(2.5f);
        for(int i = 0; i < 10; i++) {
            cupPosition n1;
            cupPosition n2;
            int num1 = Random.Range(1, 4);
            int num2 = Random.Range(1, 4);
            int[] nums = trueRandom(num1, num2);
            n1 = numToCup(nums[0]);
            n2 = numToCup(nums[1]);
            StartCoroutine(cupSwap(n1, n2));
            yield return new WaitForSeconds(0.4f);
        }
        camera.CameraPosTwo();
        clickable = true;
        StartCoroutine(waitForInput());
        yield return new WaitForSeconds(1f);
    }

    int[] trueRandom(int num1, int num2)
    {
        if(num1 == num2) {
            switch(num1)
            {
                case 1:
                    num2 = Random.Range(2, 4);
                break;
                case 2:
                    num2 = Random.Range(1, 3);
                    if(num2 == 1) {
                        num2 = 1;
                    }else{
                        num2 = 3;
                    }
                break;
                default:
                    num2 = Random.Range(1, 3);
                break;
            }
            int[] nums = {num1, num2};
            return nums;
        }else{
            int[] nums = {num1, num2};
            return nums;
        }
    }

    cupPosition numToCup(int num)
    {
        switch(num)
            {
                case 1:
                    return cupPosition.cp1;
                break;
                case 2:
                    return cupPosition.cp2;
                break;
                default:
                    return cupPosition.cp3;
                break;
            }
    }

    // takes 2f seconds
    IEnumerator startGame(int which)
    {
        Debug.Log("startGame");
        yield return new WaitForSeconds(0.5f);
        cup_one.Up();
        cup_two.Up();
        cup_three.Up();
        yield return new WaitForSeconds(0.5f);
        cup_one.Down();
        cup_two.Down();
        cup_three.Down();
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ballCover(which));
        yield return new WaitForSeconds(0.5f);
    }

    // takes 1f seconds
    IEnumerator waitForInput()
    {
        Debug.Log("waitForInput");
        Vector3 vector;
        Cups cup;

        switch(ball.getAttchedTo())
        {
            case Ball.cups.one:
                vector = new Vector3(c1.transform.position.x, ballObject.transform.position.y, c1.transform.position.z);
                cup = cup_one;
            break;
            case Ball.cups.two:
                vector = new Vector3(c2.transform.position.x, ballObject.transform.position.y, c2.transform.position.z);
                cup = cup_two;
            break;
            default:
                vector = new Vector3(c3.transform.position.x, ballObject.transform.position.y, c3.transform.position.z);
                cup = cup_three;
            break;
        }
        ball.setTarget(vector);
        yield return new WaitForSeconds(1f);
        ballObject.SetActive(true);
    }

    // takes 1f seconds
    IEnumerator endGame(Ball.cups cup)
    {
        if(cup == ball.getAttchedTo())
        {
            Debug.Log("You Won!");
            clickable = false;
            winPanel.SetActive(true);
        }else{
            Debug.Log("You Lose!");
            clickable = false;
            cup_one.Up();
            cup_two.Up();
            cup_three.Up();
            losePanel.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
    }

    // takes .4f seconds
    IEnumerator ballCover(int which)
    {
        Debug.Log("ballCover");
        Vector3 vector;
        switch(which)
        {
            case 1:
                vector = new Vector3(ballObject.transform.position.x, c1.transform.position.y, ballObject.transform.position.z);
                cup_one.Up();
                yield return new WaitForSeconds(0.1f);
                cup_one.setTarget(vector);
                yield return new WaitForSeconds(0.1f);
                cup_one.Down();
                yield return new WaitForSeconds(0.1f);
                ballObject.SetActive(false);
                ball.setAttachedTo(Ball.cups.one);
                yield return new WaitForSeconds(0.1f);
                vector = new Vector3(0.5f, c1.transform.position.y, 0.5f);
                cup_one.setTarget(vector);
                cup_one.setAttached(true);
                yield return new WaitForSeconds(0.1f);
            break;
            case 2:
                vector = new Vector3(ballObject.transform.position.x, c2.transform.position.y, ballObject.transform.position.z);
                cup_two.Up();
                yield return new WaitForSeconds(0.1f);
                cup_two.setTarget(vector);
                yield return new WaitForSeconds(0.1f);
                cup_two.Down();
                yield return new WaitForSeconds(0.1f);
                ballObject.SetActive(false);
                ball.setAttachedTo(Ball.cups.two);
                yield return new WaitForSeconds(0.1f);
                vector = new Vector3(0f, c2.transform.position.y, 0.5f);
                cup_two.setTarget(vector);
                cup_two.setAttached(true);
                yield return new WaitForSeconds(0.1f);
            break;
            default:
                vector = new Vector3(ballObject.transform.position.x, c3.transform.position.y, ballObject.transform.position.z);
                cup_three.Up();
                yield return new WaitForSeconds(0.1f);
                cup_three.setTarget(vector);
                yield return new WaitForSeconds(0.1f);
                cup_three.Down();
                yield return new WaitForSeconds(0.1f);
                ballObject.SetActive(false);
                ball.setAttachedTo(Ball.cups.three);
                yield return new WaitForSeconds(0.1f);
                vector = new Vector3(-0.5f, c3.transform.position.y, 0.5f);
                cup_three.setTarget(vector);
                cup_three.setAttached(true);
                yield return new WaitForSeconds(0.1f);
            break;
        }
    }

    // takes .5f seconds
    IEnumerator cupSwap(cupPosition firstP, cupPosition secondP)
    {
        Debug.Log("cupSwap");
        GameObject firstCup = pickCup(firstP);
        GameObject secondCup = pickCup(secondP);
        Cups firstScript = pickScript(firstP);
        Cups secondScript = pickScript(secondP);
        Vector3 vector1 = new Vector3(firstCup.transform.position.x, firstCup.transform.position.y, firstCup.transform.position.z - 0.25f);
        Vector3 vector2 = new Vector3(secondCup.transform.position.x, secondCup.transform.position.y, secondCup.transform.position.z + 0.25f);
        float holderOne = firstCup.transform.position.x;
        float holderTwo = secondCup.transform.position.x;

        firstScript.setTarget(vector1);
        secondScript.setTarget(vector2);
        yield return new WaitForSeconds(.1f);

        float holderHalf;
        holderHalf = (holderOne + holderTwo) / 2;
        vector1 = new Vector3(holderHalf, firstCup.transform.position.y, firstCup.transform.position.z);
        vector2 = new Vector3(holderHalf, secondCup.transform.position.y, secondCup.transform.position.z);
        firstScript.setTarget(vector1);
        secondScript.setTarget(vector2);
        yield return new WaitForSeconds(.1f);

        vector1 = new Vector3(holderTwo, firstCup.transform.position.y, firstCup.transform.position.z);
        vector2 = new Vector3(holderOne, secondCup.transform.position.y, secondCup.transform.position.z);
        firstScript.setTarget(vector1);
        secondScript.setTarget(vector2);
        yield return new WaitForSeconds(.1f);

        vector1 = new Vector3(firstCup.transform.position.x, firstCup.transform.position.y, firstCup.transform.position.z + 0.25f);
        vector2 = new Vector3(secondCup.transform.position.x, secondCup.transform.position.y, secondCup.transform.position.z - 0.25f);
        firstScript.setTarget(vector1);
        secondScript.setTarget(vector2);
        updatePositions(firstP, secondP);
        yield return new WaitForSeconds(.1f);
    }

    GameObject pickCup(cupPosition pos)
    {
        cupPosition cup;
        switch(pos)
        {
            case cupPosition.cp1:
                cup = positionOne;
            break;
            case cupPosition.cp2:
                cup = positionTwo;
            break;
            default: 
                cup = positionThree;
            break;
        }

        switch(cup)
        {
            case cupPosition.cp1:
                return c1;
            break;
            case cupPosition.cp2:
                return c2;
            break;
            default: 
                return c3;
            break;
        }
    }

    Cups pickScript(cupPosition pos)
    {
        cupPosition cup;
        switch(pos)
        {
            case cupPosition.cp1:
                cup = positionOne;
            break;
            case cupPosition.cp2:
                cup = positionTwo;
            break;
            default: 
                cup = positionThree;
            break;
        }

        switch(cup)
        {
            case cupPosition.cp1:
                return cup_one;
            break;
            case cupPosition.cp2:
                return cup_two;
            break;
            default: 
                return cup_three;
            break;
        }
    }

    void updatePositions(cupPosition firstC, cupPosition secondC)
    {
        cupPosition temp;
        switch(firstC)
        {
            case cupPosition.cp1:
                switch(secondC)
                {
                    case cupPosition.cp2:
                        temp = positionOne;
                        positionOne = positionTwo;
                        positionTwo = temp;
                    break;
                    default: 
                        temp = positionOne;
                        positionOne = positionThree;
                        positionThree = temp;
                    break;
                }
            break;
            case cupPosition.cp2:
                switch(secondC)
                {
                    case cupPosition.cp1:
                        temp = positionTwo;
                        positionTwo = positionOne;
                        positionOne = temp;
                    break;
                    default: 
                        temp = positionTwo;
                        positionTwo = positionThree;
                        positionThree = temp;
                    break;
                }
            break;
            default: 
                switch(secondC)
                {
                    case cupPosition.cp1:
                        temp = positionThree;
                        positionThree = positionOne;
                        positionOne = temp;
                    break;
                    default: 
                        temp = positionThree;
                        positionThree = positionTwo;
                        positionTwo = temp;
                    break;
                }
            break;
        }   
    }
}