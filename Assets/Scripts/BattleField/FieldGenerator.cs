using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField]
    //public List<MapTile>[,] fieldmaplist = new List<MapTile>[12,6];
    private MapField mapfields;

    private Tile maketilescript;
    
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
                Vector3 tilescale= new Vector3(1,1,1);
                Maketile.transform.localScale=tilescale; 
                Maketile.transform.position = new Vector3(((float)x)/2.5f,((float)y)/2.5f,y);//*1f
                maketilescript = Maketile.GetComponent<Tile>();
                maketilescript.thisx = x;
                maketilescript.thisy = y;
                
                
                
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
