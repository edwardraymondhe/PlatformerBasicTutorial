using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour
{
    public int rotOffset = 90;

    // Update is called once per frame
    void Update()
    {
        // subtracting the player pos from the mouse pos;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // keep the porpotion, just smaller that…就是标准化妈的. Normalizing the vector. Meaning that all the sum of the vector will be equal to 1.
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;   // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotOffset);   // 三维旋转
    }
}
