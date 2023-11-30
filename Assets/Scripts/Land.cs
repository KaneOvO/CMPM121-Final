using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class Land : MonoBehaviour
{
    public bool isPanted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isPanted) return;
        if(other.gameObject.tag == "SeedPack")
        {
            string seedType = other.gameObject.GetComponent<SeedPack>().seedType;
            Planting(seedType);
            isPanted = true;
        }
    }

    void Planting(string seedType)
    {
        Instantiate(Resources.Load($"Prefabs/Plant/{seedType}"), transform.position, Quaternion.identity, transform);
        Debug.Log("Planting " + seedType);
    }
}
