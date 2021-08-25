using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public enum UnitType
    {
        wood,
        stone,
        water
    }

    public UnitType unitType; 
    public NavMeshAgent navmeshagent;
    private Animator anim;
    private ResourceLoading[] resources;
    public ResourceLoading resource;

    public Building[] buildings;
    public Building building;

    private Vector3 startPos;

    public bool onDestination;
    public bool taskFinished;

    public int resourcesToLoad;

    public GameObject tool;
    public GameObject collectedItems;
    private GameObject collectingPoints;

    
    void Awake()
    {
       
        navmeshagent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        resources = FindObjectsOfType<ResourceLoading>();
        buildings = FindObjectsOfType<Building>();
        startPos = transform.position;

        GetCurrentType();
        UpgradingUnitValues();
      
    }


    void Update()
    {
        AgentMovement();
        CheckDestination();

        if (onDestination)
        {
            resource.characterCollecting = true;
            Returning();
        }

    }

    private void AgentMovement()
    {
        if (!onDestination)
        {
            if (!taskFinished)
                navmeshagent.SetDestination(FindClosestPoint().position);
            else
                navmeshagent.SetDestination(startPos);
        }
       
    }

    public void Returning()
    {
        if (resourcesToLoad == resource.resourceNumber)
        {
            anim.SetTrigger("Carrying");
            tool.SetActive(false);
            collectedItems.SetActive(true);
            resource.characterCollecting = false;
            onDestination = false;
            taskFinished = true;

        }
    }

    private void CheckDestination()
    {
        if (!navmeshagent.pathPending)
        {
            if (navmeshagent.remainingDistance <= navmeshagent.stoppingDistance)
            {
                if (!navmeshagent.hasPath || navmeshagent.velocity.sqrMagnitude == 0f)
                {
                    onDestination = true;
                    anim.SetTrigger("CollectWood");
                    transform.rotation = (FindClosestPoint().rotation);
                }
            }

        }
    }
    private Transform FindClosestPoint()
    {
    
        collectingPoints = GameObject.FindGameObjectWithTag(unitType.ToString() + "Point");
        float minDistance = Vector3.Distance(transform.position, collectingPoints.transform.GetChild(0).position);
        Transform closestPoint = collectingPoints.transform.GetChild(0);

        for(int i = 0; i < collectingPoints.transform.childCount; i++)
        {
            if(Vector3.Distance(transform.position, collectingPoints.transform.GetChild(i).position) < minDistance) {
                minDistance = Vector3.Distance(transform.position, collectingPoints.transform.GetChild(i).position);
                closestPoint = collectingPoints.transform.GetChild(i);
            }
        }
        return closestPoint;
    }

    private void GetCurrentType()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].collectingPoint == FindClosestPoint())
            {
                resource = resources[i];
            }
        }

        for(int i = 0; i < buildings.Length; i++)
        {
            if(buildings[i].buildingType.ToString() == unitType.ToString())
            {
                building = buildings[i];
            }
        }
    }

    public void UpgradingUnitValues()
    {
        navmeshagent.speed = building.agentSpeed;
        resourcesToLoad = building.resourceToLoad;
        resource.collectingSpeed = building.collectSpeed;
    }
}
