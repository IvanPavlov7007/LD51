using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRedirectToParent : MonoBehaviour
{
    private void OnMouseDown()
    {
       SendMessageUpwards("Click");
    }
}
