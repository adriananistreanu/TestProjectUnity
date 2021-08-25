using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Building : MonoBehaviour
{
    public enum BuildingType
    {
        wood,
        stone,
        water
    }

    public BuildingType buildingType;
    public int necessaryResources;
    public Text resourceCount;

    public UnitController character;
    public GameObject characterClone;
    public GameObject instantiatedClone;

    public GameObject BuildingMenu;
    public GameObject NextBuildingClone;
    public Vector3 nextBuildingPos;

    public float agentSpeed, collectSpeed;
    public int resourceToLoad;
    public Text agentSpeedUI, collectSpeedUI, resourceToLoadUI;

    private void Awake()
    {
        agentSpeed = 4.86f;
        collectSpeed = 10f;
        resourceToLoad = 1;
        SpawnInitialUnit();
    }

    void Start()
    {
        character = instantiatedClone.GetComponent<UnitController>();
        resourceCount.text = "0/" + necessaryResources.ToString();
    }


    private void OnMouseDown()
    {
        if (BuildingMenu.activeSelf == false)
            BuildingMenu.SetActive(true);
        else
            BuildingMenu.SetActive(false);
    }

    void Update()
    {
        ResetUnit();
    }

    private void SpawnInitialUnit()
    {
        instantiatedClone = Instantiate(characterClone, characterClone.transform.position, characterClone.transform.rotation);
    }
    private void ResetUnit()
    {
        float distance = Vector3.Distance(transform.position, character.transform.position);

        if (character.taskFinished && distance < 7f)
        {
            Resources();
            RespawnUnit();
        }
    }

    private void RespawnUnit()
    {
       float distance = Vector3.Distance(transform.position, character.transform.position);
       
       if(character.taskFinished && distance < 7f)
        {
            instantiatedClone = Instantiate(characterClone, characterClone.transform.position, characterClone.transform.rotation);
            Destroy(character.gameObject);
            character = instantiatedClone.GetComponent<UnitController>();
            BuildingMenu.SetActive(true);
        }
    }

    private void Resources()
    {
        character.resource.SumResourceNumber();
        resourceCount.text = character.resource.totalNumber.ToString() + "/" + necessaryResources.ToString();
        character.resource.ResourceReset();
    }


    public void SpawnNewBuilding(GameObject button)
    {
        if(character.resource.totalNumber >= necessaryResources)
        {
            Instantiate(NextBuildingClone, nextBuildingPos, NextBuildingClone.transform.rotation);
            button.SetActive(false);
            ConsumedResources();
        }
    }

    public void UpgradeBuilding()
    {
        if (character.resource.totalNumber >= necessaryResources)
        {
            UpgradeModifyValues();
            UpgradeValuesUI();
            character.UpgradingUnitValues();
            ConsumedResources();
        }
    }

    private void ConsumedResources()
    {
        character.resource.totalNumber -= necessaryResources;
        necessaryResources += 3;
        character.resource.overallResourceCounter.text = character.resource.totalNumber.ToString();
        resourceCount.text = character.resource.totalNumber.ToString() + "/" + necessaryResources.ToString();
    }

    private void UpgradeModifyValues()
    {
        agentSpeed += 1f;
        resourceToLoad += 2;
        collectSpeed += 2f;
    }

    private void UpgradeValuesUI()
    {
        agentSpeedUI.text = agentSpeed.ToString();
        resourceToLoadUI.text = resourceToLoad.ToString();
        collectSpeedUI.text = collectSpeed.ToString();
    }

}
