using UnityEngine;
using System.Collections;

public class boxScript : MonoBehaviour {
    public GameObject outerCorner;
    public GameObject innerCorner;
    public GameObject floor;
    public GameObject wall;
    public GameObject window;
    public GameObject pillar;
    private GameObject parent;  //Instantiating all of our tiles into the parent GameObject
    public string Editor_string1 = "Manually input generation Settings";
    public bool useManual;
    public int hallwayWidth;    //Hallways can be decomposed into squares, thus the distance from innerWalls to outerWalls is hallwayWidth
    public int roomWidth;
    public int roomHeight;
    GUIHandler gui;

    GameObject[] innerCorners = new GameObject[4];
    GameObject[] outerCorners = new GameObject[4];
    int innerWallCount; //how many walls do we have?
    int outerWallCount;
    //GameObject[] innerWalls = new GameObject[40];
    ArrayList innerWalls = new ArrayList();
    ArrayList outerWalls = new ArrayList();
    ArrayList flat = new ArrayList();
    //GameObject[] outerWalls = new GameObject[40];
    //Generation Data
    bool check = false; //Do the manual numbers pass?
    bool doNumbers = false; //have the numbers been generated yet?
    bool doOuterCorners = false; //have the Outer Corners been generated yet?
    bool doInnerCorners = false;
    bool doOuterWalls = false;
    bool doInnerWalls = false;
    bool doFlat = false;
    int x = 0;
    int y = 0;

    public string checkInput() {
        int hallX = roomWidth - 2 * hallwayWidth;
        int hallY = roomHeight - 2 * hallwayWidth;
        if(hallX >= 2 && hallY >= 2) {
            check = true;
            return "All is good!";
        }
        else if(!(hallX >= 2)) {
            check = false;
            return "roomWidth does not equal minimum requirements";
        }
        else if (!(hallY >= 2)) {
            check = false;
            return "roomHeight does not equal minimum requirements";
        }
        return "Cannot calculate";
    }
    //generates Floor and Ceiling
    void generateFlat() {
        if(doInnerWalls) {
            //do y1 first
            for(int i=0;i<hallwayWidth;i++) {
                for(int foo=0;foo<y;foo++) {
                    flat.Add((GameObject)GameObject.Instantiate(floor, new Vector3(i+0.5f, 0, foo), Quaternion.identity));
                }
            }
            for (int i = 0; i < hallwayWidth; i++) {
                for (int foo = 0; foo < x; foo++) {
                    flat.Add((GameObject)GameObject.Instantiate(floor, new Vector3((foo + 0.5f) + hallwayWidth, 0, (y-1) - i), Quaternion.identity));
                }
            }
            for (int i = 0; i < hallwayWidth; i++) {
                for (int foo = 0; foo < y; foo++) {
                    flat.Add((GameObject)GameObject.Instantiate(floor, new Vector3((roomWidth + 1)-(i + 1.5f), 0, foo), Quaternion.identity));
                }
            }
            for (int i = 0; i < hallwayWidth; i++) {
                for (int foo = 0; foo < x; foo++) {
                    flat.Add((GameObject)GameObject.Instantiate(floor, new Vector3((foo + 0.5f) + hallwayWidth, 0, i), Quaternion.identity));
                }
            }
            foreach (GameObject foo in flat) {
                foo.transform.parent = parent.transform;
            }
            doFlat = true;
        }
    }
    void generateInnerWalls() {
        if(doOuterWalls) {
            int genLoop1;
            int genLoop2;
            int genLoop3;   //we didn't learn our lesson from generateOuterWalls()
            int genLoop4;
            x = roomWidth - (2 * hallwayWidth); //X is the horizontal length of our innerWalls
            y = roomHeight; //Y is the vertical length of our innerWalls
            Debug.Log("X: " + x);
            Debug.Log("Y: " + y);
            int specialY = roomHeight - (2 * hallwayWidth); //I don't know why this is neccessary, but it is.
            genLoop1 = specialY/2 -2;
            genLoop2 = x / 2 - 2;
            genLoop3 = specialY / 2 -2;
            genLoop4 = x / 2 - 2;
            for(int i = 0;i<genLoop1;i++) {
                innerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3(hallwayWidth, 0, (hallwayWidth+2) + (i * 2) + 1), Quaternion.Euler(0, 90, 0)));
            }
            for(int i = 0; i<genLoop2;i++) {
                innerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3((hallwayWidth + 2) + (i * 2) + 1, 0, roomHeight-hallwayWidth), Quaternion.Euler(0, 180, 0)));
            }
            for (int i = genLoop3; i > 0; i--) {
                innerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3(roomWidth - hallwayWidth, 0, ((roomHeight - hallwayWidth) - 2) - (i * 2) + 1), Quaternion.Euler(0, 270, 0)));
            }
            for (int i = genLoop4; i > 0; i--) {
                innerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3((roomWidth - hallwayWidth) - (i*2) - 1, 0,hallwayWidth), Quaternion.Euler(0, 0, 0)));
            }
            foreach (GameObject foo in innerWalls) {
                foo.transform.parent = parent.transform;
            }
            doInnerWalls = true;
        }
    }
    void generateOuterWalls() {
        if (doInnerCorners) {
            outerWallCount = (roomHeight / 2) + (roomHeight % 2) + (roomWidth / 2) + (roomWidth % 2);
            int genLoop1;   //the amount of times a Loop will iterate
            int genLoop2;
            int genLoop3;
            int genLoop4;
            genLoop1 = roomHeight / 2;
            if (roomHeight % 2 != 0) {
                genLoop1--;
                genLoop1--;
            }
            genLoop2 = (roomWidth / 2);
            genLoop3 = genLoop1;    //In hindsight, it's stupid to have this variable and genLoop4
            genLoop4 = genLoop2;
            for (int i = 0; i < genLoop1; i++) {
                outerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3(0, 0, (i * 2 + 1)), Quaternion.Euler(0, 270, 0)));
            }
            for (int i = 0; i < genLoop2; i++) {
                outerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3((i * 2 + 1), 0, roomHeight), Quaternion.Euler(0, 0, 0)));
            }
            for (int i = genLoop3; i > 0; i--) {
                outerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3(roomWidth, 0, i * 2 - 1), Quaternion.Euler(0, 90, 0)));
            }
            for (int i = genLoop4; i > 0; i--) {
                outerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3((i * 2 - 1), 0, 0), Quaternion.Euler(0, 180, 0)));
            }
            //NOTE
            //This conditional checks if roomHeight is odd. Since the wall tiles are 2 units long, we have
            //to be clever with scaling and positioning in order to make two gameobjects take the space of
            //3 units. OR we can just make everything be even. I like this second option, I have
            //implemented this option. 
            /*if (roomHeight % 2 != 0) {
                int count = genLoop1;
                float foo = genLoop1 * 2;
                innerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3(0.0f, 0.0f, (foo + 0.75f)), Quaternion.Euler(0, 270, 0)));
                GameObject x1 = (GameObject)innerWalls[count]; //
                x1.name = "X1";
                x1.transform.localScale = new Vector3(0.75f, 1, 1);
                foo = foo + 1.0f;
                count++;
                innerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3(0, 0, (foo + 1.25f)), Quaternion.Euler(0, 270, 0)));
                GameObject x2 = (GameObject)innerWalls[count];
                x2.name = "X2";
                x2.transform.localScale = new Vector3(0.75f, 1, 1);
                foo = foo + 1.0f;
                innerWalls.Add((GameObject)GameObject.Instantiate(wall, new Vector3(0, 0, foo + 2), Quaternion.Euler(0, 270, 0)));
            }*/
            foreach (GameObject obj in outerWalls) {
                obj.transform.parent = parent.transform;
            }
            doOuterWalls = true;
        }
    }
    void generateOuterCorners() {
        //Outer Corner pieces need pivot point changed so they occupy a single 1x1 tile on the Unity grid.
        //We have to change this in the script, but to safeguard the proposed change we'll attach a boolean
        //to be turned off when the pieces get fixed.
        //Pieces that need the fix: Wallpieces, outerTiles
        //Although my generation could just be that incorrect.
        if (doNumbers) {
            Debug.Log("Beginning generateOuterCorners()");
            //Instantiate tiles
            outerCorners[0] = (GameObject)Object.Instantiate(outerCorner, new Vector3(0, 0), Quaternion.Euler(0,90,0));
            outerCorners[1] = (GameObject)Object.Instantiate(outerCorner, new Vector3(0, 0, roomHeight), Quaternion.Euler(0,180,0));
            outerCorners[2] = (GameObject)Object.Instantiate(outerCorner, new Vector3(roomWidth, 0, roomHeight), Quaternion.Euler(0,270,0));
            outerCorners[3] = (GameObject)Object.Instantiate(outerCorner, new Vector3(roomWidth, 0), Quaternion.identity); //this rotation is good
            //Making the new object a child of the boxCutout Script
            //http://answers.unity3d.com/questions/16339/how-can-i-instantiate-a-clone-as-a-child-of-anothe.html
            outerCorners[0].transform.parent = parent.transform;
            outerCorners[1].transform.parent = parent.transform;
            outerCorners[2].transform.parent = parent.transform;
            outerCorners[3].transform.parent = parent.transform;
            doOuterCorners = true;
        }
    }   
    void generateInnerCorners() {
        //This method is eerily similar to generateOuterCorners(). In fact the only thing that changes is the position, and the gameobject. 
        if(doOuterCorners) {
            Debug.Log("Beginning generateInnerCorners()");
            //Apparently all the rotation works 
            //yayayaaya
            innerCorners[0] = (GameObject)Object.Instantiate(innerCorner, new Vector3(hallwayWidth,0,hallwayWidth), Quaternion.Euler(0, 90, 0));
            innerCorners[1] = (GameObject)Object.Instantiate(innerCorner, new Vector3(hallwayWidth,0,(roomHeight-hallwayWidth)), Quaternion.Euler(0, 180, 0));
            innerCorners[2] = (GameObject)Object.Instantiate(innerCorner, new Vector3((roomWidth - hallwayWidth), 0, (roomHeight - hallwayWidth)), Quaternion.Euler(0, 270, 0));
            innerCorners[3] = (GameObject)Object.Instantiate(innerCorner, new Vector3((roomWidth - hallwayWidth), 0, hallwayWidth), Quaternion.identity);
            //set GameObjects to become a child
            innerCorners[0].transform.parent = parent.transform;
            innerCorners[1].transform.parent = parent.transform;
            innerCorners[2].transform.parent = parent.transform;
            innerCorners[3].transform.parent = parent.transform;
            doInnerCorners = true;
        }
    }
    public void doRecieve() {
        Debug.LogWarning("Ehhhhhhh");
    }
    public void generateDimensions() {
        //This method generates our numbers; roomHeight, roomWidth, hallwayWidth
        hallwayWidth = Random.Range(2, 10);
        //find the lower bounds of acceptable generation
        int min = (3 * hallwayWidth) + 2;   //The distance from corner to corner (square) has to greater than 3 Hallway widths and one more unit for the cutout
        roomHeight = Random.Range(min, min+25);
        roomWidth = Random.Range(min, min+25);
        doNumbers = true;

        gui.textField = "hallwayWidth: " + hallwayWidth + ". roomHeight: " + roomHeight + ". roomWidth: " + roomWidth;
        Debug.Log("hallwayWidth: " + hallwayWidth + ". roomHeight: " + roomHeight + ". roomWidth: " + roomWidth);
    }
    public int Setup(GameObject foo, GameObject handler) {
        gui = handler.GetComponent<GUIHandler>();
        try {
            parent = foo;
        }
        catch(System.Exception e) {
            Debug.LogException(e);
            return 0;
        }
        return 1;
    }
    public void build() {
        bool pass = false;
        //make everything even!
        while (!pass) {
            generateDimensions();
            if(roomHeight % 2 == 0 && roomWidth % 2 == 0) {
                pass = true;
            }
        }
        generateOuterCorners();
        generateInnerCorners();
        generateOuterWalls();
        generateInnerWalls();
        generateFlat();
    }
    public void destroy() {
        doOuterCorners = false;
        doInnerCorners = false;
        doOuterWalls = false;
        doInnerWalls = false;
        for(int i=0;i<4;i++) {
            Destroy(innerCorners[i]);
        }
        for (int i = 0; i < 4; i++) {
            Destroy(outerCorners[i]);
        }
        foreach (GameObject x in outerWalls) {
            Destroy(x);
        }
        foreach (GameObject x in innerWalls) {
            Destroy(x);
        }
        foreach (GameObject x in flat) {
            Destroy(x);
        }
    }
}
