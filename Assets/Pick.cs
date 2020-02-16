﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    bool carrying;
    public Transform theDest;

    bool ableToAcquireAbility;
    bool ableToGrabandHold;
    bool ableToChopTree;
    bool ableToBreakRocks;

    public void UpdatePickAbility(ManipulationSate state, bool value)
    {
        switch (state)
        {
            case ManipulationSate.BreakRock:
                ableToBreakRocks = value;
                break;
            case ManipulationSate.ChopTree:
                ableToChopTree = value;
                break;
            case ManipulationSate.Pickup:
                ableToAcquireAbility = value;
                break;
            case ManipulationSate.PickupAndHold:
                ableToGrabandHold = value;
                break;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other){
        if(other.tag == "item")
        {
            if (carrying == false)
            {
                if (Input.GetKeyDown("space"))
                {
                    pickup(other.gameObject);
                    carrying = true;
                }
            }
            else if (carrying == true)
            {
                if (Input.GetKeyDown("space"))
                {
                    putdown(other.gameObject);
                    carrying = false;
                }
            }
         }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(ableToAcquireAbility && other.tag == "ability")
        {
            if(carrying == false)
            {
                Destroy(other.gameObject);
            }
        }

        if(ableToGrabandHold && other.tag == "diaper")
        {
            if (carrying == false)
            {
                Destroy(other.gameObject);
            }
        }

        if (ableToBreakRocks && other.tag == "rock")
        {

            Destroy(other.gameObject);
            
        }

        if(ableToChopTree && other.tag == "tree")
        {

            Destroy(other.gameObject);
            
        }
    }

    void pickup(GameObject o)
    {
      o.GetComponent<Rigidbody>().useGravity = false;
      o.transform.position = theDest.position;
      o.transform.parent = this.transform;
    }

    void putdown(GameObject o)
    {
      o.transform.parent = null;
      o.GetComponent<Rigidbody>().useGravity = true;
    }




}
