using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HorrorGame
{
    public class CreateDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        RectTransform rf;

        Vector3 newPosition;

        public RectTransform jigsawRF;
        RectTransform cellRF;

        CanvasGroup cg;

        bool isDrag;

        GameObject enterGameObject;

        void Awake() {
            rf = GetComponent<RectTransform>();
            cg = GetComponent<CanvasGroup>();
           
        }
        void Start() {
           
        }

        public void OnBeginDrag(PointerEventData eventData) {
            cellRF = rf.parent.GetComponent<RectTransform>();
            rf.SetParent(jigsawRF);
            cg.blocksRaycasts = false;
            isDrag = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDrag) {
                RectTransformUtility.ScreenPointToWorldPointInRectangle(rf, eventData.position, eventData.enterEventCamera, out newPosition);
                rf.position = newPosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (isDrag) {
                enterGameObject = eventData.pointerEnter;

                if (enterGameObject == null)
                {
                    MySetParent(rf, cellRF);
                }
                else {
                    switch (enterGameObject.tag) {
                        case "cell":
                            MySetParent(rf, enterGameObject.transform);
                            break;
                        case "fragment":
                            MySetParent(rf, enterGameObject.transform.parent);
                            MySetParent(enterGameObject.transform, cellRF);
                            break;
                        default:
                            MySetParent(rf, cellRF);
                            break;
                    }
                }
                cg.blocksRaycasts = true;
                isDrag = false;
            }
            if (CreateImage.instance.IsFinished()) {
                CreateImage.instance.PuzzleSolved();
            }
        }

        void MySetParent(Transform son, Transform parent) {
            son.SetParent(parent);
            son.localPosition = Vector3.zero;
        }


    }
}
