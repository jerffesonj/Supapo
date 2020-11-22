using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageCanvasFeedback : MonoBehaviour
{
    public HitBoxScript hitBoxScript;
    public GameObject powTextDefense;
    public GameObject powTextPerfectDefense;
    public GameObject powStunText;

    [Header ("Enemy")]
    public GameObject powText;
    

    [Header ("Player")]
    public GameObject hitCanvas;
    public Transform canvasSpawn;
    public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SpawnPowLeftSide()
    {
        GameObject powTextClone = Instantiate(powText, hitBoxScript.LeftBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);
        
    }
    public void SpawnPowRightSide()
    {
        GameObject powTextClone = Instantiate(powText, hitBoxScript.RightBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowDefenseLeftSide()
    {
        GameObject powTextClone = Instantiate(powTextDefense, hitBoxScript.LeftBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }
    public void SpawnPowDefenseRightSide()
    {
        GameObject powTextClone = Instantiate(powTextDefense, hitBoxScript.RightBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowStunLeftSide()
    {
        GameObject powTextClone = Instantiate(powStunText, hitBoxScript.LeftBoxCollider.transform);
        //powTextClone.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowStunRightSide()
    {
        GameObject powTextClone = Instantiate(powStunText, hitBoxScript.RightBoxCollider.transform);
        //powTextClone.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowPerfectDefenseLeftSide()
    {
        GameObject powTextClone = Instantiate(powTextPerfectDefense, hitBoxScript.LeftBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        print("efeti");
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowPerfectDefenseRightSide()
    {
        GameObject powTextClone = Instantiate(powTextPerfectDefense, hitBoxScript.RightBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        print("efeti");
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void CameraShake(float value)
    {
        cameraShake.ShakeDuration = value;
    }
}
