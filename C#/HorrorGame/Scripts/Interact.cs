using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HorrorGame
{
    public class Interact : MonoBehaviour
    {
        public string interactButton;

        public float interactDistance = 3f;
        public LayerMask interactLayer;
        public Image interactIcon;

        public Sprite InteractSprite;
        public Sprite noneInteractSprite;

        public bool isInteracting;

        // Start is called before the first frame update
        void Start()
        {
            if (interactIcon != null)
            {
                interactIcon.enabled = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            {     
                if (isInteracting == false)
                {

                    if (hit.collider.gameObject.GetComponent<InteractiveObject>())
                    {
                        InteractiveObject obj = hit.collider.gameObject.GetComponent<InteractiveObject>();
                        if (obj.interactive)
                        {
                            if (interactIcon != null)
                            {
                                interactIcon.sprite = InteractSprite;
                                

                            }

                            if (Input.GetButtonDown(interactButton))
                            {
                                obj.Interact();
                                //isInteracting = true;
                            }
                        }
                    }
                }
            }
            else {
                if (interactIcon != null)
                {
                    interactIcon.sprite = noneInteractSprite;
                    
                }
            }
        }


    }
}
