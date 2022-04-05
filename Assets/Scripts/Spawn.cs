using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // create a block array to add out blocks to it
    [Header("Blocks")]
    public GameObject[] blocks;
    // shadow of the blocks
    public GameObject[] shadows;
    // spawn & shadow position
    public Vector3 spawnPos;
    public Vector3 shadowPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnBlock();
    }

    // spawn block from list at the top spawn point
    public void spawnBlock()
    {
        // choose a random block and spawn it
        int randomBlock = Random.Range(0,blocks.Length);
        GameObject block = Instantiate (blocks[randomBlock], spawnPos, Quaternion.identity);
        // generate a shadow gameObj
        GameObject shadowBlock = Instantiate(shadows[randomBlock], shadowPos, Quaternion.identity);
    }
}
