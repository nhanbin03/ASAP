using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private bool isChanneling = false;
    private SpriteRenderer sprite;

    public bool IsChanneling { get => isChanneling; set => isChanneling = value; }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            isChanneling = true;
        } else {
            isChanneling = false;
        }
        if (isChanneling) {
            sprite.color = Color.blue;
        } else {
            sprite.color = Color.red;
        }
    }
}
