using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Navigate : MonoBehaviour
{
    private NavMeshAgent agent;
    public bool isPathfindingEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        PathFingding();
    }

    private bool IsOverUI()
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }



    private void PathFingding()
    {
        if (Input.GetMouseButtonUp(0) && !IsOverUI() && isPathfindingEnabled)
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            agent.destination = target;
        }

    }

    public void EnablePathfinding()
    {
        isPathfindingEnabled = true;
    }

    public void DisablePathfinding()
    {
        isPathfindingEnabled = false;
    }

}