using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    private float cameraZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + .5f, player.position.y + .5f, cameraZ);
    }
}
