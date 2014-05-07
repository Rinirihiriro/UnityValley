using UnityEngine;
using System.Collections;

public class Map02 : IMapData {
	Vector3[] cubePos = new Vector3[]{
		new Vector3(0,3,7),
		new Vector3(0,3,6),
		new Vector3(0,3,5),
		new Vector3(0,3,4),

		new Vector3(0,10,7),
		new Vector3(0,10,6),
		new Vector3(0,10,5),
		new Vector3(0,10,4),

		new Vector3(0,10,3),
		new Vector3(0,10,2),
		new Vector3(0,10,1),
		new Vector3(0,10,0),
		new Vector3(0,9,0),
		new Vector3(0,8,0),
		new Vector3(0,7,0),
		new Vector3(0,6,0),
		new Vector3(0,5,0),
		new Vector3(0,4,0),
		new Vector3(0,3,0),
		new Vector3(-1,3,0),
		new Vector3(-2,3,0),
		new Vector3(-3,3,0),

		new Vector3(4,3,0),
		new Vector3(5,3,0),
		new Vector3(-2,11,-8),
		new Vector3(-1,11,-8),
		new Vector3(-1,11,-7),
		new Vector3(-1,11,-6),
		new Vector3(-1,11,-5),

	};
	
	Vector3[] slopePos = new Vector3[]{
		new Vector3(-3,11,-8),
	};
	
	Quaternion[] slopeRot = new Quaternion[]{
		Quaternion.Euler(0,0,90),
	};
	
	RotatingObject[] rotObj = new RotatingObject[]{
		new RotatingObject("C0103", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C0102", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C0101", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C0100", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C090", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C080", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C070", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C060", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C050", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C040", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C030", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C-130", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C-230", new Vector3(0,0,0), new Vector3(0,1,0), true),
		new RotatingObject("C-330", new Vector3(0,0,0), new Vector3(0,1,0), true),
	};
	
	Vector3 playerStart = new Vector3(0, 3, 7);
	
	public Vector3[] CubePos{ get{ return cubePos; } }
	public Vector3[] SlopePos{ get{ return slopePos; } }
	public Quaternion[] SlopeRot{ get{ return slopeRot; } }
	public RotatingObject[] RotObj{ get{ return rotObj; } }
	public Vector3 PlayerStart{ get{ return playerStart; } }
	
}
