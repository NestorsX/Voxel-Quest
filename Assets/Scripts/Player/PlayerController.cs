using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public static float speed = 3f; // макс. скорость

	public Transform rotate; // объект вращения (локальный)
	private Vector3 direction;
	private float h, v;
	private int layerMask;
	private Rigidbody body;

	//стрельба из лука
	private Animator anim;

	public Transform targetLook;
	public GameObject arrowInHand;
	public GameObject arrowPrefab;
	public Transform LeftHand;
	
	public static bool isAiming;

	public static AudioSource audio;
	public AudioClip Bow;
	public AudioClip Step;

	void Start () { 
        anim = GetComponent<Animator>();
		audio = GameObject.Find("/Camera/SoundController").GetComponent<AudioSource>();
    }
	
	void Awake()
	{
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		gameObject.tag = "Player";

		// объекту должен быть присвоен отдельный слой, для работы прыжка
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;
	}
	
	void FixedUpdate()
	{
		Debug.DrawLine(Camera.main.transform.position, targetLook.position, Color.black);
		
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) // движение вперед
		{
			anim.SetBool("isWalking", true);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // движение влево
		{
			anim.SetBool("isWalking", true);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // движение вправо
		{
			anim.SetBool("isWalking", true);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // движение назад
		{
			anim.SetBool("isWalking", true);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		else
        {
            anim.SetBool("isWalking", false);
        }

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("continuous_aiming"))
		{
			Debug.Log("OK");
		}
	}

	bool IsGrounded()
	{
		RaycastHit hit;
		float raycastDistance = 0.02f;
		int mask = 1 << LayerMask.NameToLayer("Ground");

		if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, mask))
		{
			return true;
		}
		return false;
	}

	void Update()
	{
		Debug.DrawLine(new Vector3(LeftHand.position.x, LeftHand.position.y, LeftHand.position.z),
		new Vector3(targetLook.position.x, targetLook.position.y, targetLook.position.z), Color.blue);

		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		// вектор направления движения
		direction = new Vector3(h, 0, v);
		direction = Camera.main.transform.TransformDirection(direction);
		direction = new Vector3(direction.x, 0, direction.z);

		if(Mathf.Abs(v) > 0 || Mathf.Abs(h) > 0) // разворот тела по вектору движения
		{
			rotate.rotation = Quaternion.Lerp(rotate.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed*=2.5f;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed/=2.5f;
            anim.SetBool("isRunning", false);
        }

		if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
		{
			body.AddForce(0, 270, 0);
			if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetTrigger("Jump_when_run");
            }
            else
            {
                anim.SetTrigger("Jump");
            }
		}

		if(Input.GetMouseButtonDown(1))
		{
			anim.SetBool("isAiming", true);
			isAiming = true;
			arrowInHand.SetActive(true);
			speed/=2;
		}
		if(Input.GetMouseButtonUp(1))
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("continuous_aiming") || 
			anim.GetCurrentAnimatorStateInfo(0).IsName("walk_when_aim"))
			{
				audio.PlayOneShot(Bow, 0.5f);
				Instantiate(arrowPrefab, arrowInHand.transform.position, transform.rotation);
			}
			arrowInHand.SetActive(false);
			anim.SetBool("isAiming", false);
			speed*=2;
			isAiming = false;
		}
	}

	public static void PlayHitSound(AudioClip hitSound)
	{
		audio.PlayOneShot(hitSound, 0.5f);
	}

	void StepSound()
	{
		audio.PlayOneShot(Step);
	}
}