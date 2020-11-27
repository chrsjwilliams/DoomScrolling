using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public RectTransform panel;
    public Button[] buttons;
    public Button centeredButton;
    public RectTransform center;
    public int startButton = 0;

    public float[] distances;
    public float[] distReposition;
    private float lerpSpeed = 10;
    private bool dragging = false;
    private int buttonDistance;
    private int minButtonNum;
    private bool buttonIsCentered = false;
    private bool targetNearestButton = true;

    // Start is called before the first frame update
    void Start()
    {
        int buttonLength = buttons.Length;
        distances = new float[buttonLength];
        distReposition = new float[buttonLength];

        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.y - buttons[0].GetComponent<RectTransform>().anchoredPosition.y);
    
        panel.anchoredPosition = new Vector2(0, startButton * 750);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.y - buttons[i].GetComponent<RectTransform>().position.y; 
            distances[i] = Mathf.Abs(distReposition[i]);

            if(distReposition[i] < -2600)
            {
                float currY = buttons[i].GetComponent<RectTransform>().anchoredPosition.y;
                float currX = buttons[i].GetComponent<RectTransform>().anchoredPosition.x;

                Vector2 newAnchoredPosition = new Vector2(currX, currY - (buttons.Length * buttonDistance));

                buttons[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
            }

            if(distReposition[i] > 2600)
            {
                float currY = buttons[i].GetComponent<RectTransform>().anchoredPosition.y;
                float currX = buttons[i].GetComponent<RectTransform>().anchoredPosition.x;

                Vector2 newAnchoredPosition = new Vector2(currX, currY + (buttons.Length * buttonDistance));

                buttons[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
            }
        }

        if(targetNearestButton)
        {
            float minDistance = Mathf.Min(distances);

            for(int i = 0; i < buttons.Length; i++)
            {
                if(minDistance == distances[i])
                {
                    minButtonNum = i;
                
                }
            }
        }


        if(!dragging)
        {
            LerpToButton(-buttons[minButtonNum].GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    void LerpToButton(float position)
    {
        float newY = Mathf.Lerp(panel.anchoredPosition.y, position, Time.deltaTime * lerpSpeed);

        if(Mathf.Abs(position - newY) < 100f)
        {
            lerpSpeed = 20;
        }

        if(Mathf.Abs(newY) >= Mathf.Abs(position) - 1 && 
        Mathf.Abs(newY) <= Mathf.Abs(position) + 1 && !buttonIsCentered)
        {
            buttonIsCentered = true;
            centeredButton = buttons[minButtonNum];
            SendMessageFromButton(minButtonNum);
        }
        Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newY);

        panel.anchoredPosition = newPosition;
    }

    public void GoToButton(int buttonIndex)
    {
        targetNearestButton = false;
        minButtonNum = buttonIndex;
    }

    public void SendMessageFromButton(int buttonIndex)
    {
        if(buttonIndex == 1) Debug.Log("Hallo");
    }

    public void BeginDragging()
    {   
        lerpSpeed = 10;
        buttonIsCentered = false;
        dragging = true;

        targetNearestButton = true;    
    }
    public void EndDragging(){      dragging = false;   }
}
