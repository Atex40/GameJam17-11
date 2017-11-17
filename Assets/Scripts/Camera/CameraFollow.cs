using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private Player _playerBehaviour;
    private Camera _viewCamera;

	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerBehaviour = _player.GetComponent<Player>();
        _viewCamera = Camera.main;
	}
	
	void Update () {
        if (!_playerBehaviour.IsDead)
        {
            _viewCamera.transform.rotation = Quaternion.Euler(75, 0, 0);
            _viewCamera.transform.position = new Vector3(_player.position.x, 15, _player.position.z - 5);
        }
	}
}
