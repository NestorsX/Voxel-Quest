using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    bool isNewPointSpawned = false;

    public GameObject wayPointPrefab;
    GameObject prevWayPoint;
    GameObject wayPoint;

    public GameObject AnimalContol;

    Animator anim;
    public float speed = 1f;

    bool isPatrol = true;

    public float timer;

    void Start()
    {
        StartCoroutine(spawnNewWayPoint());
        prevWayPoint = wayPoint;
    }

    void FixedUpdate()
    {
        if(isPatrol)
            Patrol();
        else
            Chill();
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    void Patrol()
    {
        Vector3 direction = (wayPoint.transform.position - AnimalContol.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        AnimalContol.transform.rotation = Quaternion.Slerp(AnimalContol.transform.rotation, lookRotation, Time.deltaTime * 10);
        float distance = Vector3.Distance(AnimalContol.transform.position, wayPoint.transform.position);
        if (distance >= 1f)
        {
            AnimalContol.transform.position += AnimalContol.transform.forward * speed * Time.deltaTime;
        }
        else
        {
            isPatrol = false;
            timer = Random.Range(4f, 8f);
        }
    }

    void Chill()
    {
        if(prevWayPoint!=null)
        {
            Destroy(prevWayPoint);
            isNewPointSpawned = false;
        }
        if(timer>0)
        {
            speed = 0;
            if(prevWayPoint==null && !isNewPointSpawned)
            {
                StartCoroutine(spawnNewWayPoint());
            }
        }
        else
        {
            prevWayPoint = wayPoint;
            speed = 1f;
            isPatrol = true;
        }
    }

    bool isObjectHere(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, 10f);
        if (intersecting.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator spawnNewWayPoint () {
        isNewPointSpawned = true;
        float xPos;
        float zPos;
        do{
            xPos = Random.Range(-10f, 10f); // рандомная координата по X
            zPos = Random.Range(-10f, 10f); // рандомная координата по Y
        }
        while(!isObjectHere(new Vector3(xPos, 1f, zPos)));

        wayPoint = Instantiate(wayPointPrefab, new Vector3(0f, 0.4f, 0f), Quaternion.identity);
        wayPoint.transform.SetParent(gameObject.transform);
        wayPoint.transform.localPosition = new Vector3(xPos, 0.4f, zPos);

        yield return new WaitForSeconds(0); // задержка - 0
    }
}
