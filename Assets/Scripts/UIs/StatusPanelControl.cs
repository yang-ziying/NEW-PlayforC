using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using naichilab; //POLYGONを使うときに必須'
using UnityEngine.UI;


public class StatusPanelControl : MonoBehaviour
{
    int now_idx;
    [SerializeField]
    Sprite[] images= new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
        int now_idx =0;
        
        ShowStatus(now_idx);
    }
    

    void ShowStatus(int idx)
    {
        UnitInfo allunitinfo= UnitInfo.CreateFromSaveData();
        //idxにキャラが存在するかチェック いない場合は＋１を召喚して自身の後の操作を全部消す
        if(!allunitinfo.isUsing[idx])
        {
            ShowStatus(idx+1);
            return;
        }


        //ボタン表示非表示のコントロール（途中途切れIDのために確実にした）
        GameObject backbutton = transform.Find("BackButton").gameObject;
        GameObject forwardbutton = transform.Find("ForwardButton").gameObject;
        int backcheck = 0;
        int frontcheck = 0;

        for(int i=0;i<idx;i++)
        {
            if(allunitinfo.isUsing[i]) backcheck++;
        } 
        if (backcheck==0) backbutton.SetActive(false);
        else backbutton.SetActive(true);
        for(int i=idx+1;i<100;i++)
        {
            if(allunitinfo.isUsing[i]) frontcheck++;
        }
        if (frontcheck ==0 ) forwardbutton.SetActive(false);
        else forwardbutton.SetActive(true); 

        //テキストエリア表示
        GameObject nameplace = transform.Find("NameText").gameObject;
        Text nametext = nameplace.GetComponent<Text> ();
        nametext.text= allunitinfo.u_name[idx];

        //キャラ画像変更
        GameObject charaimage = transform.Find("CharaImage").gameObject;
        Image cimage = charaimage.GetComponent<Image>();
        cimage.sprite=images[allunitinfo.u_job[idx]];

        

        //チャートの変更
        GameObject child = transform.Find("RadarChartPolygonUGUI").gameObject;
        RadarChartPolygonUGUI polygon = child.GetComponent<RadarChartPolygonUGUI>();
        
        float physic_type = (allunitinfo.u_vit[idx]+allunitinfo.u_str[idx])/16f;
        float skilled_type = (allunitinfo.u_agi[idx]+allunitinfo.u_dex[idx])/16f;
        float magic_type = (allunitinfo.u_int[idx]+allunitinfo.u_wis[idx]+allunitinfo.u_men[idx])/24f;
        float mov = (allunitinfo.u_mov[idx]-2)/3f;
        float luk = allunitinfo.u_luk[idx]/8f;

        polygon.SetVolume(0,physic_type);
        polygon.SetVolume(1,magic_type);
        polygon.SetVolume(2,luk);
        polygon.SetVolume(3,mov);
        polygon.SetVolume(4,skilled_type);

        child.GetComponent<RadarChartPolygonUGUI>().enabled = false;
        child.GetComponent<RadarChartPolygonUGUI>().enabled = true;
        //チャート変更ここまで

        

    }
    public void PushForwardButton()
    {
        now_idx++;
        ShowStatus(now_idx);
    }
    public void PushBackButton()
    {
        now_idx--;
        ShowStatus(now_idx);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
