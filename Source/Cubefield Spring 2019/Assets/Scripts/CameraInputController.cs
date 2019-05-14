using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class CameraInputController : MonoBehaviour {

	//camera input specfic fields
	//source: https://www.raywenderlich.com/5475-introduction-to-using-opencv-with-unity
	private static Thread receiveThread;
	private static UdpClient client;
	public static int port;

	public static bool cameraInput = false;
	public static float cameraMotion = 0;
	public static string inputPath = "Assets/Resources/CameraInput.txt";

	public static void InitUDP() {
		receiveThread = new Thread(new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}

	//returns -1 on left, 0 on center, 1 on right
	private static void ReceiveData() {
		client = new UdpClient(port);
		while (true) {
			try {
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
				byte[] data = client.Receive(ref anyIP);

				string text = Encoding.UTF8.GetString(data);
				if (text.Equals("left")) {
					cameraMotion = -1;
				} else if (text.Equals("right")) {
					cameraMotion = 1;
				} else {
					cameraMotion = 0;
				}
				//print("Camera Input: " + text);

			} catch (System.Exception e) {
				print(e.ToString());
			}
		}
	}

	private void Update() {
		cameraInput = GetComponent<Toggle>().isOn;
	}

	public static float GetAxisRaw() {
		return cameraMotion;
	}
	/* Old way of using files to send data
	public static float GetAxisRaw() {
		//Exit with error code 2 if file not found
		if (!File.Exists(inputPath)) {
			Debug.Log("Input not found");
			if (Application.isEditor) {
				EditorApplication.Exit(2);
			}
			Application.Quit(2);
			
		}

		//Read the first line in inputPath, then go in that direction
		using (StreamReader sr = File.OpenText(inputPath)) {
			string s = sr.ReadLine();
			//Debug.LogFormat(s);
			if (s.Equals("left")) {
				return -1f;
			} else if (s.Equals("right")) {
				return 1f;
			} else {
				return 0f;
			}
		}
	}*/
}
