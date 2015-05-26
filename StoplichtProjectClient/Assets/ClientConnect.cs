using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System;
using RAIN;

public class ClientConnect : MonoBehaviour {
	public string ipaddress;
	public GameObject WaypointCollection;
	public GameObject Auto;
	public GameObject Voetganger;
	public GameObject Bus;
	public GameObject Fiets;
	public GameObject spawnpoints;
	public GameObject trafficlights;
	public GameObject TrafficLightStopGoBoxes;
	private List<GameObject> spawnpoints_ = new List<GameObject>();
	private List<Trafficlight> trafficlights_ = new List<Trafficlight>();
	System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
	NetworkStream serverStream;
	
	void Start () {
		print("Client Started");
		clientSocket.Connect(ipaddress, 10000);
		print("Client Socket Program - Server Connected ...");
		serverStream = clientSocket.GetStream();

		foreach (Transform s in spawnpoints.transform) {

			spawnpoints_.Add(s.gameObject);

		}

		foreach (Transform gameObj in trafficlights.transform) {
		
			trafficlightscript lightScript = gameObj.GetComponent<trafficlightscript>();
			Trafficlight light = gameObj.gameObject.AddComponent<TrafficlightCar>();

			light.id = lightScript.ID;
			trafficlights_.Add(light);

		}

	}
	
	// Update is called once per frame
	void Update () {
		if (serverStream.DataAvailable) {
			byte[] inStream = new byte[4];

			int bytesRead = serverStream.Read (inStream, 0, inStream.Length);

			switch (inStream [0]) {
			case 1:
				SpawnVehicle (inStream [1], inStream [2], inStream [3]);
				break;
			case 2:
				ChangeTrafficLight (inStream [1], inStream [2]);
				break;
			case 3:
				break;
			default:
				break;
			}
		}
	}
	void SpawnVehicle(byte startpoint, byte endpoint, byte type){
		string StartingPoint = null;
		System.Random newrand = new System.Random ();
		switch (startpoint)
        {
        case 0:
                StartingPoint = "Noord";
                break;
        case 1:
                StartingPoint = "Oost";
                break;
        case 2:
                StartingPoint = "Zuid";
                break;
        case 3:
                StartingPoint = "West";
                break;
        case 4:
                StartingPoint = "Ventweg";
                break;
        default:
                print("Invalid startingpoint.");
                break;
        }
		string EndPoint = null;
		switch (endpoint)
		{
		case 0:
			EndPoint = "Noord";
			break;
		case 1:
			EndPoint = "Oost";
			break;
		case 2:
			EndPoint = "Zuid";
			break;
		case 3:
			EndPoint = "West";
			break;
		case 4:
			EndPoint = "Ventweg";
			break;
		default:
			print("Invalid endpoint.");
			break;
		}
		string VehicleType = null;
		switch (type)
		{
		case 0:
			VehicleType = "Auto";
			break;
		case 1:
			VehicleType = "Fiets";
			break;
		case 2:
			VehicleType = "Bus";
			break;
		case 3:
			VehicleType = "Voetganger";
			break;
		default:
			print("Invalid VehicleType.");
			break;
		}
//		print ("Received packet: Spawning a " + VehicleType + " at " + StartingPoint + " that is heading to " + EndPoint);
		List<Spawnpoint> possibleSpawnpoints = new List<Spawnpoint>();
		foreach(GameObject gameObj in spawnpoints_)
		{
			Spawnpoint spawnpoint = gameObj.GetComponent<Spawnpoint>();

			if(spawnpoint.direction.ToString() == StartingPoint && spawnpoint.vehicle.ToString() == VehicleType){

				possibleSpawnpoints.Add(spawnpoint);

			}

		}
		foreach(Spawnpoint currentspawnpoint in possibleSpawnpoints){

			if(currentspawnpoint.available == true){
				currentspawnpoint.available = false;

				switch(VehicleType) {
				case "Auto": 

					GameObject car = Instantiate(Auto, currentspawnpoint.transform.position, currentspawnpoint.transform.rotation) as GameObject;
					AIController controller = car.GetComponent<AIController>();
					foreach(Transform gameObj in WaypointCollection.transform)
					{
						if(gameObj.transform.name == StartingPoint+EndPoint){
							print("Spawning new car: " + gameObj.transform.name + " " + StartingPoint+EndPoint);
							AIControllerWheelCol aiscript = car.GetComponent<AIControllerWheelCol>();
							aiscript.WaypointCollection = gameObj.gameObject;
							aiscript.WaypointCollection2 = gameObj.gameObject;
						}
					}
					break;

				case "Voetganger":
					//Instantiate(Voetganger, currentspawnpoint.transform.position, currentspawnpoint.transform.rotation) as GameObject;			
					break;

				case "Fiets":
					GameObject Bicycle = Instantiate(Fiets, currentspawnpoint.transform.position, currentspawnpoint.transform.rotation) as GameObject;
					BikeAI AI = Bicycle.GetComponent<BikeAI>();
					AI.Init(currentspawnpoint.gameObject, EndPoint);
					Bicycle.SetActive(true);

					break;

				case "Bus":
					Instantiate(Bus, currentspawnpoint.transform.position, currentspawnpoint.transform.rotation);
					break;
				
				}
				break;
			}
		}
		
	}

	void ChangeTrafficLight(byte id, byte state){
		string State = null;
		switch (state)
		{
		case 0:
			State = "Rood";
			break;
		case 1:
			State = "Oranje";
			break;
		case 2:
			State = "Groen";
			break;
		default:
			print("Invalid Trafficlight state.");
			break;
		}
		print ("Received packet: Turning the trafficlight with id " + id + " " + State);

		foreach(Trafficlight light in trafficlights_) {

			if(light.id == id){
				print ("Executing void.");
				light.setNewState(State);

			}

		}

		foreach(Transform gameObj in TrafficLightStopGoBoxes.transform)
		{
			WaitingScript hotspot = gameObj.GetComponent<WaitingScript>();
			if(hotspot.TrafficLightID == id){
				{
					if(State == "Groen"){
						hotspot.SetGoBool(true);
					}
					else{
						hotspot.SetGoBool(false);
					}
				}
			}
		}
	}

	public void SendVehicleSignal(int trafficlightid, int state){

		// dont send if not connected
		if (serverStream != null) {

			byte[] outStream = {0x03, (byte)trafficlightid, (byte)state, 0x00};

			if (state == 0) {

				print ("Sending packet: A car is logging in at trafficlight id " + trafficlightid);

			} else {

				print ("Sending packet: A car is logging out at trafficlight id " + trafficlightid);

			}

			serverStream.Write (outStream, 0, outStream.Length);
		}

	}

	public void registerSpawnpoint(GameObject spawnpoint) {

		spawnpoints_.Add (spawnpoint);

	}

	public void registerLight(Trafficlight light) {

		trafficlights_.Add (light);

	}

}
