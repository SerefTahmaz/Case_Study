using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

//Create a level of grid of cubes
public class GridSpawn : MonoBehaviour
{

    //Object to spawn
    public GameObject[] itemsToPickFrom;
    //Grid size in x axes
    public int gridX;
    //Grid size in z axes
    public int gridZ;
    //Grid spacing offset
    public float gridSpacingOffset = 1f;
    //Grid Origin point
    public Vector3 gridOrigin = Vector3.zero;
    //Hold all created cubes
    List<GameObject> oldCubes;
    private void Start()
    {
        oldCubes = new List<GameObject>();
        //When levels start, play levelstartsound
        if (AudioManager.Initialized)
            AudioManager.Play(AudioClipName.LevelStartSound);
    }
    void Update()
    {
        //When button clicks, then create Level
        if (CrossPlatformInputManager.GetButtonDown("Create"))
        {
            //Destroy old cubes
            foreach (GameObject g in oldCubes)
                Destroy(g);
            //Spawn grid
            SpawnGrid();
        }
    }
    
    //Create Level
    void SpawnGrid()
    {
        //Base level, spawn yellow cubes
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                //Traverse all x, z locations
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                //Spawn a cube at every grid point
                PickAndSpawn(spawnPosition, Quaternion.identity, 0);
            }
        }

        //Upper level, spawn blue cubes
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                //Traverse all x, z locations
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 1, z * gridSpacingOffset) + gridOrigin;
                //Spawn a cube at every grid point
                PickAndSpawn(spawnPosition, Quaternion.identity, 1);
            }
        }

    }

    //Spawning cubes
    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn, int indexF)
    {
        //Instantiate cube
        GameObject g = Instantiate(itemsToPickFrom[indexF], positionToSpawn, rotationToSpawn);
        //Add list the created cube
        oldCubes.Add(g);
    }
}
