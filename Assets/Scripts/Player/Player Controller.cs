using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Navigate : MonoBehaviour
{
    private NavMeshAgent agent;
    

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
        return EventSystem.current.IsPointerOverGameObject();
    }

    

    private void PathFingding()
    {
        if (Input.GetMouseButtonUp(0) && !IsOverUI())
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            agent.destination = target;
        }
    }


}