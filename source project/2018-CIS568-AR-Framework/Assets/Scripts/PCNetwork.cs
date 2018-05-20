using UnityEngine;
using System.Collections;

public class PCNetwork : Photon.PunBehaviour
{
    // This is for Paint Ball networking at PC
    //     if you are looking for LOOK-1.b, please refer to PCNetwork_Cube.cs 
    string roomName;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        roomName = GenerateRoomName();

    }

    void OnGUI()
    {
        GUI.contentColor = Color.red;
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() + " Room Name: " + roomName);
		//sss
		var a = 1.0f;
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonJoinRoomFailed(codeAndMsg);
    }

    public override void OnCreatedRoom()
    {
		base.OnCreatedRoom();
    }

    static string GenerateRoomName()
    {
		return "testing";
        const string characters = "abcdefghijklmnopqrstuvwxyz0123456789";

        string result = "";

        int charAmount = Random.Range(4, 6);
        for (int i = 0; i < charAmount; i++)
        {
            result += characters[Random.Range(0, characters.Length)];
        }

        return result;
    }
	public void addScore(int score, GameObject ball_other){
		PhotonView photonView = GetComponent<PhotonView> ();
		PhotonNetwork.Destroy (ball_other);
		photonView.RPC ("ScoreHitting", PhotonTargets.All, score);
	}
	[PunRPC]
	//public void ShootBallPC(float ix,float iy,float iz,float cr,float cg,float cb,float vx,float vy, float vz,float dx,float dy){
	public void ShootBallPC(float[] input){
		float ix = input [0];
		float iy = input [1];
		float iz = input [2];
		float cr = input [3];
		float cg = input [4];
		float cb = input [5];
		float vx = input [6];
		float vy = input [7];
		float vz = input [8];
		float dx = input [9];
		float dy = input [10];
		GameObject ball = PhotonNetwork.Instantiate ("ball",new Vector3(ix,iy,iz),Quaternion.identity,0);
		Rigidbody rb = ball.GetComponent<Rigidbody>();
		rb.velocity = new Vector3 (vx,vy,vz);
		Renderer rend = ball.GetComponent<Renderer>();
		rend.material.SetColor("_Color",new Color(cr,cg,cb));
	}
}