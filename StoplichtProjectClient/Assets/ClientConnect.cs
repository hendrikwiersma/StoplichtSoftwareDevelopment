using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System;

public class ClientConnect : MonoBehaviour {
	public string ipaddress;
	public GameObject Auto;
	public GameObject Voetganger;
	public GameObject Bus;
	public GameObject Fiets;
	public GameObject spawnpoints;
	public GameObject trafficlights;
	System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
	NetworkStream serverStream;
	// Use this for initialization
	void Start () {
		print("Client Started");
		clientSocket.Connect(ipaddress, 10000);
		print("Client Socket Program - Server Connected ...");
		serverStream = clientSocket.GetStream();

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
		print ("Received packet: Spawning a " + VehicleType + " at " + StartingPoint + " that is heading to " + EndPoint);
		List<Spawnpoint> possibleSpawnpoints = new List<Spawnpoint>();
		foreach(Transform gameObj in spawnpoints.transform)
		{
			Spawnpoint spawnpoint = gameObj.GetComponent<Spawnpoint>();
			if(spawnpoint.direction.ToString() == StartingPoint && spawnpoint.vehicle.ToString() == VehicleType){
				{
					possibleSpawnpoints.Add(spawnpoint);
				}
			}
		}
		Spawnpoint chosenSpawnPoint = possibleSpawnpoints [newrand.Next (possibleSpawnpoints.Count)];
		if (VehicleType == "Auto") {
			Instantiate(Auto, chosenSpawnPoint.transform.position, chosenSpawnPoint.transform.rotation);
		}
		else if (VehicleType == "Voetganger") {
			Instantiate(Voetganger, chosenSpawnPoint.transform.position, chosenSpawnPoint.transform.rotation);			
		}
		else if (VehicleType == "Fiets") {
			Instantiate(Fiets, chosenSpawnPoint.transform.position, chosenSpawnPoint.transform.rotation);
		}
		else if (VehicleType == "Bus") {
			Instantiate(Bus, chosenSpawnPoint.transform.position, chosenSpawnPoint.transform.rotation);
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
		foreach(Transform gameObj in trafficlights.transform)
		{
			trafficlightscript light = gameObj.GetComponent<trafficlightscript>();
			if(light.ID == id){
				{
					print ("Executing void.");
					light.switchlight(State);

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

}
