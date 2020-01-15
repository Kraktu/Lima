using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
	public string resourceName;
	public double totalResource = 0;
	public double resourcePerSec = 1;
	public double resourcePerClick = 1;

    public Resource(string name, double rps, double rpc, double total=0)
    {
        resourceName = name;
        totalResource = total;
        resourcePerSec = rps;
        resourcePerClick = rpc;
    }
}
