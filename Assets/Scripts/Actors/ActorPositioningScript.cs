using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPositioningScript : MonoBehaviour
{
    public Vector2Int gridPos;
    public int moveRange;

    public GameObject highlightPlane;
    public GameObject highlightHolder;
    public bool highlightStateBuffer;

    private void Awake()
    {
        GenerateHighlight();
        SetHighlightState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (highlightStateBuffer == false)
        {
            SetHighlightState(false);
        }
    }

    private void LateUpdate()
    {
        highlightStateBuffer = false;
    }

    public void SetHighlightState(bool state)
    {
        highlightHolder.SetActive(state);
    }

    [ContextMenu("Generate Highlights")]
    public void GenerateHighlight()
    {
        for (int j = -moveRange; j <= moveRange; j++)
        {
            for (int i = -moveRange; i <= moveRange; i++)
            {
                if (Mathf.Abs(j) + Mathf.Abs(i) <= moveRange)
                {
                    Debug.Log("Plotting: " + i + "," + j);
                    var temp = Instantiate(highlightPlane, Vector3.zero, Quaternion.identity);
                    temp.transform.parent = highlightHolder.transform;
                    temp.transform.localPosition = new Vector3(i, -.45f, j);
                    temp.transform.localRotation = Quaternion.identity;
                }
            }
        }
        highlightHolder.SetActive(false);
    }
}
