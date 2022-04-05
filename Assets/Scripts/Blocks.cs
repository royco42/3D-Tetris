using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blocks : MonoBehaviour
{
	//
	private float prevTime;
	public float fallTime = 0.8f;
	
    // dimension of the grid cube
	public static int Xgrid = 7; // x width
    public static int Ygrid = 10; // y height
	public static int Zgrid = 7; // z width
	//
	public Vector3 rotPoiint;
	
    // save blocks position
	public static Transform[,,] fillGrid = new Transform[Xgrid,Ygrid+5,Zgrid];
    
    // a game over flag
    public bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
            // check movement of shadow
            if (validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position -= new Vector3(0, 1, 0);

                if (!validMoveShadow())
                {
                    GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
                }
            }

            // move the block down   	
            if (Time.time - prevTime > fallTime)
            {
                transform.position += new Vector3(0, -1, 0);

                if (!validMove()) // undo the move
                {  
                    transform.position -= new Vector3(0, -1, 0);
                    // add the block to grid
                    addBlocks();
                    // check for line
                    checkFullLine();
                    // destroy the shadow
                    Destroy(GameObject.FindWithTag("shadow"));
                    // disable and access spawn script to drop another one
                    this.enabled = false;

                // check for game over
                if (!gameOver)
                {
                    FindObjectOfType<Spawn>().spawnBlock();
                }
                else
                {
                    FindObjectOfType<GameManager>().GameOver();
                    gameOver = true;
                }
            }
            prevTime = Time.time;
            }

            //// movements keys
            // drop down 
            if (Input.GetKey(KeyCode.Z))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!validMove())
                { // if movement is not valid undo it
                    transform.position -= new Vector3(0, -1, 0);
                }
            }

            // x-left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position += new Vector3(-1, 0, 0);

                if (!validMove())
                { // if movement is not valid undo it
                    transform.position -= new Vector3(-1, 0, 0);
                    // move the shadow
                    GameObject.FindWithTag("shadow").transform.position -= new Vector3(-1, 0, 0);
                }
                if (!validMoveShadow())
                {
                    GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
                }
            }
            // x-right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position += new Vector3(1, 0, 0);

                if (!validMove())
                { // if movement is not valid undo it
                    transform.position -= new Vector3(1, 0, 0);
                    // move the shadow
                    GameObject.FindWithTag("shadow").transform.position -= new Vector3(1, 0, 0);
                }
                if (!validMoveShadow())
                {
                    GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
                }
            }
            // z-right
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, 0, -1);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0, -1);

                if (!validMove())
                { // if movement is not valid undo it
                    transform.position -= new Vector3(0, 0, -1);
                    // move the shadow
                    GameObject.FindWithTag("shadow").transform.position -= new Vector3(0, 0, -1);
                }
                if (!validMoveShadow())
                {
                    GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
                }
            }
        // z-left
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, 1);
            // move the shadow
            GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0, 1);

            if (!validMove())
            { // if movement is not valid undo it
                transform.position -= new Vector3(0, 0, 1);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position -= new Vector3(0, 0, 1);
            }
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
            }

        }
        
        // A,S,D -> rotates in x,y,z axis
        // x
        if (Input.GetKeyUp(KeyCode.A))
        { //
            transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), 90);
            // rotate shadow
            GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), 90);

            if (!validMove())
            { // rotate back if it is not a valid move
                transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), -90);
                // rotate shadow
                GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), -90);
            }

            // if shadow is below y-axis move +1 up
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
            }
        }
        // y axis
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), 90);
            // rotate shadow
            GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), 90);

            if (!validMove())
            { // rotate back if it is not a valid move
                transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), -90);
                // rotate shadow
                GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), -90);
            }
            // if shadow is below y-axis move +1 up
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
            }
        }
        // z axis
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), 90);
            // rotate shadow
            GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), 90);

            if (!validMove())
            { // rotate back if it is not a valid move
                transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), -90);
                // rotate shadow
                GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), -90);
            }
            // if shadow is below y-axis move +1 up
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 1, 0);
            }
        }
    }

    // function to check whether the movement is valid
    public bool validMove()
    {
    	foreach (Transform children in transform)
    	{
    		// get x,y,z position as int
    		// for all childrens - in this case cubes
    		int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
    		int Zpos = Mathf.RoundToInt(children.transform.position.z);

      		// if any exceed the border limit return false
    		if( Xpos < 0 || Xpos >= Xgrid || Ypos < 0 || Zpos < 0 || Zpos >= Zgrid )
    		{
    			return false;
    		}
            // check if position already taken
            if (fillGrid[Xpos,Ypos,Zpos] != null)
            {
                return false;
            }
    	}
    	return true;
    } 

    // change it to whether reach the end of fall on top of other block
    bool reachBottom()
    {
    	foreach (Transform children in transform)
    	{
    		// get y position as int
    		// for all childrens - in this case cubes
    		int Ypos = Mathf.RoundToInt(children.transform.position.y);
    		// if any exceed the bouder limit return false
    		if( Ypos <= 0 )
    		{
    			return false;
    		}
    	}
    	return true;
    }

    // keep track of fallen blocks add to array
    void addBlocks()
    {
        foreach (Transform children in transform)
        {
            // get x,y,z position as int
            // for all childrens - in this case cubes
            int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
            int Zpos = Mathf.RoundToInt(children.transform.position.z);

            fillGrid[Xpos,Ypos,Zpos] = children;

            // check for game over
            if (fillGrid[Xpos,10,Zpos] != null)
            {
                gameOver = true;
                //Debug.Log("game over");
            }
        }

    }

    // after a block landed check for full line to destroy
    void checkFullLine()
    {
        for(int y = 0; y < Ygrid; y++)
        {
            checkPlane(y);
        }
    }

    // check for full lines in xz plane
    void checkPlane(int y)
    {
        for(int x = 0; x < Xgrid; x++)
        { 
            // check x lines 
            if(isLineX(x,y))
            { // if there is a line
                for(int z = 0; z < Zgrid; z++)
                {
                    Destroy(fillGrid[x,y,z].gameObject);               
                    fillGrid[x,y,z] = null;
                }
                lineDownX(x,y);
            }
        }
        for(int z = 0; z < Zgrid; z++)
        { 
            // check z lines 
            if(isLineZ(y,z))
            { // if there is a line
                for(int x = 0; x < Xgrid; x++)
                {
                    Destroy(fillGrid[x,y,z].gameObject);
                    fillGrid[x,y,z] = null;
                }
                lineDownZ(z,y);
            }
        }
    }

    // check for line in x-axis
    bool isLineX(int x, int y)
    {
        for(int z =0; z < Zgrid; z++)
        {
            if(fillGrid[x,y,z] == null)
            { // if only one block is empty return flase
                return false;
            }
        }
        return true;
    }

    // check for line in z-axis
    bool isLineZ(int y, int z)
    { 
        for(int x =0; x < Xgrid; x++)
        {
            if(fillGrid[x,y,z] == null)
            { // if only one block is empty return flase
                return false;
            }
        }
        return true;
    }

    // then move line x down
    void lineDownX(int x, int j)
    {
        for (int y = j; y < 10; y++)
        {
            for (int z=0; z < Zgrid; z++)
            {
                if(fillGrid[x,y,z] != null)
                {
                    fillGrid[x,y-1,z] = fillGrid[x,y,z];
                    fillGrid[x,y,z] = null;
                    fillGrid[x,y-1,z].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }

    // then move line z down
    void lineDownZ(int z, int j)
    {
        for (int y = j; y < 10; y++)
        {
            for (int x=0; x < Xgrid; x++)
            {
                if(fillGrid[x,y,z] != null)
                {
                    fillGrid[x,y-1,z] = fillGrid[x,y,z];
                    fillGrid[x,y,z] = null;
                    fillGrid[x,y-1,z].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }

    // check for valid shadow placement
    bool validMoveShadow()
    {
    	foreach (Transform children in GameObject.FindWithTag("shadow").transform)
    	{
    		int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
            int Zpos = Mathf.RoundToInt(children.transform.position.z);

            if(Ypos < 0)
            {
            	return false;
            }
	    	if (fillGrid[Xpos,Ypos,Zpos] != null)
	            {
	                return false;
	            }

	        // deal with shadow in button but block are over it
	        for(int i=Ypos; i<10; i++)
	        {
	        	if(fillGrid[Xpos,i,Zpos] != null)
	        		GameObject.FindWithTag("shadow").transform.position = new Vector3(GameObject.FindWithTag("shadow").transform.position.x, i+1, GameObject.FindWithTag("shadow").transform.position.z);
	        }
	    }
	    return true;
    }

}
