using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    [Header("--SECTION GENERATION--")]
    [SerializeField] private GameObject section1;
    [SerializeField] private GameObject section2;
    [SerializeField] private GameObject section3;
    [SerializeField] private GameObject section4;
    [SerializeField] public List<GameObject> sections = new List<GameObject>();
    [SerializeField] private GameObject lastSection;
    [SerializeField] private float lastEndPoint;
    [SerializeField] private float newStartYPos;
    // Start is called before the first frame update
    void Start()
    {
        //Spawning the first sections
        lastSection = Instantiate(sections[Random.Range(0,sections.Count)],Vector3.zero,transform.rotation);
        lastEndPoint = lastSection.transform.Find("EndPoint").position.y;
        generateSection(2,54);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void generateSection(int spawnAmount,float position)
    {
        for (int i = 1; i < spawnAmount+1; i++)
        {
            //choose a random section to spawn
            int choosenSection = Random.Range(0, sections.Count);
            //54 is the lenght of each section 
            Vector3 spawnPos = new Vector3(i * position, 0, 0);
            //get lastEndPoint and lastSection
            lastSection = Instantiate(sections[choosenSection], spawnPos, Quaternion.identity);
            // calculate the y based on "b= a + ae - bs" and then change it 
            newStartYPos = lastSection.transform.Find("StartPoint").position.y;
            float yPos = lastSection.transform.position.y + lastEndPoint - newStartYPos;
            spawnPos = new Vector3(spawnPos.x, yPos, 0);
            lastSection.transform.position = spawnPos;
            // Set the end point to the one of the last section
            lastEndPoint = lastSection.transform.Find("EndPoint").position.y;


        }
    }
}
