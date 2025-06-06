using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f; // íœ‚³‚ê‚é‚Ü‚Å‚ÌŽžŠÔ
    public bool isDelete; // ‚Ô‚Â‚©‚Á‚½‚çÁ‚¦‚é‚©

    // Start is called before the first frame update
    void Start()
    {
        // ŽžŠÔ·‚ÅÁ–Å
        Destroy(gameObject, deleteTime);

        isDelete = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDelete)
        {
            Destroy(gameObject);
        }
    }

    // Edit > Project Settings > Physics2D > layer Collision Matrix
}
