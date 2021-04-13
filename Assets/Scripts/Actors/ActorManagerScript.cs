using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManagerScript : MonoBehaviour
{
    private Camera mainCamera;
    private TileGridManager tileGridScript;
    public Vector3 positionOffset;

    public GameObject clickNewTarget, clickLastTarget;
    public List<GameObject> playerActors, enemyActors;

    // Start is called before the first frame update
    void Start()
    {
        tileGridScript = GameObject.Find("TileManager").GetComponent<TileGridManager>();
        mainCamera = Camera.main;
        InitialPositioning();
        clickNewTarget = clickLastTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        clickNewTarget = MouseClickCheck();

        if (clickLastTarget.tag == "PlayerActor")
        {
            var actorScript = clickLastTarget.GetComponent<ActorPositioningScript>();
            actorScript.SetHighlightState(true);
            // tileGridScript.HighlightPossibleMoves(actorScript.gridPos, actorScript.moveRange);
            if (clickNewTarget.tag == "Tile")
            {
                Vector2Int tileTarget = new Vector2Int(clickNewTarget.GetComponent<TileVariablesScript>().posX, clickNewTarget.GetComponent<TileVariablesScript>().posY);
                if (MoveValid(actorScript.gridPos, tileTarget, actorScript.moveRange))
                {
                    ActorPositioning(clickLastTarget, tileTarget);
                }
            }
        }
        if (clickLastTarget.tag == "EnemyActor")
        {
            clickLastTarget.GetComponent<ActorPositioningScript>().SetHighlightState(true);
        }

    }

    private void LateUpdate()
    {
        clickLastTarget = clickNewTarget;
    }

    GameObject MouseClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked on: " + hit.transform.name);
                return hit.transform.gameObject;
            }
        }
        return clickLastTarget;
    }

    void InitialPositioning()
    {
        foreach (GameObject actor in playerActors)
        {
            var actorScript = actor.GetComponent<ActorPositioningScript>();
            ActorPositioning(actor, actorScript.gridPos);
        }
        foreach (GameObject actor in enemyActors)
        {
            var actorScript = actor.GetComponent<ActorPositioningScript>();
            ActorPositioning(actor, actorScript.gridPos);
        }
    }

    void ActorPositioning(GameObject actor, Vector2Int tileTarget)
    {
        GameObject targetObject = tileGridScript.gridTiles[tileTarget.x, tileTarget.y];
        actor.transform.position = targetObject.transform.position;
        actor.transform.position += positionOffset;
        actor.GetComponent<ActorPositioningScript>().gridPos = tileTarget;
    }

    bool MoveValid(Vector2Int actorPos, Vector2Int targetPos, int range)
    {
        if (Mathf.Abs(targetPos.x - actorPos.x) + Mathf.Abs(targetPos.y - actorPos.y) <= range)
        {
            return true;
        }
        return false;
    }
}
