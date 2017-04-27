using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSensor : MonoBehaviour
{
    public bool byBed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Barkeeper")
        {
            //other.gameObject.GetComponent<Interact>().setSensorObject(this.gameObject);
            setByBed(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Barkeeper")
        {
            //other.gameObject.GetComponent<Interact>().setSensorObject(null);
            setByBed(true);
        }
    }

    void setByBed(bool b)
    {
        byBed = b;
    }

    public bool getByBed()
    {
        return byBed;
    }
}
