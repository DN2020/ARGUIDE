using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class  MuseumLocationsList
{
	public List< waypoint>  waypoints;

}



/* [System.Serializable]
public class  MuseumLocationsList
{
	public List< waypoint>  waypoints;

}

*/

[System.Serializable]
public class  WayPointList
{
	public List< waypoint>  waypoints;

}





[System.Serializable]
public class waypoint {



	public string x;
	public string y;
	public string z;


	public float xf;
	public float yf;
	public float zf;



	public string imaged_ind;
	public string image_uri;


}    
