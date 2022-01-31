using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedMovement : MonoBehaviour
{
    // www.youtube.com/watch?v=AChwSWU4AaU
    
    //stores the leg foot targets
    public Transform leftTarget;
    public Transform rightTarget;

    //stores the last foot positions
    private Vector3 _lastLeftFootPos;
    private Vector3 _lastRightFootPos;

    //stores the character's movement speed;
    public float movementSpeed = 1f;
    //stores how high the leg is to move
    public float stepHeight = 1;
    //stores the animation frame the movement is going to be on. 
    private float _timeStep = 0f;
    
    //Stores the next foot positions;
    private Vector3 _nextLeftFootPos;
    private Vector3 _nextRightFootPos;

    //stores the current foot positions
    private Vector3 _currentLeftFootPos;
    private Vector3 _currentRightFootPos;

    //stores how wide a gap in between each step
    public float _footSpacing; 

    //stores positions for the body
    private Vector3 _lastBodyPosition;
    private Vector3 _currentBodyPosition;

    void Start()
    {     //initialises the current positions of the feet prior to start of animation.
        
        //ensures the steps are offset from each other to avoid clashing
        _lastLeftFootPos = leftTarget.position;
        _lastRightFootPos = rightTarget.position;

        //this calculates the next positions.
        _nextLeftFootPos = _lastLeftFootPos + transform.forward * (_footSpacing / 2); //left foot is half to offset feet and ensure different movement
        _nextRightFootPos = _lastRightFootPos + transform.forward * _footSpacing;

        //initialises the body variable.
        _lastBodyPosition = transform.position;
   

    }
        
    
    void Update()
    { 
        //updates the target positions to what was calculated in the previous frame
        leftTarget.position = _currentLeftFootPos;
        rightTarget.position = _currentRightFootPos;

        //gets the midway points between the two legs and sets the y position back to where it should be 
        Vector3 pos = (_currentLeftFootPos + _currentRightFootPos) / 2;
        pos.y = transform.position.y;
        transform.position = pos;

        //if the timestep is less than one, move the left leg.
        if(_timeStep < 1) {
            // Moves the left leg first
            _currentLeftFootPos = Vector3.Lerp(_lastLeftFootPos, _nextLeftFootPos, _timeStep); //lerp between the previous and the next position to get the current position
            _currentLeftFootPos.y += Mathf.Sin(Mathf.PI * _timeStep) * stepHeight;  //does a Sin function to get a curve of the step and applies it to the y axis.
    
            _timeStep += Time.deltaTime * movementSpeed;
            
            //if the _timestep is greater than one but less than 2 it will move the right leg. 
        } else if(_timeStep < 2) {
            // Moves the right leg next
            _currentRightFootPos = Vector3.Lerp(_lastRightFootPos, _nextRightFootPos, (_timeStep - 1)); //lerp between the previous and the next position to get the current position
            _currentRightFootPos.y += Mathf.Sin(Mathf.PI * (_timeStep - 1)) * stepHeight; //does a Sin function to get a curve of the step and applies it to the y axis.

            //increases the step offset
            _timeStep += Time.deltaTime * movementSpeed;
        } 
        // if timestep is greater than 2 it resets everything for the next cycle. 
        else
        {
            
            _lastLeftFootPos = _nextLeftFootPos;
            _lastRightFootPos = _nextRightFootPos;
            
            _timeStep = 0; // Starts it over again
            _nextLeftFootPos = _lastLeftFootPos + (transform.forward * _footSpacing);
            _nextRightFootPos = _lastRightFootPos + (transform.forward * _footSpacing);
        }
       
        
    }
}
