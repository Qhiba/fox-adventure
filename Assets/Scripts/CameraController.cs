using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform background;

    public Vector2 maxRange;
    public Vector2 minRange;

    // Update is called once per frame
    void Update()
    {
        if((Vector2)player.position != (Vector2)transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), 0.1f);
        }

        float posX = Mathf.Clamp(transform.position.x, minRange.x, maxRange.x);
        float posY = Mathf.Clamp(transform.position.y, minRange.y, maxRange.y);
        transform.position = new Vector3(posX, posY, transform.position.z);

        background.position = new Vector2(transform.position.x, transform.position.y);
    }
}
