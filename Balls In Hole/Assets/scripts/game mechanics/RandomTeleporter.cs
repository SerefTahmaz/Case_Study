using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleports what it touches
public class RandomTeleporter : MonoBehaviour
{
    //Holding grid script for grid positions
    public GridSpawn gridSpawn;

    //Teleport it when collide with it
    private void OnTriggerEnter(Collider other)
    {
        //If other is not player or balls, then ignore it
        if (other.gameObject.tag != "Players") return;

        //Holding possible teleport positions
        List<Vector3> possibleLocation = new List<Vector3>();
        //Looping grid
        for (int x = 0; x < gridSpawn.gridX; x++)
        {
            for (int z = 0; z < gridSpawn.gridZ; z++)
            {
                //Get each grid position
                Vector3 spawnPosition = new Vector3(x * gridSpawn.gridSpacingOffset, 0, z * gridSpawn.gridSpacingOffset) + gridSpawn.gridOrigin;

                //Test if spawnPosition is empty
                RaycastHit hit;
                if (Physics.Raycast(spawnPosition + new Vector3(0, 10, 0), -Vector3.up, out hit, 100))
                {
                    //Only level base cubes has a y value 0 so if it is zero, then free to spawn
                    if (hit.transform.position.y<=0.001)
                    {
                        possibleLocation.Add(spawnPosition);
                    }
                }
            }
        }
        //Getting random possible spawn position for variety
        other.gameObject.transform.position = possibleLocation[Random.Range(0, possibleLocation.Count)];

        if(AudioManager.Initialized)
        AudioManager.Play(AudioClipName.TeleportSound);
    }
}
