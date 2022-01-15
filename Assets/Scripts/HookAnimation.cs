using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAnimation : MonoBehaviour
{

    private Animation HookUpAndDown;
    private Animation Arch;
    // Start is called before the first frame update
    void Start()
    {
        HookUpAndDown = gameObject.GetComponent<Animation>();
        HookUpAndDown["spin"].layer = 123;
    }

    // Update is called once per frame
    void Update()
    {
        HookUpAndDown.Play("spin");
    }
}
