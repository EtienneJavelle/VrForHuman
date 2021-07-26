/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using CodeUtilities.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    public List<float> valueListX;
    public List<float> valueListY;

    private void Awake() {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        valueListX = new List<float>();
        valueListY = new List<float>();
        //List<float> valueList = new List<float>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        //valueListY = new List<float>() { 1.05f, 1.19f, 1.11f, 1.21f, 1.3f, 1.31f, 1.305f, 1.049f, 0.97f, 1.43f };
        //ShowGraph(valueListY);
    }

    public void SetValueListX(float _value) {
        valueListX.Add(Mathf.Abs(_value));
    }

    public void SetValueListY(float _value) {
        valueListY.Add(Mathf.Abs(_value));
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(List<float> valueList) {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = 500f;
        float xSize = 500f;

        GameObject lastCircleGameObject = null;
        for(int i = 0; i < valueList.Count; i++) {
            //float xPosition = xSize + i * xSize;
            float xPosition = (valueListX[i] * 50 / xSize) * graphWidth;
            float yPosition = (valueList[i] * 50 / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            circleGameObject.name = "circle" + i;
            if(lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }

        Debug.Log(graphWidth);
        Debug.Log(graphHeight);

        Debug.Log((valueListX[0] * 50 / xSize) * graphWidth);
        Debug.Log((valueList[0] * 50 / yMaximum) * graphHeight);
        //Debug.Log(xSize + 0 * xSize);
        Debug.Log((valueListX[valueListX.Count - 1] * 50 / xSize) * graphWidth);
        Debug.Log((valueList[valueList.Count - 1] * 50 / yMaximum) * graphHeight);
        //Debug.Log(xSize + (valueList.Count - 1) * xSize);
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    internal void SetValuesListX(object depth) {
        throw new NotImplementedException();
    }
}
