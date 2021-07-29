using CardiacMassage;
using CodeUtilities.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour {
    private float graphHeight;
    private float graphWidth;

    [SerializeField] private Color successColor, failureColor;

    [SerializeField] private float yGraphScale;
    [SerializeField] private float xGraphScale;

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private float baseXValue;
    private List<CardiacMassagePressureData> valuesList = new List<CardiacMassagePressureData>();

    private void Awake() {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    }

    public void SetValuesList(CardiacMassagePressureData _cardiacMassagePressureData) {
        valuesList.Add(_cardiacMassagePressureData);
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

    private float CalculatePositionX(int _index) {
        return ((Mathf.Abs(valuesList[_index].Time) - baseXValue) * 50 / xGraphScale) * graphWidth;
    }

    private float CalculatePositionY(int _index) {
        return (Mathf.Abs(valuesList[_index].Depth) * 50 / yGraphScale) * graphHeight;
    }

    public void ShowGraph() {

        graphHeight = graphContainer.sizeDelta.y;
        graphWidth = graphContainer.sizeDelta.x;

        baseXValue = valuesList[0].Time;

        float finalXPosition = CalculatePositionX(valuesList.Count - 1);
        int count = 0;
        while(finalXPosition >= (graphWidth - (graphWidth * 3 / 100)) && count < 100) {
            count++;
            xGraphScale *= 1.1f;
            finalXPosition = CalculatePositionX(valuesList.Count - 1);
        }

        float finalYPosition = CalculatePositionY(valuesList.Count - 1);
        count = 0;
        while(finalYPosition >= (graphHeight - (graphHeight * 3 / 100)) && count < 100) {
            count++;
            yGraphScale *= 1.1f;
            finalYPosition = CalculatePositionY(valuesList.Count - 1);
        }

        GameObject lastCircleGameObject = null;
        for(int i = 0; i < valuesList.Count; i++) {
            //float xPosition = xSize + i * xSize;
            float xPosition = CalculatePositionX(i);
            float yPosition = CalculatePositionY(i);

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            circleGameObject.name = "circle" + i;

            if(lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }

        /*Debug.Log(graphWidth);
        Debug.Log(graphHeight);

        Debug.Log((Mathf.Abs(valuesList[0].Time) * 50 / xGraphScale) * graphWidth);
        Debug.Log((Mathf.Abs(valuesList[0].Depth) * 50 / yGraphScale) * graphHeight);

        Debug.Log((Mathf.Abs(valuesList[valuesList.Count - 1].Time) * 50 / xGraphScale) * graphWidth);
        Debug.Log((Mathf.Abs(valuesList[valuesList.Count - 1].Depth) * 50 / yGraphScale) * graphHeight);*/
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
