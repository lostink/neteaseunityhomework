using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MobileShooter : MonoBehaviour {

    //public GameObject Ball;
    public GameObject ARCamera;
    public Button ShootFrontButton;


    bool started = false;
    //float swipespeed_max = 5; // 0.2s cross screen height
    float swipespeed_min = 1; // 1s cross screen height
    //float ballspeed_max = 25f;
    //float ballspeed_min = 2f;
    Vector3 mousedown_pos;
    float mousedowned_time;

    bool bMouseDown = false;
    float ballSpeedFixed = 25f;

	float nextFire;
	public float fireRate;

	public GameObject CountDown;
	float startTime = 0;
	float totalTime = 0;



    // Use this for initialization
    void Start()
    {
		startTime = Time.time;
		ShootFrontButton.enabled = true;
		ShootFrontButton.onClick.AddListener(ShootBallFront);
		started = true;
		nextFire = 0.0f;
    }

    public void Activate()
    {
        ShootFrontButton.enabled = true;
        ShootFrontButton.onClick.AddListener(ShootBallFront);
        started = true;
    }

    // shoot ball on swipe
    void Update()
    {
		totalTime = (60.0f - (Time.time - startTime));
		CountDown.GetComponent<GUIText> ().text = "Time Left: " + totalTime.ToString();

        if (!started) return;

        if (bMouseDown)
        {
            mousedowned_time += Time.deltaTime;
        }

        if (!bMouseDown && Input.GetMouseButtonDown(0))
        {
            mousedown_pos = Input.mousePosition;
            mousedowned_time = 0;
            bMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
			float timeNow = Time.time;
			if (!bMouseDown || mousedowned_time <= 0.05f)
				return;
			Vector3 mouseup_pos = Input.mousePosition;
			Vector3 deltaVec = mouseup_pos - mousedown_pos;
			float dx_ratio = deltaVec.x / Screen.width;
			float dy_ratio = deltaVec.y / Screen.height;
			float dz_ratio = 2.0f;
			float speed = 10.0f;
			Vector4 BallVel_CameraSpace = new Vector4 (dx_ratio * 10.0f, dy_ratio * 10.0f, dz_ratio * 10.0f, 1.0f);
			Matrix4x4 O = Matrix4x4.identity;
			O.SetRow (0, new Vector4 (transform.right.x, transform.right.y, transform.right.z, 0.0f));
			O.SetRow (1, new Vector4 (transform.up.x, transform.up.y, transform.up.z, 0.0f));
			O.SetRow (2, new Vector4 (transform.forward.x, transform.forward.y, transform.forward.z, 0.0f));
			Vector4 BallVel_UnitySpacwe4 = (O * BallVel_CameraSpace);
			Vector3 BallVel = new Vector3 (BallVel_UnitySpacwe4.x, BallVel_UnitySpacwe4.y, BallVel_UnitySpacwe4.z);
			ShootBall (BallVel);
			bMouseDown = false;
			mousedowned_time = 0;
        }
    }

    public void ShootBall(Vector3 velocity)
    {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			GetComponent<AudioSource> ().Play ();

			// You may want to use a random nice color so there is one!
			Color color = Random.ColorHSV (0f, 1f, 0.5f, 1f, 0.5f, 1f, 1f, 1f);
			Vector3 color_v = new Vector3 (color.r, color.g, color.b);

			// TODO-2.c PhotonNetwork.Instantiate to shoot a ball!
			// You may want to initialize a RPC function call to RPCInitialize() 
			//   (See BallBehavior.cs) to set the velocity and color
			//   of the ball across all clients (PhotonTargets.All) and transfer 
			//   the ownership of the ball to PC so the ball is correctly destroyed
			//   upon hitting a wall.
			Vector3 CP = ARCamera.transform.position;
			PhotonView photonView = GetComponent<PhotonView> ();
			photonView.RPC ("ShootBallPC", PhotonTargets.All, new float[] {CP.x, CP.y, CP.z,
				color_v.x, color_v.y, color_v.z,
				velocity.x, velocity.y, velocity.z, 0.0f, 0.0f
			});
		}
    }



    public void ShootBallFront()
    {
        ShootBall(ballSpeedFixed * ARCamera.transform.forward);
    }

    public void ShootBallUp()
    {
        ShootBall(ballSpeedFixed * ARCamera.transform.up);
    }
}
