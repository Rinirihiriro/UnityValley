using UnityEngine;
using System.Collections;

public class Map01 : IMapData {
	Vector3[] cubePos = new Vector3[]{
		new Vector3(0,0,0),
		new Vector3(0,0,1),
		new Vector3(0,0,2),
		
		new Vector3(0,0,2),
		new Vector3(0,0,3),
		new Vector3(0,0,4),
		new Vector3(0,0,5),
		new Vector3(-8,8,-2),
		new Vector3(-8,8,-1),
		
		new Vector3(-7,8,-1),
		new Vector3(-6,8,-1),
		new Vector3(-5,8,-1),
		
		new Vector3(0,1,0),
		new Vector3(0,2,0),
		new Vector3(0,3,0),
		
		new Vector3(0,4,0),
		new Vector3(0,5,0),
		new Vector3(0,6,0),
		new Vector3(0,7,0),
		
		new Vector3(0,7,1),
		new Vector3(0,7,2),
		new Vector3(0,7,3),
		new Vector3(0,7,4),
		new Vector3(0,7,5),
		new Vector3(0,7,6),
	};
	
	Vector3[] slopePos = new Vector3[]{
		new Vector3(-8,8,-3),
	};
	
	Quaternion[] slopeRot = new Quaternion[]{
		Quaternion.Euler(0,270,90),
	};
	
	RotatingObject[] rotObj = new RotatingObject[]{
		new RotatingObject("C040", new Vector3(0,7,0), new Vector3(0,0,1)),
		new RotatingObject("C050", new Vector3(0,7,0), new Vector3(0,0,1)),
		new RotatingObject("C060", new Vector3(0,7,0), new Vector3(0,0,1)),
		new RotatingObject("C070", new Vector3(0,7,0), new Vector3(0,0,1)),
		new RotatingObject("C071", new Vector3(0,7,0), new Vector3(0,0,1)),
		new RotatingObject("C072", new Vector3(0,7,0), new Vector3(0,0,1)),
		new RotatingObject("C073", new Vector3(0,7,0), new Vector3(0,0,1)),
		/*
		new RotatingObject("C040", new Vector3(0,7,0), new Vector3(0,0,1), true),
		new RotatingObject("C050", new Vector3(0,7,0), new Vector3(0,0,1), true),
		new RotatingObject("C060", new Vector3(0,7,0), new Vector3(0,0,1), true),
		new RotatingObject("C070", new Vector3(0,7,0), new Vector3(0,0,1), true),
		new RotatingObject("C071", new Vector3(0,7,0), new Vector3(0,0,1), true),
		new RotatingObject("C072", new Vector3(0,7,0), new Vector3(0,0,1), true),
		new RotatingObject("C073", new Vector3(0,7,0), new Vector3(0,0,1), true),
		*/
	};
	
	Vector3 playerStart = new Vector3(0, 0, 2);

	public Vector3[] CubePos{ get{ return cubePos; } }
	public Vector3[] SlopePos{ get{ return slopePos; } }
	public Quaternion[] SlopeRot{ get{ return slopeRot; } }
	public RotatingObject[] RotObj{ get{ return rotObj; } }
	public Vector3 PlayerStart{ get{ return playerStart; } }

}
