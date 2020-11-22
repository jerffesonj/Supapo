using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] Transform beforeRoundPosition;
    [SerializeField] Transform loseRoundPosition;
    [SerializeField] Transform winRoundPosition;

    [SerializeField] Transform loseRoundStartPosition;
    [SerializeField] Transform winRoundStartPosition;

    public void MoveCamera(Vector3 targetLocation, float time)
    {
        iTween.MoveTo(this.gameObject, targetLocation, time);

        iTween.LookTo(this.gameObject, targetLocation, 0.1f);

        if (targetLocation == beforeRoundPosition.position)
        {

            this.gameObject.transform.parent = winRoundPosition.parent;
        }
    }

    public Transform BeforeRoundPosition { get { return beforeRoundPosition; } }
    public Transform LoseRoundPosition { get { return loseRoundPosition; } }
    public Transform WinRoundPosition { get { return winRoundPosition; } }
    public Transform LoseRoundStartPosition { get { return loseRoundStartPosition; } }
    public Transform WinRoundStartPosition { get { return winRoundStartPosition; } }
}
