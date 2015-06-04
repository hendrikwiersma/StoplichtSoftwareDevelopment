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

	private const byte SEND_VEHICLE_ID = 0x03;
	private const byte FILLER = 0x00;

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
				ChangeTrafficLight (inStream [1], getLightState(inStream[2]));
				break;
			default:
				Debug.LogError("Incorrect message id:" + inStream[0]);
				break;
			}

		}

	}

	public void SpawnVehicle(byte startpoint, byte endpoint, byte type){

		Data.DIRECTION StartingPoint = getDirection (startpoint);
		Data.DIRECTION EndPoint = getDirection (endpoint);
		Data.VEHICLE_TYPE VehicleType = getVehicleType (type);

		List<Spawnpoint> possibleSpawnpoints = new List<Spawnpoint>();
		foreach(GameObject gameObj in spawnpoints_)
		{
			Spawnpoint spawnpoint = gameObj.GetComponent<Spawnpoint>();

			if(spawnpoint.begin == StartingPoint && spawnpoint.vehicle == VehicleType && (spawnpoint.end == EndPoint || spawnpoint.end == Data.DIRECTION.AUTO)){

				possibleSpawnpoints.Add(spawnpoint);

			}

		}

		if (verifyDirection(StartingPoint, EndPoint, VehicleType)) {

			foreach(Spawnpoint currentspawnpoint in possibleSpawnpoints){

				if(currentspawnpoint.spawnpointAvailable() == true){

					switch(VehicleType) {
					case Data.VEHICLE_TYPE.CAR: 
						spawnCar(currentspawnpoint, StartingPoint, EndPoint);
						break;
						
					case Data.VEHICLE_TYPE.BIKE:
						spawnBicycle(currentspawnpoint, EndPoint);
						break;

					case Data.VEHICLE_TYPE.BUS:
						spawnBus(currentspawnpoint, EndPoint);
						break;
						
					case Data.VEHICLE_TYPE.PEDESTRIAN:
						spawnPedestrian(currentspawnpoint, EndPoint);		
						break;

					}
					break;

				}

			}

		}
		
	}

	void ChangeTrafficLight(byte id, Data.LIGHT_STATE State){

		if (verifyLight(id, State)) {

			foreach(Trafficlight light in trafficlights_) {

				if(light.id == id){

					light.setNewState(State);

				}

			}

			foreach(Transform gameObj in TrafficLightStopGoBoxes.transform) {

				WaitingScript hotspot = gameObj.GetComponent<WaitingScript>();
				if(hotspot.TrafficLightID == id){
					{
						if(State == Data.LIGHT_STATE.GREEN){
							hotspot.SetGoBool(true);
						}
						else{
							hotspot.SetGoBool(false);
						}

					}

				}

			}

		}

	}

	public void SendVehicleSignal(int trafficlightid, int state){

		// dont send if not connected
		if (serverStream != null) {

			byte[] outStream = {SEND_VEHICLE_ID, (byte)trafficlightid, (byte)state, FILLER};
			serverStream.Write (outStream, 0, outStream.Length);

		}

	}

	public void registerSpawnpoint(GameObject spawnpoint) {

		spawnpoints_.Add (spawnpoint);

	}

	public void registerLight(Trafficlight light) {

		trafficlights_.Add (light);

	}

	public Data.DIRECTION getDirection(byte dir) {

		switch(dir) {

			case 0: return Data.DIRECTION.NORTH;
			case 1: return Data.DIRECTION.EAST;
			case 2: return Data.DIRECTION.SOUTH;
			case 3: return Data.DIRECTION.WEST;
			case 4: return Data.DIRECTION.VENTWEG;
			default:
				Debug.Log("Received incorrect direction");
				return Data.DIRECTION.NULL;

		}

	}

	public Data.VEHICLE_TYPE getVehicleType(byte dir) {
		
		switch(dir) {
			
		case 0: return Data.VEHICLE_TYPE.CAR;
		case 1: return Data.VEHICLE_TYPE.BIKE;
		case 2: return Data.VEHICLE_TYPE.BUS;
		case 3: return Data.VEHICLE_TYPE.PEDESTRIAN;
		default:
			Debug.Log("Received incorrect vehicle type");
			return Data.VEHICLE_TYPE.NULL;
			
		}
		
	}

	public Data.LIGHT_STATE getLightState(byte dir) {
		
		switch(dir) {
			
		case 0: return Data.LIGHT_STATE.RED;
		case 1: return Data.LIGHT_STATE.ORANGE;
		case 2: return Data.LIGHT_STATE.GREEN;
		default:
			Debug.Log("Received incorrect light state");
			return Data.LIGHT_STATE.NULL;
			
		}
		
	}

	public void spawnCar(Spawnpoint Spawn, Data.DIRECTION Begin, Data.DIRECTION End) {

		GameObject car = Instantiate(Auto, Spawn.transform.position, Spawn.transform.rotation) as GameObject;
		int counter = 0;
		AIControllerWheelCol aiscript = car.GetComponent<AIControllerWheelCol>();
		foreach(Transform gameObj in WaypointCollection.transform)
		{
			if(gameObj.transform.name == EnumToString(Begin) + EnumToString(End)){
				
				if(counter == 0){
					aiscript.WaypointCollection1 = gameObj.gameObject;
					counter++;
				}
				else if(counter == 1){
					aiscript.WaypointCollection2 = gameObj.gameObject;
					counter++;
				}
				else{
					print("Not more than two waypoint collections possible.");
				}
			}
		}
		if(aiscript.WaypointCollection2 == null){
			aiscript.WaypointCollection2 = aiscript.WaypointCollection1;
		}
		if(Spawn.StartWaypointCollection == 0){
			aiscript.CurrentWaypoints = aiscript.WaypointCollection1;
		}
		else if(Spawn.StartWaypointCollection == 1){
			aiscript.CurrentWaypoints = aiscript.WaypointCollection2;
		}
		else{
			print("Wrong start waypointNumber set in spawncube.");
		}

		car.SetActive (true);

	}

	public void spawnBicycle(Spawnpoint Spawn, Data.DIRECTION End) {

		GameObject Bicycle = Instantiate(Fiets, Spawn.transform.position, Spawn.transform.rotation) as GameObject;
		AIData AIB = Bicycle.GetComponent<AIData>();
		AIB.Init(Spawn.gameObject, End);
		Bicycle.SetActive(true);

	}

	public void spawnBus(Spawnpoint Spawn, Data.DIRECTION End) {

		Instantiate(Bus, Spawn.transform.position, Spawn.transform.rotation);

	}

	public void spawnPedestrian(Spawnpoint Spawn, Data.DIRECTION End) {

		GameObject Pedestrian = Instantiate(Voetganger, Spawn.transform.position, Spawn.transform.rotation) as GameObject;
		AIData AIP = Pedestrian.GetComponent<AIData>();
		AIP.Init(Spawn.gameObject, End);
		Pedestrian.SetActive(true);	

	}

	public Boolean verifyDirection(Data.DIRECTION Begin, Data.DIRECTION End, Data.VEHICLE_TYPE Vehicle) {

		if (Begin == Data.DIRECTION.NULL || End == Data.DIRECTION.NULL || Vehicle == Data.VEHICLE_TYPE.NULL) {

			Debug.LogWarning("Found spawn attempt with uninitiallized data");
			return false;

		}

		if (Begin == End) {

			Debug.LogWarning("Received vehicle with the same Start and Destination");
			return false;

		}

		if (Vehicle == Data.VEHICLE_TYPE.CAR || Vehicle == Data.VEHICLE_TYPE.BUS) {

			if (Begin == Data.DIRECTION.VENTWEG && (End == Data.DIRECTION.SOUTH || End == Data.DIRECTION.WEST)) {

				Debug.LogWarning ("Received vehicle with impossible route from Ventweg");
				return false;

			}

			if (End == Data.DIRECTION.VENTWEG && (Begin == Data.DIRECTION.NORTH || Begin == Data.DIRECTION.EAST)) {
				
				Debug.LogWarning ("Received vehicle with impossible route to Ventweg");
				return false;
				
			}

		}
	
		return true;

	}

	public Boolean verifyLight(byte Id, Data.LIGHT_STATE State) {

		if (State == Data.LIGHT_STATE.NULL) {
			
			Debug.LogWarning("Found set light attempt with uninitiallized data");
			return false;
			
		}

		if (Id < 0 || Id > 50|| Id == 28 || Id == 29 || Id == 31 || Id == 33 || Id == 41 || Id == 42 || Id == 44 || Id == 45) {

			Debug.LogWarning("Received light state with unknown ID:" + Id);
			return false;

		}

		return true;

	}

	private String EnumToString(Data.DIRECTION Direction) {

		switch (Direction) {

			case Data.DIRECTION.NORTH: return "Noord";
			case Data.DIRECTION.EAST: return "Oost";
			case Data.DIRECTION.SOUTH: return "Zuid";
			case Data.DIRECTION.WEST: return "West";
			case Data.DIRECTION.VENTWEG: return "Ventweg";

		}

		return null;

	}

}
