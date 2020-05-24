using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public GameObject block;
    public GameObject ground;
    public Transform player;
    public Camera cam;
    
    public int sectorWidth = 5;
    public int height = 30;

    private int sector;

    void Start() {
        sector = 0;
        GenerateBlocks(sector);
        GenerateBlocks(sector-1);
        GenerateBlocks(sector+1);
    }

    void Update() {

        int curSector = GetPlayerSector(player.transform.position.x);

        // debug
        int left = curSector * sectorWidth * 2 - sectorWidth; 
        int right = curSector * sectorWidth * 2 + sectorWidth; 
        Debug.DrawLine(new Vector3(left, -10, 0), new Vector3(left, 100, 0));
        Debug.DrawLine(new Vector3(right, -10, 0), new Vector3(right, 100, 0));
        Debug.Log(curSector);

        if (curSector != sector) {

            int generate = (curSector > sector) ? curSector + 1 : curSector - 1;  
            int remove = (curSector > sector) ? sector - 1 : sector + 1;  

            GenerateBlocks(generate);
            RemoveBlocks(remove);

            sector = curSector;
        }
    }

    private int GetPlayerSector(float x) {
        
        if (x < -sectorWidth) return Mathf.FloorToInt((x + (float)sectorWidth) / ((float)sectorWidth * 2));

        if (x > sectorWidth) return Mathf.CeilToInt((x - (float)sectorWidth) / ((float)sectorWidth * 2));

        return 0;
    }

    void RemoveBlocks(int sect) {

        int left = sect * sectorWidth * 2 - sectorWidth; 
        int right = sect * sectorWidth * 2 + sectorWidth; 

        GameObject[] objs = FindObjectsOfType<GameObject>();

        foreach(GameObject obj in objs) {

            if (obj.tag == "Block" || obj.tag == "Lava") {

                float x = obj.transform.position.x;

                if(x >= (float)left && x <= (float)right) {
                    Destroy(obj);
                }
            }
        }
    }

    void GenerateBlocks(int sect) {

        int left = sect * sectorWidth * 2 - sectorWidth; 
        int right = sect * sectorWidth * 2 + sectorWidth; 

        GameObject g = Instantiate(ground) as GameObject;
        g.transform.position = new Vector3(sect * sectorWidth * 2, -9, 0);

        System.Random random = new System.Random();

        for (int i=0; i<50; i++) {

            int x = random.Next(left, right);
            int y = random.Next(1, height);


            GameObject b = Instantiate(block) as GameObject;
            b.transform.position = new Vector3(x, y, 0);
        }
    }
}
