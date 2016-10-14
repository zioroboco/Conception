using System;
using UnityEngine;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{
	enum MouseButton { Primary, Secondary };
	
	[SerializeField]
	Palette palette;
	[SerializeField]
	Transform desktop;
	[SerializeField]
	ConceptMap map;
	[SerializeField]
	Feedback feedback;
	[SerializeField]
	Scoreboard scoreboard;

	[Header("Zoom")]
	[SerializeField] float Min = 50f;
	[SerializeField] float Max = 200f;
	[SerializeField] float ZoomSensitivity = 1f;
	[SerializeField] bool Invert = false;

	[Header("Scroll")]
	[SerializeField] float ScrollSensitivity = 0.5f;
	[SerializeField] MouseButton button;

	float size;

	List<LineRenderer> lines;

	void Awake ()
	{
		size = GetComponent<Camera>().orthographicSize;
		lines = new List<LineRenderer>();
	}
     
	void LateUpdate ()
	{
		Zoom();
		Scroll();
		Select();
		foreach (LineRenderer line in lines)
		{
			Transform parentTransform = line.GetComponent<SpringJoint2D>().connectedBody.transform;
			line.SetPosition(0, parentTransform.position);
			line.SetPosition(1, line.transform.position);
		}
	}

	void Zoom ()
	{
		float scrollInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollInput != 0)
		{
			size -= scrollInput * ZoomSensitivity * (Invert ? -1 : 1);
			size = Mathf.Clamp(size, Min, Max);
			GetComponent<Camera>().orthographicSize = size;
			
			this.palette.Reposition();
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
			if (map.root != null)
			{
				map.root.gameObject.transform.position += new Vector3(delta.x, delta.y, 0f) * ScrollSensitivity;
			}
		}
		lastMousePosition = currentMousePosition;
	}

	GameObject selected;
	void Select ()
	{
		if (Input.GetMouseButtonDown((int) button))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			if (hit.transform != null)
			{
				if (selected == null)
				{
					selected = hit.transform.gameObject;
					feedback.ResetFeedback();
					selected.GetComponent<Concept>().ApplyHighlight();
				}
				else
				{
					selected.GetComponent<Concept>().ClearHighlight();
					Link(selected, hit.transform.gameObject);
					selected = null;
				}
			}
		}
	}

    void Link(GameObject selected, GameObject hit)
    {
		Concept selectedConcept = selected.GetComponent<Concept>();
		Concept hitConcept = hit.GetComponent<Concept>();

		if (selectedConcept.parent ==  hitConcept)
		{
			selected.transform.parent = this.desktop;
			
			selected.GetComponent<Rigidbody2D>().isKinematic = false;
			selected.GetComponent<CircleCollider2D>().enabled = true;
			selected.GetComponent<PointEffector2D>().enabled = true;
			selected.GetComponent<SpringJoint2D>().enabled = true;
			selected.GetComponent<SpringJoint2D>().connectedBody = hit.GetComponent<Rigidbody2D>();
			LineRenderer line = selected.GetComponent<LineRenderer>();
			line.enabled = true;
			lines.Add(line);
			
			scoreboard.IncrementCorrect();
		}
		scoreboard.IncrementAttempts();
		scoreboard.UpdateScoreDisplay();
		
		feedback.DisplayFeedback(hitConcept, selectedConcept);
    }
}
