using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public Rigidbody2D agentbody;
    public GameObject door;
    public GameObject openDoor;
    // Start is called before the first frame update
    void Start()
    {
        openDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            agentbody.MovePosition(new Vector2(agentbody.position.x, agentbody.position.y + 0.1f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            agentbody.MovePosition(new Vector2(agentbody.position.x, agentbody.position.y - 0.1f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            agentbody.MovePosition(new Vector2(agentbody.position.x -0.1f, agentbody.position.y));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            agentbody.MovePosition(new Vector2(agentbody.position.x + 0.1f, agentbody.position.y));
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            door.SetActive(false);
            openDoor.SetActive(true);
        }
    }
}
