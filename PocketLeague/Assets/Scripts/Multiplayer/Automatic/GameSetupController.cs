using Photon.Pun;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    private static GameObject p1;
    private static Vector3 defaultP1Pos;
    private static GameObject p2;
    private static Vector3 defaultP2Pos;

    public static string otherPlayerNickname;

    private static PhotonView pView;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "MultiplayerManager"), Vector3.zero, Quaternion.identity); // Instantiating multiplayers controller
    }
    void Start()
    {
        defaultP1Pos = new Vector3(-8f, -3f, 0f);
        defaultP2Pos = new Vector3(8f, -3f, 0f);
        CreatePlayer();

        if (PhotonNetwork.IsMasterClient)
            instatiateObjects();
        pView = gameObject.GetComponent<PhotonView>();
        Debug.Log("Sending RPC");
        pView.RPC("RPC_SendPlayerNickName", RpcTarget.Others, PlayerPrefs.GetString(Constants.NicknameKey, ""));
    }


    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 spawnVector = defaultP1Pos;
            Quaternion spawnRotation = Quaternion.Euler(0, 90, 0);
            p1 = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerCar"), spawnVector, spawnRotation);
        }
        else
        {
            Vector3 spawnVector = defaultP2Pos;
            Quaternion spawnRotation = Quaternion.Euler(0, -90, 0);
            p2 = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerCar 1"), spawnVector, spawnRotation);
        }
    }



    public static GameObject ball;
    /// <summary>
    /// Called when a goal is scored or to instatiate objs in the beginning
    /// </summary>
    public static void instatiateObjects()
    {
        DeleteObjs();
        ResetCarsPosition();
        var balls = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Ball(Clone)");

        if (!balls.Any() && PhotonNetwork.IsMasterClient) // If comes inside it means its empty
            ball = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Ball"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Goal"), new Vector3(-11f, Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity).tag = "Goal1";
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Goal"), new Vector3(11f, Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity).tag = "Goal2";

    }

    private static void DeleteObjs()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Goal(Clone)" || obj.name == "Ball(Clone)");
        foreach (var singleObj in objects)
            PhotonNetwork.Destroy(singleObj);
    }

    private static void ResetCarsPosition()
    {
        if (p1 != null)
        {
            p1.transform.position = defaultP1Pos;
            p1.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (p2 != null)
        {
            p2.transform.position = defaultP2Pos;
            p2.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }


    // RPCs

    [PunRPC]
    private void RPC_SendPlayerNickName(string playerName)
    {
        string playerNick = PlayerPrefs.GetString(Constants.NicknameKey, "");
        if (playerNick == playerName)
            return;
        Score.setPlayersNickNames(playerName, PhotonNetwork.IsMasterClient);
    }
}
