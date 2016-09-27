using UnityEngine;
using System.Collections.Generic;

public class Palette : MonoBehaviour
{
	const float FEEDBACK_COL_WIDTH = 0f; // todo: replace me in feedback code
	const float CONCEPT_WIDTH = 36f; // todo: look me up in the concept prefab
	const float CONCEPT_HEIGHT = 8f; // todo: look me up in the concept prefab

	[SerializeField]
	float HorizontalPadding = 5f;
	
    [SerializeField]
	float VerticalPadding = 5f;

	float DesktopWidth;
	float DesktopBottomCentre;
	float ConceptSpacing;
	
	int MaxConceptsPerRow;
	int NumberOfRows;

	public List<Transform> ConceptTransforms;

	/// Arrange the concepts at the bottom of the screen.
	public void Arrange ()
	{
		ConceptTransforms.Shuffle();
		CalculateArrangement();
		Place(ConceptTransforms);

		this.transform.position += Vector3.left * (DesktopWidth / 2f - ConceptSpacing / 2f);
		this.transform.position += Vector3.down * (Camera.main.orthographicSize - CONCEPT_HEIGHT / 2f - VerticalPadding);
	}

	/// Calculate how many rows there will be, and how many concepts per full row.
	void CalculateArrangement ()
	{
		DesktopWidth = (Camera.main.orthographicSize * 2f * Screen.width / Screen.height) - FEEDBACK_COL_WIDTH;
		MaxConceptsPerRow = ConceptTransforms.Count;
		NumberOfRows = 1;
		while (MaxConceptsPerRow * (CONCEPT_WIDTH + HorizontalPadding) - HorizontalPadding > DesktopWidth)
		{
			NumberOfRows++;
			MaxConceptsPerRow = (int) Mathf.Ceil((float) ConceptTransforms.Count / (float) NumberOfRows);
		}
		ConceptSpacing = DesktopWidth / MaxConceptsPerRow;
	}

	/// Place the concept transforms in rows on the palette.
	void Place (List<Transform> conceptTransforms)
	{
		// for each row we need...
		for (int r = 0; r < NumberOfRows; r++)
		{
			// make a list of transforms which will go in that row
			List<Transform> rowConceptTransforms = new List<Transform>();

			// add some transforms to the row and remove them from the master list
			for (int t = 0; t < MaxConceptsPerRow; t++)
			{
				if (conceptTransforms.Count > 0)
				{
					rowConceptTransforms.Add(conceptTransforms[0]);
					conceptTransforms.RemoveAt(0);
				}
			}

			// make the new row with this list of transforms
			Transform newRow = CreateRow(rowConceptTransforms, r);

			// position this row on the screen
			PositionRow(newRow, r);
		}
	}

	/// Create a row containing the given transforms.
	Transform CreateRow (List<Transform> rowConceptTransforms, int rowNumber)
	{
		// create and name the row gameobject
		GameObject rowGameObject = new GameObject();
		rowGameObject.name = "Row" + rowNumber.ToString();

		// make the row's transform a child of palette
		Transform rowTransform = rowGameObject.transform;
		rowTransform.SetParent(this.gameObject.transform);

		// for each transform in this row's list of concept transforms...
		for (int t = 0; t < rowConceptTransforms.Count; t++)
		{
			// set that transform's parent to the row
			rowConceptTransforms[t].SetParent(rowTransform);
		}

		foreach (Transform t in rowConceptTransforms)
		{
			t.SetParent(rowTransform);
		}

		ArrangeConceptsWithinRow(rowTransform);

		return rowTransform;
	}

	void ArrangeConceptsWithinRow(Transform row)
	{
		foreach (Transform t in row)
		{
			int index = t.GetSiblingIndex();
			t.localPosition += Vector3.right * ConceptSpacing * index;
		}
	}

	/// Position a row on the screen according to its row number.
	void PositionRow (Transform rowTransform, int rowNumber)
	{
		// calculate how far up to place the row
		float verticalOffset = rowNumber * (CONCEPT_HEIGHT + VerticalPadding);

		// increase the row's y-component by the above
		rowTransform.position += Vector3.up * verticalOffset;

		if (rowTransform.childCount < MaxConceptsPerRow)
		{
			rowTransform.position += Vector3.right * ConceptSpacing / 2f;
		}
	}
}

