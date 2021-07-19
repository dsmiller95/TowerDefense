using UnityEngine;


public class PlaceAtCursorComponent : MonoBehaviour
{
    public LayerMask validPlacementSurfaces;
    public GameObject enableWhenValidPlacement;

    private void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(!Physics.Raycast(ray, out var raycastHit, validPlacementSurfaces))
        {
            // there is no placement site under the cursor
            enableWhenValidPlacement.SetActive(false);
            return;
        }

        enableWhenValidPlacement.SetActive(true);
        this.transform.position = raycastHit.point;

        if (Input.GetMouseButton(0))
        {
            // on mouse click, anchor wherever you are. as long as the placement is valid.
            Destroy(this);
        }
    }
}
