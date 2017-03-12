using UnityEngine;
using UnityEngine.UI;

public class ScrollerController : MonoBehaviour {

    public GameObject scrollContent;

	void Update () {
        print(scrollContent.GetComponent<RectTransform>().position.y);
        if (scrollContent.GetComponent<RectTransform>().position.y < 5.3f) {
            GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
        }
        else {
            GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
        }
	}
}
