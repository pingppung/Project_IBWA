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

    //---------------------------- keyboard를 통해 받을 경우
    string S_NameWord = null;
    int S_NameWordIndex = 0;
    string Namealpha;

    string S_IDWord = null;
    int S_IDWordIndex = 0;
    string IDalpha;

    string S_PWWord = null;
    int S_PWWordIndex = 0;
    string PWalpha;

    public Text S_Name;
    public Text S_ID;
    public Text S_PW;
    //--------------------------------------------------
    int i = 0;
    string[] name;
    string[] id;
    string[] pw;
    //--------------------------------------

    //로그인을 통해 입력할 정보를 저장할 변수
    public InputField L_IDinputField = null;
    public InputField L_PasswordInputField = null;
    //---------------------------- keyboard를 통해 받을 경우
    string L_IDWord = null;
    int L_IDWordIndex = 0;
    //string Namealpha;

    string L_PWWord = null;
    int L_PWWordIndex = 0;
    //string IDalpha;
  
    public Text L_ID;
    public Text L_PW;

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
            if(PlayerList.Count == 0)
            {
                Debug.Log("정보가 없습니다. 회원가입해주세요");
                mainmenu.SetActive(true);
            }
             for (int j = 0; j < PlayerList.Count; j++)
             {
                if (L_ID.text == PlayerList[j].ID.ToString() && L_PW.text == PlayerList[j].Password.ToString())
                 {
                     //Debug.Log("gamescene으로~");
                     SceneManager.LoadScene("MainGameScene");
                 }
                 else if(L_ID.text != PlayerList[j].ID.ToString() || L_PW.text != PlayerList[j].Password.ToString())
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
        PlayerList.Add(new PlayerInfo(S_Name.text, S_ID.text, S_PW.text));
        SaveBtnFunc();
    }
   
    public void LogInBtn()
    {
        //Debug.Log(L_IDinputField.text);
        //Debug.Log(L_PasswordInputField.text);
        LoadBtnFunc();

    }
    //하ㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏ 야매로 회원가입 키보드 기능 구현
    public void S_NickName_alphabetFunction(string alphabet)
    {
        S_NameWordIndex++;
        S_NameWord = S_NameWord + alphabet;
        S_Name.text = S_NameWord;

    }
    public void S_NickName_Eraser()
    {
        S_NameWord = null;
        /*S_NameWordIndex--;
        S_NameWord = null;
        for(int j = 0; j < i; j++)
        {
            Debug.Log(name[j]);
            S_NameWord = S_NameWord + name[j];
            
        }*/
        S_Name.text = S_NameWord;
        
    }
    public void S_ID_alphabetFunction(string alphabet)
    {
        Debug.Log("a");
        S_IDWordIndex++;
        //name[i] = alphabet;
        S_IDWord = S_IDWord + alphabet;
        S_ID.text = S_IDWord;

    }
    public void S_ID_Eraser()
    {
        S_IDWord = null;
        S_ID.text = S_IDWord;
    }
    public void S_PW_alphabetFunction(string alphabet)
    {
        S_PWWordIndex++;
        S_PWWord = S_PWWord + alphabet;
        S_PW.text = S_PWWord;

    }
    public void S_PW_Eraser()
    {
        S_PWWord = null;
        S_PW.text = S_PWWord;
    }
    //하ㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏ 야매로 로그인 기능 구현
    public void L_ID_alphabetFunction(string alphabet)
    {
        L_IDWordIndex++;
        //name[i] = alphabet;
        L_IDWord = L_IDWord + alphabet;
        L_ID.text = L_IDWord;

    }
    public void L_ID_Eraser()
    {
        L_IDWord = null;
        L_ID.text = L_IDWord;
    }
    public void L_PW_alphabetFunction(string alphabet)
    {
        L_PWWordIndex++;
        L_PWWord = L_PWWord + alphabet;
        L_PW.text = L_PWWord;

    }
    public void L_PW_Eraser()
    {
        L_PWWord = null;
        L_PW.text = L_PWWord;
    }
}
