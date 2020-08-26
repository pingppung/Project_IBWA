using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInfo
{
    public static PlayerInfo instance;

    //PlayerData.json에 저장할 변수
    public string Name;
    public string ID;
    public string Password;


    public PlayerInfo(string name, string id, string password)
    {
        Name = name;
        ID = id;
        Password = password;
    }
}

public class JsonManager : MonoBehaviour
{

    public GameObject mainmenu;
    //회원가입을 통해 받은 정보를 저장할 변수
    public InputField S_NameInputField;
    public InputField S_IDInputField;
    public InputField S_PasswordInputField;

    //로그인을 통해 입력할 정보를 저장할 변수
    public InputField L_IDinputField = null;
    public InputField L_PasswordInputField = null;

    public List<PlayerInfo> PlayerList = new List<PlayerInfo>();
    public List<PlayerInfo> PlayerCheck = new List<PlayerInfo>();

    public void SaveBtnFunc()
    {
        Debug.Log("저장합니다");

        /*for(int i = 0; i < PlayerList.Count; i++)
        {
            Debug.Log(PlayerList[i].ID);
        }*/

        //PlayerList를 Json파일화하는 코드
        JsonData PlayerJson = JsonMapper.ToJson(PlayerList);
        File.WriteAllText(Application.dataPath + "/Resource/PlayerData.json", PlayerJson.ToString());

    }

    public void LoadBtnFunc()
    {
        
        Debug.Log("불러옵니다");
        StartCoroutine(LoadJson());
    }

    IEnumerator LoadJson()
    {
        string Jsonstring = File.ReadAllText(Application.dataPath + "/Resource/PlayerData.json");


        //Debug.Log(Jsonstring);
        
        JsonData PlayerData = JsonMapper.ToObject(Jsonstring);
        ParsingJsonPlayer(PlayerData);
        yield return null;
    }


    private void ParsingJsonPlayer(JsonData name)
    {
        Debug.Log("정보확인중");
        for (int i = 0; i < name.Count; i++)
        {
            //Debug.Log(name[i]["ID"]);
            //string player_ID = name[i]["ID"].ToString();
            //string player_password = name[i]["Password"].ToString();
 
            Debug.Log(PlayerList.Count);
             for (int j = 0; j < PlayerList.Count; j++)
             {
                if (L_IDinputField.text == PlayerList[j].ID.ToString() && L_PasswordInputField.text == PlayerList[j].Password.ToString())
                 {
                     //Debug.Log("gamescene으로~");
                     SceneManager.LoadScene("MainGameScene");
                 }
                 else if(L_IDinputField.text != PlayerList[j].ID.ToString() || L_PasswordInputField.text != PlayerList[j].Password.ToString())
                 {
                     Debug.Log("정보가 없습니다. 회원가입해주세요");
                    mainmenu.SetActive(true);
                    
                 }
             }
        }
    }
        public void SingUpBtn()
    {
        //PlayerInfo.instance.Name = S_NameInputField.text;
        //PlayerInfo.instance.ID = S_IDInputField.text;
        //PlayerInfo.instance.Password = S_PasswordInputField.text;

        //Debug.Log(S_NameInputField.text);
        //Debug.Log(S_IDInputField.text);
        //Debug.Log(S_PasswordInputField.text);
        PlayerList.Add(new PlayerInfo(S_NameInputField.text, S_IDInputField.text, S_PasswordInputField.text));
        SaveBtnFunc();
    }
   
    public void LogInBtn()
    {
        //Debug.Log(L_IDinputField.text);
        //Debug.Log(L_PasswordInputField.text);
        LoadBtnFunc();

    }
}
