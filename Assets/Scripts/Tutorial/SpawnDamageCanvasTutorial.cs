using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageCanvasTutorial : MonoBehaviour
{
    [SerializeField] HitBoxScriptTutorial hitBoxScript;
    [SerializeField] GameObject powTextDefense;
    [SerializeField] GameObject powTextPerfectDefense;
    [SerializeField] GameObject powStunText;

    [Header("Enemy")]
    [SerializeField] GameObject powText;


    [Header("Player")]
    [SerializeField] GameObject hitCanvas;
    [SerializeField] Transform canvasSpawn;
    CameraShake cameraShake;

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    public void SpawnPowLeftSide()
    {
        GameObject powTextClone = Instantiate(powText, hitBoxScript.leftBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }
    public void SpawnPowRightSide()
    {
        GameObject powTextClone = Instantiate(powText, hitBoxScript.rightBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowDefenseLeftSide()
    {
        GameObject powTextClone = Instantiate(powTextDefense, hitBoxScript.leftBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }
    public void SpawnPowDefenseRightSide()
    {
        GameObject powTextClone = Instantiate(powTextDefense, hitBoxScript.rightBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowStunLeftSide()
    {
        GameObject powTextClone = Instantiate(powStunText, hitBoxScript.leftBoxCollider.transform);
        //powTextClone.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowStunRightSide()
    {
        GameObject powTextClone = Instantiate(powStunText, hitBoxScript.rightBoxCollider.transform);
        //powTextClone.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowPerfectDefenseLeftSide()
    {
        GameObject powTextClone = Instantiate(powTextPerfectDefense, hitBoxScript.leftBoxCollider.transform);
        powTextClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        print("efeti");
        powTextClone.transform.parent = null;
        //Destroy(powTextClone.gameObject, 0.5f);

    }

    public void SpawnPowPerfectDefenseRightSide()
    {
        GameObject powTextClone = Instantiate(powTextPerfectDefense, hitBoxScript.rightBoxCollider.transform);
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
