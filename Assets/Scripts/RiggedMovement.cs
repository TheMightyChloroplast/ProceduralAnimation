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

    private float _timeStep = 0f;
    private Vector3 _nextLeftFootPos;
    private Vector3 _nextRightFootPos;

    private Vector3 _currentLeftFootPos;
    private Vector3 _currentRightFootPos;

    public float _footSpacing; //stores how wide a gap in between each step
    
    void Start()
    {
        _initLeftFootPos = leftTarget.localPosition;
        _initRightFootPos = rightTarget.localPosition;

        _lastLeftFootPos = leftTarget.position;
        _lastRightFootPos = rightTarget.position;
        
        _nextLeftFootPos = _lastLeftFootPos + (transform.forward * _footSpacing);
        _nextRightFootPos = _lastRightFootPos + (transform.forward * _footSpacing); 

    }
        
    
    void Update()
    {
        leftTarget.position = _currentLeftFootPos;
        rightTarget.position = _currentRightFootPos; 

        if (_timeStep < 1)//_timeStep make it so it takes a second between each step.
        {
            
            _currentLeftFootPos =  Vector3.Lerp(_lastLeftFootPos, _nextLeftFootPos, _timeStep); 
            _currentRightFootPos = Vector3.Lerp(_lastRightFootPos, _nextRightFootPos, _timeStep);

            _timeStep += Time.deltaTime;

        }
        else
        {
            _timeStep = 0;
            //calculates where the next position is going to be.
            _lastLeftFootPos = _nextLeftFootPos;
            _lastRightFootPos = _nextRightFootPos;
            
            _nextLeftFootPos = _lastLeftFootPos + (transform.forward * _footSpacing);
            _nextRightFootPos = _lastRightFootPos + (transform.forward * _footSpacing); 
        }

    }
}
