using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField]
    //public List<MapTile>[,] fieldmaplist = new List<MapTile>[12,6];
    private MapField mapfields;
    
    [SerializeField]
    private GameObject tilePrefab;
    private MapTile[,] maptiles= new MapTile[12,24];
    // Start is called before the first frame update

    private void Makefield(MapField mf){
        maptiles= mf.GetMaptiles();
        for(int x=0;x<24;x++)
        {
            for(int y=0;y<12;y++){
                
                GameObject Maketile = Instantiate(tilePrefab);
                Maketile.GetComponent<SpriteRenderer>().sprite = maptiles[y,x].tileImage;
                Maketile.transform.position = new Vector3(x,y,0);
            }
        }
    }

    void Start()
    {
        Makefield(mapfields);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
