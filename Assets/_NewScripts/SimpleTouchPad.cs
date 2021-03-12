using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched;
	private int pointerID;
	private Image jsContainer;
	private Image joystick;

	void Awake () {
		jsContainer = GetComponent<Image>();
		joystick = transform.GetChild(0).GetComponent<Image>(); 
		direction = Vector2.zero;
		touched = false;
	}

	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
			OnDrag(data);
		}
	}

	public void OnDrag (PointerEventData ped) {
		if (ped.pointerId == pointerID) {
			Vector2 position = ped.position;
			Vector2 directionRaw = position - origin;
			direction = directionRaw.normalized;
			RectTransformUtility.ScreenPointToLocalPointInRectangle
			(jsContainer.rectTransform, 
				ped.position,
				ped.pressEventCamera,
				out position);

			position.x = (position.x/jsContainer.rectTransform.sizeDelta.x);
			position.y = (position.y/jsContainer.rectTransform.sizeDelta.y);

			float x = (jsContainer.rectTransform.pivot.x == 1f) ? position.x *2 + 1 : position.x *2 - 1;
			float y = (jsContainer.rectTransform.pivot.y == 1f) ? position.y *2 + 1 : position.y *2 - 1;


			//to define the area in which joystick can move around
			joystick.rectTransform.anchoredPosition = new Vector3 (direction.x * (jsContainer.rectTransform.sizeDelta.x/3)
				,direction.y * (jsContainer.rectTransform.sizeDelta.y)/3);
			
		}
	}

	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			direction = Vector2.zero;
			joystick.rectTransform.anchoredPosition = Vector3.zero;
			touched = false;
		}
	}

	public Vector2 GetDirection () {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}