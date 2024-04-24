using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class layermsk : MonoBehaviour
{
    public int numbermsk;
    public LayerMask msk;
    void Update()
    {
        numbermsk = msk;
    }
}
