using UnityEngine;
using Photon.Pun;

public class MultiplayerGameManager : MonoBehaviour
{
    public static bool isPlayer1Ready;
    public static bool isPlayer2Ready;

    public static bool hasGameStarted;

    public void ready(bool isPlayer1)
    {
        PhotonView pView = GetComponent<PhotonView>();
        pView.RPC("RPC_PlayerReady", RpcTarget.Others);
        if (isPlayer1)
            isPlayer1Ready = true;
        else
            isPlayer2Ready = true;
        Evaluate();

    }

    public static void StartGame()
    {
        hasGameStarted = true;
    }

    private void Evaluate()
    {
        if (isPlayer1Ready && isPlayer2Ready)
        {
            GameObject.Find("Time").GetComponent<Countdown>().initiateCountdown();
        }
    }

    [PunRPC]
    private void RPC_PlayerReady()
    {
        if (PhotonNetwork.IsMasterClient) // If this is true it means the other player is the master client
            isPlayer2Ready = true;
        else
            isPlayer1Ready = true;
        Evaluate();
    }
}
