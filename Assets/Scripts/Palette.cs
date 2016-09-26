using UnityEngine;
using System.Collections.Generic;

public class Palette : MonoBehaviour
{
	const float FEEDBACK_COL_WIDTH = 0; // todo: replace me in feedback code
	const float CONCEPT_WIDTH = 36; // todo: look me up in the concept prefab
	const float CONCEPT_HEIGHT = 8; // todo: look me up in the concept prefab

	[SerializeField]
	float HorizontalPadding = 5;
	
    [SerializeField]
	float VerticalPadding = 5;

	float DesktopWidth;
	
	int MaxConceptsPerRow;
	int NumberOfRows;

	public List<Transform> ConceptTransforms;

	/// Arrange the concepts at the bottom of the screen.
	public void Arrange ()
	{
		ConceptTransforms.Shuffle();
		CalculateArrangement();
		Place(ConceptTransforms);
	}

	/// Calculate how many rows there will be, and how many concepts per full row.
	void CalculateArrangement ()
	{
		DesktopWidth = Screen.width - FEEDBACK_COL_WIDTH;
		MaxConceptsPerRow = ConceptTransforms.Count;
		NumberOfRows = 1;
		while (MaxConceptsPerRow * (CONCEPT_WIDTH + HorizontalPadding) - HorizontalPadding > DesktopWidth)
		{
			MaxConceptsPerRow -= (MaxConceptsPerRow / 2);
			NumberOfRows++;
		}
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
				rowConceptTransforms.Add(conceptTransforms[0]);
				conceptTransforms.RemoveAt(0);
			}

			// make the new row with this list of transforms
			Transform newRow = CreateRow(rowConceptTransforms, r);

			// position this row on the screen
			PlaceRow(newRow, r);
		}
	}

	/// Create a row containing the given transforms.
	Transform CreateRow (List<Transform> rowConceptTransforms, int rowNumber)
	{
		// create the row gameobject and make it a child of the palette
		Transform row = new GameObject(name = "Row " + rowNumber.ToString()).transform;
		row.parent = this.gameObject.transform;

/*
		// for each transform in this row's list...
		for (int t = 0; t < rowConceptTransforms.Count; t++)
		{
			// set that transform's parent to the row
			rowConceptTransforms[t].parent = row;

			// position the transform within the row
			// todo: get row heights/composition, then fix this
//			rowConceptTransforms[t].localPosition = Vector3.left * (CONCEPT_WIDTH + HorizontalPadding) / 2f; // todo: too much horizontal padding
		}
*/

		foreach (Transform t in rowConceptTransforms)
		{
			t.parent = row;
		}

		return row;
	}

	/// Position a row on the screen according to its row number.
	void PlaceRow (Transform rowTransform, int rowNumber)
	{
		// calculate how far up to place the row
		float verticalOffset = rowNumber * (CONCEPT_HEIGHT + VerticalPadding);

		// increase the row's y-component by the above
		rowTransform.position += Vector3.up * verticalOffset;

		// if the row has an odd number of elements, center it on the screen
		// todo: worry about this later
//		if (rowTransform.childCount % 2 != 0)
//		{
//			float oddOffset = (CONCEPT_WIDTH + HorizontalPadding) / 2f;
//			rowTransform.position += Vector3.right * oddOffset;
//		}
	}
}

