using UnityEngine;
using System.Collections;

public class Map05 : IMapData {
	Vector3[] cubePos = new Vector3[]{
		new Vector3(3,0,3),
		new Vector3(4,0,3),
		new Vector3(5,0,3),

		new Vector3(2,-1,8),
		new Vector3(2,-1,7),
		new Vector3(2,-1,6),
		new Vector3(2,-1,5),
		new Vector3(2,-1,4),
		new Vector3(2,-1,3),
		new Vector3(2,-1,2),
		new Vector3(3,-1,2),
		new Vector3(4,-1,2),
		new Vector3(5,-1,2),
		new Vector3(6,-1,2),
		new Vector3(7,-1,2),
		new Vector3(8,-1,2),

		new Vector3(2,0,8),
		new Vector3(2,0,7),
		new Vector3(2,0,6),
		new Vector3(2,0,5),
		new Vector3(2,0,4),
		new Vector3(2,0,3),
		new Vector3(2,0,2),
		new Vector3(3,0,2),
		new Vector3(4,0,2),
		new Vector3(5,0,2),
		new Vector3(6,0,2),
		new Vector3(7,0,2),
		new Vector3(8,0,2),

		new Vector3(3,1,8),
		new Vector3(3,1,7),
		new Vector3(3,1,6),
		new Vector3(3,1,5),
		new Vector3(3,1,4),
		new Vector3(3,1,3),
		new Vector3(4,1,3),
		new Vector3(5,1,3),
		new Vector3(6,1,3),
		new Vector3(7,1,3),

		new Vector3(3,2,8),
		new Vector3(3,2,7),
		new Vector3(3,2,6),
		new Vector3(3,2,4),
		new Vector3(3,2,3),
		new Vector3(4,2,3),
		new Vector3(5,2,3),
		new Vector3(6,2,3),
		new Vector3(7,2,3),
		new Vector3(8,2,3),

		new Vector3(6,3,3),
		new Vector3(6,4,3),
		new Vector3(6,5,3),
		new Vector3(5,5,3),

		new Vector3(7,8,5),
		new Vector3(6,8,5),
		new Vector3(5,8,5),
		new Vector3(4,8,5),
		new Vector3(3,8,5),
		new Vector3(2,8,5),

		new Vector3(3,6,6),
		new Vector3(3,5,6),
		new Vector3(3,4,6),
		new Vector3(3,3,6),

		new Vector3(4,2,6),
		new Vector3(5,2,6),
		new Vector3(6,2,6),
		new Vector3(7,2,6),
	};
	
	Vector3[] slopePos = new Vector3[]{
		new Vector3(8,1,2),
		new Vector3(6,6,3),
		new Vector3(6,6,4),
		new Vector3(6,7,4),
		new Vector3(6,7,5),
		new Vector3(4,5,3),
	};
	
	Quaternion[] slopeRot = new Quaternion[]{
		Quaternion.Euler(0,270,0),
		Quaternion.Euler(0,270,0),
		Quaternion.Euler(0,270,180),
		Quaternion.Euler(0,270,0),
		Quaternion.Euler(0,270,180),
		Quaternion.Euler(90,0,0),
	};
	
	RotatingObject[] rotObj = new RotatingObject[]{
		new RotatingObject("C302", new Vector3(0,0,2), new Vector3(1,0,0), true),
		new RotatingObject("C402", new Vector3(0,0,2), new Vector3(1,0,0), true),
		new RotatingObject("C502", new Vector3(0,0,2), new Vector3(1,0,0), true),
	};
	
	Vector3 playerStart = new Vector3(2, 0, 7);
	
	public Vector3[] CubePos{ get{ return cubePos; } }
	public Vector3[] SlopePos{ get{ return slopePos; } }
	public Quaternion[] SlopeRot{ get{ return slopeRot; } }
	public RotatingObject[] RotObj{ get{ return rotObj; } }
	public Vector3 PlayerStart{ get{ return playerStart; } }
	
}
