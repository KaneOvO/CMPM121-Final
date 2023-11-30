using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class Land : MonoBehaviour
{
    public bool isPanted = false;
    public GameObject panel;
    public float water;
    public float sun;

    // Start is called before the first frame update
    void Start()
    {
        panel = UIManager.Instance.GetPanel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPanted) return;
        if (other.gameObject.tag == "SeedPack")
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

    private void OnMouseDown()
    {
        //
        Debug.Log("Plant Selected");
        if (PlantManager.Instance.packSelected)
        {
            return;
        }
        panel.SetActive(true);
        UIManager.Instance.panel.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3((Input.mousePosition.y >= Screen.height * 2 / 3 ? -50 : 50), 0, 0);

    }
}
