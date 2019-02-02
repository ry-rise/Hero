using UnityEngine;

public class HalfTimeButton : MonoBehaviour {
    private HalfTime halftime;

	private void Start ()
    {
        halftime = transform.root.gameObject.GetComponent<HalfTime>();
	}
	
    public void OnClick()
    {
        halftime.OnClick(gameObject);
    }
}
