using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week6 {
    public class MaterialChanger : MonoBehaviour
    {
        public Material material;

        public void ChangeMaterial()
        {
            if (material == null)
            {
                Debug.LogError("No material specified");
            }

            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arrMaterials)
            {
                if (r.gameObject != this.gameObject)
                {
                    r.material = material;
                }
            }
        }
    }


}
