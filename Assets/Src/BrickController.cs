using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BrickController : MonoBehaviourSingleton<BrickController>
{
    public Transform root;

    // Start is called before the first frame update
    protected override void Start()
    {
        StartCoroutine(test());
    }

    public IEnumerator test(){
        yield return null;
        GameObject o_brick_group = Instantiate(BrickDef.Instance._brick_group_prefab , root);
        BrickGroup brick_group = o_brick_group.GetComponent<BrickGroup>();
        brick_group.Init(BrickDef.Instance.GetBrickDef("L1"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
