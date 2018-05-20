using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour
{
	string roomName;
	public GameObject ScoreBoard;
	int total_score = 0;

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		roomName = "testing";
	}

	public override void OnJoinedLobby()
	{
		//PhotonNetwork.CreateRoom(null);
		PhotonNetwork.JoinRoom(roomName);
		base.OnJoinedLobby ();
	}
	public override void OnJoinedRoom()
	{
		GetComponent<MobileShooter>().Activate();
		base.OnJoinedRoom ();
	}

	[PunRPC]
	public void ScoreHitting(int input){
		total_score += input;
		ScoreBoard.GetComponent<GUIText> ().text = "Score: " + total_score.ToString();
	}
}
