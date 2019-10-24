using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class WashArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Marked")) {

            InkCanvas[] canvases = other.GetComponentsInChildren<InkCanvas>();
            foreach (InkCanvas canvas in canvases) {
                canvas.ResetPaint();
            }

            other.gameObject.layer = 0;
        }
    }
}
