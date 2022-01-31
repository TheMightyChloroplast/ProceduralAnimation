using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedMovement : MonoBehaviour
{
    // www.youtube.com/watch?v=AChwSWU4AaU
    public Transform leftTarget;
    public Transform rightTarget;

    private Vector3 _initLeftFootPos;
    private Vector3 _initRightFootPos;

    private Vector3 _lastLeftFootPos;
    private Vector3 _lastRightFootPos;

    public float movementSpeed = 1f;
    public float stepHeight = 1;
    private float _timeStep = 0f;
    private Vector3 _nextLeftFootPos;
    private Vector3 _nextRightFootPos;

    private Vector3 _currentLeftFootPos;
    private Vector3 _currentRightFootPos;

    public float _footSpacing; //stores how wide a gap in between each step

    //stores positions for the body
    private Vector3 _lastBodyPosition;
    private Vector3 _currentBodyPosition;
    private Vector3 _nextBodyPosition;
   
    void Start()
    {
        _initLeftFootPos = leftTarget.localPosition;
        _initRightFootPos = rightTarget.localPosition;

        //ensures the steps are offset from each other to avoid clashing
        _lastLeftFootPos = leftTarget.position;
        _lastRightFootPos = rightTarget.position;

        _nextLeftFootPos = _lastLeftFootPos + (transform.forward * (_footSpacing / 2));
        _nextRightFootPos = _lastRightFootPos + (transform.forward * _footSpacing);

        //initialises the body variables. 
        _lastBodyPosition = transform.position;
        _nextBodyPosition = _lastBodyPosition + transform.forward * _footSpacing;

    }
        
    
    void Update()
    { 

        leftTarget.position = _currentLeftFootPos;
        rightTarget.position = _currentRightFootPos;

        //gets the midway points between the two legs and sets the y position back to where it should be 
        Vector3 pos = (_currentLeftFootPos + _currentRightFootPos) / 2;
        pos.y = transform.position.y;
        transform.position = pos;

        if(_timeStep < 1) {
            // Moves the left leg first
            _currentLeftFootPos = Vector3.Lerp(_lastLeftFootPos, _nextLeftFootPos, _timeStep);
            _currentLeftFootPos.y += Mathf.Sin(Mathf.PI * _timeStep) * stepHeight;
    
            _timeStep += Time.deltaTime * movementSpeed;
        } else if(_timeStep < 2) {
            // Moves the right leg next
            _currentRightFootPos = Vector3.Lerp(_lastRightFootPos, _nextRightFootPos, (_timeStep - 1));
            _currentRightFootPos.y += Mathf.Sin(Mathf.PI * (_timeStep - 1)) * stepHeight;

            _timeStep += Time.deltaTime * movementSpeed;
        } else
        {
            _lastLeftFootPos = _nextLeftFootPos;
            _lastRightFootPos = _nextRightFootPos;
            _timeStep = 0; // Starts it over again
            _nextLeftFootPos = _lastLeftFootPos + (transform.forward * _footSpacing);
            _nextRightFootPos = _lastRightFootPos + (transform.forward * _footSpacing);
        }
       

    }
}
