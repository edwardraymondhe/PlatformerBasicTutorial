using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{

    public int offsetX = 2; // The offset in case the buddy isn't there when the player gets the border

    // For checking if we need to extend the border(current buddy)
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false;   // for background elements that're not tilable

    private float spriteWidth = 0f;     // the width of our element
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();  // Only going to return the first one
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // does it still need buddies? If not do nothing
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            // calculate the cameras extend (half the width) of what the camera can see in world coordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
            //Debug.Log(camHorizontalExtend);
            // calculate the x position where the camera can se the edge of the sprite(element)
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;
            // creating the tiles, checking if we can see the edge of the element and then calling MakeNewbuddy if we can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                Debug.Log(camHorizontalExtend);

                Debug.Log("x" + myTransform.position.x);
                Debug.Log("right" + edgeVisiblePositionRight);
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                Debug.Log(camHorizontalExtend);

                Debug.Log("x" + myTransform.position.x);

                Debug.Log("left" + edgeVisiblePositionLeft);
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    // pass the position of the edgeVisiblePosition
    // to know whether it's a right or left buddy, then pass the var above
    // a function that created a buddy on the side required
    void MakeNewBuddy(int rightOrLeft)
    {
        // no extra if statement with the 1,-1
        // calculationg the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //Debug.Log(spriteWidth);
        // instantating our new body and storing him in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;   // the instantiate only returns the object

        // if not tilable reverse the x size of our boj to get rid of ugly seams
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }else{
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}