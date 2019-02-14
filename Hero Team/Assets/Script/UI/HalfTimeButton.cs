using UnityEngine;

public class HalfTimeButton : MonoBehaviour {
    private HalfTime halftime;
    [SerializeField] private AudioSource Tap;
	private void Start ()
    {
        halftime = transform.root.gameObject.GetComponent<HalfTime>();
	}
	
    public void OnClick()
    {
        Tap.Play();
        halftime.OnClick(gameObject);
    }
}
