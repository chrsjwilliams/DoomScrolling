using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public RectTransform panel;
    public Button[] buttons;
    public RectTransform center;

    private float[] distances;
    private bool dragging = false;
    private int buttonDistance;
    private int minButtonNum;

    // Start is called before the first frame update
    void Start()
    {
        int buttonLength = buttons.Length;
        distances = new float[buttonLength];

        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.y - buttons[0].GetComponent<RectTransform>().anchoredPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            distances[i] = Mathf.Abs(center.transform.position.y - buttons[i].transform.position.y);
        }

        float minDistance = Mathf.Min(distances);

        for(int i = 0; i < buttons.Length; i++)
        {
            if(minDistance == distances[i])
            {
                minButtonNum = i;
            }
        }

        if(!dragging)
        {
            LerpToButton(minButtonNum * buttonDistance);
        }
    }

    void LerpToButton(int position)
    {
        float newY = Mathf.Lerp(panel.anchoredPosition.y, position, Time.deltaTime * 10.0f);
        Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newY);

        panel.anchoredPosition = newPosition;
    }

    public void BeginDragging(){    dragging = true;    }
    public void EndDragging(){      dragging = false;   }
}
