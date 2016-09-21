using UnityEngine;

public class CameraController : MonoBehaviour
{
	enum MouseButton { Primary, Secondary };

	[Header("Zoom")]
	[SerializeField] float Min = 50f;
	[SerializeField] float Max = 200f;
	[SerializeField] float ZoomSensitivity = 1f;
	[SerializeField] bool Invert = false;

	[Header("Scroll")]
	[SerializeField] float ScrollSensitivity = 0.5f;
	[SerializeField] MouseButton button;

	float size;

	void Awake ()
	{
		size = GetComponent<Camera>().orthographicSize;
	}
     
	void LateUpdate ()
	{
		Zoom();
		Scroll();
	}

	void Zoom ()
	{
		float scrollInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollInput != 0)
		{
			size -= scrollInput * ZoomSensitivity * (Invert ? -1 : 1);
			size = Mathf.Clamp(size, Min, Max);
			GetComponent<Camera>().orthographicSize = size;
		}
	}

	Vector3 currentMousePosition;
	Vector3 lastMousePosition;
	void Scroll ()
	{
		currentMousePosition = Input.mousePosition;
		if (Input.GetMouseButton((int)button))
		{
			Vector3 delta = currentMousePosition - lastMousePosition;
			transform.position -= new Vector3(delta.x, delta.y, 0f) * ScrollSensitivity;
		}
		lastMousePosition = currentMousePosition;
	}
}
