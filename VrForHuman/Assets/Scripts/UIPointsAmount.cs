using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPointsAmount : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    public float lifeTime = 1f;
    public float moveSpeed = 1f;

    public float placementJitter = 0.5f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void SetPoints(int amount)
    {
        if (amount >= 0)
        {
            pointsText.text = "+ " + amount.ToString();
        }
        else
        {
            pointsText.text = amount.ToString();
        }
        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
    }
}
