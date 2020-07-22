using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public int moveSpeed = 50;

    void Update()
    {
        // if we don't need any calculation, rigidbody is too much
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

        Destroy(gameObject, 1.5f);
    }
}
