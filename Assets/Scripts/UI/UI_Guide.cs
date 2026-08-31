using UnityEngine;

public class UI_Guide : MonoBehaviour
{
	[SerializeField] private GameObject childA;
	[SerializeField] private GameObject childB;

	private void Start()
	{
		if (childA != null)
			childA.SetActive(true);
		if (childB != null)
			childB.SetActive(false);
	}

	public void ToggleChildren()
	{
		if (childA == null || childB == null)
			return;

		bool aActive = childA.activeSelf;
		childA.SetActive(!aActive);
		childB.SetActive(aActive);
	}
}
