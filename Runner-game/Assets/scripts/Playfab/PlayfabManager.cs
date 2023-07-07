using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject rowCoinPrefab;
    public Transform rowsCoinParent;
    public GameObject ranking;
    public GameObject leaderboard;
    public GameObject login;
    public GameObject menu;
    public GameObject nameWindow;
     public TMP_InputField nameInput;
    public static bool isFirstLoad = true;


    [Header("UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;


    public void RegisterButton(){
        if(passwordInput.text.Length < 6){
            messageText.text = "Password too short!";
            return;
        }

        var request = new RegisterPlayFabUserRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser( request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text = "registered and logged in!";
    }

    public void loadMenu(){
        login.SetActive(false);
        menu.SetActive(true);
    }

    public void LoginButton(){
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result){
        messageText.text = "Logged in!";
        isFirstLoad=false;
        string name = null;
        if ( result.InfoResultPayload.PlayerProfile != null){
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        if(name == null) {
            login.SetActive(false);
            nameWindow.SetActive(true);
        }
        else {
            login.SetActive(false);
            menu.SetActive(true);
        }
        Debug.Log("Successful login/account create!");
    }

    public void SubmitNameButton() {
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result){
        Debug.Log("Updated display name!");
        nameWindow.SetActive(false);
        menu.SetActive(true);
    }

    public void ResetPasswordButton(){
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailInput.text,
            TitleId = "DF91C"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result){
        messageText.text = "Password reset mail sent!";
    }


    // Start is called before the first frame update
    void Start()
    {
        if(isFirstLoad)
        {  
            Login();
        }
        else {
            loadMenu();
        }
    }

    // Update is called once per frame
    void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result){
        Debug.Log("Successful login/account create!");
    }

    void OnError(PlayFabError error){
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    public void ToggleLeaderboard () {
        GetLeaderboard();
        GetLeaderboard2();
    }

    public void SendLeaderboard(int distance){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "RunnerScore",
                    Value = distance
                } 
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    public void SendLeaderboard2(int coins){
        var request2 = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "CoinScore",
                    Value = coins
                } 
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request2, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Successful leaderboard sent!");
    }

    public void GetLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "RunnerScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    public void GetLeaderboard2() {
        var request2 = new GetLeaderboardRequest {
            StatisticName = "CoinScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request2, OnLeaderboardGet2, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result){
        foreach ( var item in result.Leaderboard) {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            
        
            
            menu.SetActive(false);
            ranking.SetActive(true);

        }
    }

        void OnLeaderboardGet2(GetLeaderboardResult result2){
        foreach ( var item in result2.Leaderboard) {
            GameObject newGo2 = Instantiate(rowCoinPrefab, rowsCoinParent);
            TMP_Text[] texts2 = newGo2.GetComponentsInChildren<TMP_Text>();
            texts2[0].text = (item.Position+1).ToString();
            texts2[1].text = item.DisplayName;
            texts2[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            

        }
    }
}
