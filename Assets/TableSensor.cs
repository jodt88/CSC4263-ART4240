using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSensor : MonoBehaviour {

    public bool byTable = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Barkeeper")
        {
            setByTable(true);
            //other.gameObject.GetComponent<Interact>().setSensorObject(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Barkeeper")
        {
            setByTable(false);
           //other.gameObject.GetComponent<Interact>().setSensorObject(null);
        }
    }

    void setByTable(bool b)
    {
        byTable = b;
    }

    public bool getByTable()
    {
        return byTable;
    }
}
