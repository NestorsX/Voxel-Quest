using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
	[Header("General")]
	public float sensitivity = 2; // чувствительность мышки
	public float distance = 7; // расстояние между камерой и игроком
	public float height = 3.5f; // высота

	[Header("Clamp Angle")]
	public float minY = 0f; // ограничение углов при наклоне
	public float maxY = 0f;

	[Header("Other")]
	public float speed = 8; // скорость сглаживания

	private float rotationY;

	public Transform player;
	public Transform Head;

	public Transform targetLook;
	public Transform targetLookStart;
	public GameObject targetPoint;


	void Start()
	{
		gameObject.tag = "MainCamera";
		Physics.gravity = new Vector3(0, -10.0F, 0);
	}

	// проверяем, если есть на пути луча, от игрока до камеры, какое-либо препятствие (коллайдер)
	Vector3 PositionCorrection(Vector3 player, Vector3 cam)
	{
		RaycastHit hit;
		if(Physics.Linecast(player, cam, out hit)) 
		{
			if(Arrow.Targets.Any(str => hit.transform.name.Contains(str)))
			{
				float tempDistance = Vector3.Distance(player, hit.point) / 2f;
				Vector3 pos = player - (transform.rotation * Vector3.forward * tempDistance);
				cam = new Vector3(pos.x, cam.y, pos.z); // сдвиг позиции в точку контакта
			}
		}
		return cam;
	}

	void LateUpdate()
	{
		if(!PauseMenu.GameIsPaused)
		{
			if(player)
			{
				targetPoint.SetActive(false);

				// вращение камеры вокруг игрока
				transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X") * sensitivity);

				// определяем точку на указанной дистанции от игрока
				Vector3 position = player.position - (transform.rotation * Vector3.forward * distance);
				position = position + (transform.rotation * Vector3.right); // сдвиг по горизонтали
				position = new Vector3(position.x, player.position.y + height, position.z); // корректировка высоты
				position = PositionCorrection(player.position, position); // находим текущую позицию, относительно игрока

				// поворот камеры по оси Y
				rotationY += Input.GetAxis("Mouse Y") * sensitivity;
				rotationY = Mathf.Clamp(rotationY, -Mathf.Abs(minY), Mathf.Abs(maxY));
				transform.localEulerAngles = new Vector3(rotationY * -1, transform.localEulerAngles.y, 0);

				transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);

				if(PlayerController.isAiming)
				{
					transform.position = new Vector3(Head.transform.position.x, Head.transform.position.y + 0.15f, Head.transform.position.z) - (transform.rotation * Vector3.forward * -0.12f);
					targetPoint.SetActive(true);
					float horizontal = Input.GetAxis("Mouse X") * sensitivity;
					player.Rotate(0, horizontal, 0);
					
					Ray ray = new Ray(transform.position, transform.forward);
					Debug.DrawRay(transform.position, transform.forward*30f, Color.blue);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
					{
						targetLook.position = hit.point;
					}
					else
					{
						targetLook.position = targetLookStart.position;
					}
					targetPoint.transform.localScale = new Vector3(targetLook.localPosition.z/100f,targetLook.localPosition.z/100f,targetLook.localPosition.z/100f);
					player.LookAt(targetLookStart);
				}
			}
		}
	}
}