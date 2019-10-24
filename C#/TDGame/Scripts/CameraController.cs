
using UnityEngine;

public class CameraController : MonoBehaviour {

    private bool doMovement = true;

    public float panSpeed = 30f;
    public float panBoarderThickness = 10f;
    public float minPanX = 0f;
    public float maxPanX = 76f;
    public float minPanZ = -110f;
    public float maxPanZ = 0f;

    public float scrollSpeed = 5f;
    public float minY =10f;
    public float maxY =80f;

    // Update is called once per frame
    void Update () {

        if (GameManager.gameIsOver) {
            this.enabled = false;
            return;
        }


        if (Input.GetKeyDown("y")) {
            doMovement = !doMovement;
        }

        if (!doMovement)
            return;

        if (Input.GetKey("w")|| Input.mousePosition.y >= Screen.height - panBoarderThickness) {
            if (transform.position.z + panSpeed * Time.deltaTime > maxPanZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxPanZ);
                return;
            }
            transform.Translate(Vector3.forward*panSpeed*Time.deltaTime,Space.World);
        }

        if (Input.GetKey("s")|| Input.mousePosition.y <= panBoarderThickness)
        {
            if (transform.position.z - panSpeed * Time.deltaTime < minPanZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minPanZ);
                return;
            }
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
        {

            if (transform.position.x - panSpeed * Time.deltaTime < minPanX)
            {
                transform.position = new Vector3(minPanX, transform.position.y, transform.position.z);
                return;
            }
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {

            if (transform.position.x + panSpeed * Time.deltaTime > maxPanX)
            {
                transform.position = new Vector3(maxPanX, transform.position.y, transform.position.z);
                return;
            }
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * scrollSpeed * Time.deltaTime * 1000;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }
}
