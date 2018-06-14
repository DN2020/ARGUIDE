using UnityEngine;
using System.Collections;

//This is a basic interface with a single required
//method.
public interface IDataConsumer
{
	void RequestData (string request, string payload);
	string getPayLoad ();
	string getRequest ();
	void ProcessPayload (string payload);
}

