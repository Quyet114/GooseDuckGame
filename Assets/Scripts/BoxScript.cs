using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private bool isPushed = false; // Trạng thái khi bị đẩy

    private void Update()
    {
        if (isPushed)
        {
            // Di chuyển OB theo hướng của OB (theo hướng đã đẩy từ nhân vật)
            transform.Translate(Vector3.right * Time.deltaTime); // Di chuyển sang phải
        }
    }

    public void SetPushed(bool pushed)
    {
        isPushed = pushed;
    }
}
