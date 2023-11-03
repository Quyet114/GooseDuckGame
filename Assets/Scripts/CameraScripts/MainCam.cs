using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public float start, end, down, up;
    private Vector3 Vector3Velocity = Vector3.zero;
    public Transform character; // Tham chiếu đến GameObject nhân vật
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Độ lệch vị trí Camera

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            var charaterX = character.transform.position.x;
            var charaterY = character.transform.position.y;
            var camO = transform.position.x;
            var camV = transform.position.y;
            if (charaterX > start && charaterX < end)
            {
                camO = charaterX;

            }
            else
            {
                if (charaterX < start)
                {
                    camO = start;

                }
                if (charaterX > end)
                {
                    camO = end;
                }

            }
            if (charaterY > down && charaterY < up)
            {
                camV = charaterY;
            }
            else
            {
                if (charaterY < down)
                {
                    camV = down;
                }
                if (charaterY > up)
                {
                    camV = up;
                }
            }
            // Đồng bộ hóa vị trí Camera với vị trí của nhân vật
            Vector3 targetPosition = character.position + offset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                new Vector3(camO + 2f, camV, -1f),
                ref Vector3Velocity,
                0.1f
                );
        }

        //-------------------------------------------------------------
        /*        // lấy vị trí

                var playerX = player.transform.position.x;
                // lấy vị trí trục x của cam
                var camX = transform.position.x;

                if (playerX > start && playerX < end)
                {
                    camX = playerX;
                }
                else
                {
                    if (playerX < start)
                    {
                        camX = start;
                    }
                    if (playerX > end)
                    {
                        camX = end;
                    }
                }
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    new Vector3(camX,0,-10),
                    ref Vector3Velocity,
                    0.3f
                   );*/
    }
}
