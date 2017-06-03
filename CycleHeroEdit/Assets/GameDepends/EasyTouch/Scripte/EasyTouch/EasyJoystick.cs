// EasyTouch library is copyright (c) of Hedgehog Team
// Please send feedback or bug reports to the.hedgehog.team@gmail.com
using UnityEngine;
using System.Collections;

/// <summary>
/// Release notes:
/// 
/// V1.0 November 2012
/// =============================
/// 	- First release



/// <summary>
/// Easy joystick allow to quickly create a virtual joystick
/// </summary>
public class EasyJoystick : MonoBehaviour {
	
	#region Enumeration
	/// <summary>
	/// Properties influenced by the joystick
	/// </summary>
	public enum PropertiesInfluenced {Rotate, RotateLocal,Translate, TranslateLocal, Scale}
	/// <summary>
	/// Axis influenced by the joystick
	/// </summary>
	public enum AxisInfluenced{X,Y,Z,XYZ}
	/// <summary>
	/// Dynamic area zone.
	/// </summary>
	public enum DynamicArea {FullScreen, Left,Right,Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight};
	/// <summary>
	/// Interaction type.
	/// </summary>
	public enum InteractionType {Direct, Include}

	/// <summary>
	/// Message name.
	/// </summary>
	private enum MessageName{ On_JoystickMove, On_JoystickMoveEnd};
	#endregion
	
	#region Public Joystick return values read only property
	private Vector2 joystickAxis;
	/// <summary>
	/// Gets the joystick axis value between -1 & 1...
	/// </summary>
	/// <value>
	/// The joystick axis.
	/// </value>
	public Vector2 JoystickAxis {
		get {
			return this.joystickAxis;
		}
	}	
	#endregion
	
	#region public members
	public static EasyJoystick instance;	
	/// <summary>
	/// Enable or disable the joystick.
	/// </summary>
	public bool enable = true;
	/// <summary>
	/// Use fixed update.
	/// </summary>
	public bool useFixedUpdate = false;
	
	/// <summary>
	/// The zone radius size.
	/// </summary>
	public float zoneRadius=100f;
	
	[SerializeField]
	private float touchSize = 30;
	/// <summary>
	/// Gets or sets the size of the touch.
	/// 
	/// </summary>
	/// <value>
	/// The size of the touch.
	/// </value>
	public float TouchSize {
		get {
			return this.touchSize;
		}
		set {
			touchSize = value;
			if (touchSize>zoneRadius/2 && restrictArea){
				touchSize =zoneRadius/2; 	
			}
		}
	}
	
	/// <summary>
	/// The dead zone size. While the touch is in this area, the joystick is considered stalled
	/// </summary> 
	public float deadZone=20;
	
	[SerializeField]
	private bool dynamicJoystick=false;
	/// <summary>
	/// Gets or sets a value indicating whether this is a dynamic joystick.
	/// When this option is enabled, the joystick will display the location of the touch
	/// </summary>
	/// <value>
	/// <c>true</c> if dynamic joystick; otherwise, <c>false</c>.
	/// </value>
	public bool DynamicJoystick {
		get {
			return this.dynamicJoystick;
		}
		set {
			joystickIndex=-1;
			dynamicJoystick = value;
			if (dynamicJoystick){
				virtualJoystick=false;
			}
			else{
				virtualJoystick=true;
				joystickCenter = joystickPosition;
			}
		}
	}
	
	/// <summary>
	/// When the joystick is dynamic mode, this value indicates the area authorized for display
	/// </summary>
	public DynamicArea area = DynamicArea.FullScreen;
	
	/// <summary>
	/// The joystick position on the screen
	/// </summary>
	public Vector2 joystickPosition = new Vector2( 135f,135f);
	
	[SerializeField]
	private bool restrictArea=false;
	/// <summary>
	/// Gets or sets a value indicating whether the touch must be in the radius area.
	/// </summary>
	/// <value>
	/// <c>true</c> if restrict area; otherwise, <c>false</c>.
	/// </value>
	public bool RestrictArea {
		get {
			return this.restrictArea;
		}
		set {
			restrictArea = value;
			if (restrictArea){
				touchSizeCoef = touchSize;
			}
			else{
				touchSizeCoef=0;	
			}
		}
	}	
	
	
	// Messaging
	/// <summary>
	/// The enable smoothing.When smoothing is enabled, resets the joystick slowly in the start position
	/// </summary>
	public bool enableSmoothing = false;
	
	[SerializeField]
	private Vector2 smoothing = new Vector2(2f,2f);
	/// <summary>
	/// Gets or sets the smoothing values
	/// </summary>
	/// <value>
	/// The smoothing.
	/// </value>
	public Vector2 Smoothing {
		get {
			return this.smoothing;
		}
		set {
			smoothing = value;
			if (smoothing.x<0.1f){
				smoothing.x=0.1f;
			}
			if (smoothing.y<0.1){
				smoothing.y=0.1f;	
			}
		}
	}
	
	/// <summary>
	/// The enable inertia. Inertia simulates sliding movements (like a hovercraft, for example)
	/// </summary>
	public bool enableInertia = false;
	
	[SerializeField]
	public Vector2 inertia = new Vector2(100,100);
	/// <summary>
	/// Gets or sets the inertia values
	/// </summary>
	/// <value>
	/// The inertia.
	/// </value>
	public Vector2 Inertia {
		get {
			return this.inertia;
		}
		set {
			inertia = value;
			if (inertia.x<=0){
				inertia.x=1;
			}
			if (inertia.y<=0){
				inertia.y=1;
			}
			
		}
	}	

	///----------------------------------------------------------------------------------
	/// <summary>
	/// 得到输入区域
	/// </summary> 
	///----------------------------------------------------------------------------------
	public Rect GetInputArea()
	{
		return new Rect ();
	}


	// Helper
	public bool showZone = true;
	public bool showTouch = true;
	public bool showDeadZone = true;
	public Texture areaTexture;
	public Texture touchTexture;
	public Texture deadTexture;
	#endregion
	
	/// <summary>
	/// The speed of each joystick axis
	/// </summary>
	public Vector2 speed;
	
	/// <summary>
	/// The interaction.
	/// </summary>
	public  InteractionType interaction = InteractionType.Direct;

	#region private members
	// Joystick properties
	private Vector2 		joystickCenter;
	private Vector2 		joyTouch;
	private bool 			virtualJoystick = true;
	private int 			joystickIndex=-1;
	private float 			touchSizeCoef=0;
	#endregion
	
	#region Inspector
	public bool showProperties=true;
	public bool showInteraction=true;
	public bool showAppearance=true;
	#endregion

	#region Monobehaviour methods
	void Start(){
					
		if (!dynamicJoystick){
			joystickCenter = joystickPosition;
			virtualJoystick = true;
			instance = this;
		}
		else{
			virtualJoystick = false;	
		}
	}

	public void UpdateJoystick( Finger gesture )
	{
		if (Application.isPlaying){
					
			if (!IsRectUnderTouch (gesture))
				return;

			joystickIndex = gesture.fingerIndex;

			// Reset to initial position
			if (!enableSmoothing)
			{
				joyTouch = Vector2.zero;
			}
			else
			{ 
				if (joyTouch.sqrMagnitude>0.1)
				{
					joyTouch = new Vector2( joyTouch.x - joyTouch.x*smoothing.x*Time.deltaTime, joyTouch.y - joyTouch.y*smoothing.y*Time.deltaTime);	
				}
				else
				{
					joyTouch = Vector2.zero;
				}

				if ((joyTouch/(zoneRadius-touchSizeCoef)).sqrMagnitude > 1)
				{
					joyTouch.Normalize();
					joyTouch *= zoneRadius-touchSizeCoef;
				}
			}

			
			// Joystick Axis 
			if (joyTouch.sqrMagnitude>deadZone*deadZone){
				
				joystickAxis = Vector2.zero;
				if (Mathf.Abs(joyTouch.x)> deadZone){
					joystickAxis = new Vector2( (joyTouch.x -(deadZone*Mathf.Sign(joyTouch.x)))/(zoneRadius-touchSizeCoef-deadZone),joystickAxis.y);
				}
				else{
					joystickAxis = new Vector2( joyTouch.x /(zoneRadius-touchSizeCoef),joystickAxis.y);
				}
				if (Mathf.Abs(joyTouch.y)> deadZone){
					joystickAxis = new Vector2( joystickAxis.x,(joyTouch.y-(deadZone*Mathf.Sign(joyTouch.y)))/(zoneRadius-touchSizeCoef-deadZone));
				}
				else{
					joystickAxis = new Vector2( joystickAxis.x,joyTouch.y/(zoneRadius-touchSizeCoef));	
				}
			
			}
			else{
				joystickAxis = new Vector2(0,0);	
			}
		}		
	}
	
	void OnGUI(){
					
		// area zone
		if ((showZone && areaTexture!=null && !dynamicJoystick && enable) || (showZone && dynamicJoystick && virtualJoystick && areaTexture!=null && enable)  ){
			GUI.DrawTexture( new Rect(joystickCenter.x -zoneRadius ,Screen.height- joystickCenter.y-zoneRadius,zoneRadius*2,zoneRadius*2), areaTexture,ScaleMode.ScaleToFit,true);
		}
		
		// area touch
		if ((showTouch && touchTexture!=null && !dynamicJoystick && enable)|| (showTouch && dynamicJoystick && virtualJoystick && touchTexture!=null &enable) ){
			GUI.DrawTexture( new Rect(joystickCenter.x+(joyTouch.x -touchSize) ,Screen.height-joystickCenter.y-(joyTouch.y+touchSize),touchSize*2,touchSize*2), touchTexture,ScaleMode.ScaleToFit,true);
			
		}	

		// dead zone
		if ((showDeadZone && deadTexture!=null && !dynamicJoystick && enable)|| (showDeadZone && dynamicJoystick && virtualJoystick && deadTexture!=null && enable) ){
			GUI.DrawTexture( new Rect(joystickCenter.x -deadZone,Screen.height-joystickCenter.y-deadZone,deadZone*2,deadZone*2), deadTexture,ScaleMode.ScaleToFit,true);
			
		}	
	}
	#endregion

	Vector3 GetInfluencedAxis(AxisInfluenced axisInfluenced){
		
		Vector3 axis = Vector3.zero;
		switch(axisInfluenced){
			case AxisInfluenced.X:
				axis = Vector3.right;
				break;
			case AxisInfluenced.Y:
				axis = Vector3.up;
				break;
			case AxisInfluenced.Z:
				axis = Vector3.forward;
				break;
			case AxisInfluenced.XYZ:
				axis = Vector3.one;
				break;
				
		}	
		
		return axis;
	}
	
	void DoActionDirect(Transform axisTransform, PropertiesInfluenced inlfuencedProperty,Vector3 axis, float sensibility, CharacterController charact){
		switch(inlfuencedProperty){
			case PropertiesInfluenced.Rotate:
				axisTransform.Rotate( axis * sensibility * Time.deltaTime,Space.World);
				break;	
			case PropertiesInfluenced.RotateLocal:
				axisTransform.Rotate( axis * sensibility * Time.deltaTime,Space.Self);
				break;
			case PropertiesInfluenced.Translate:
				if (charact==null){
					axisTransform.Translate(axis * sensibility * Time.deltaTime,Space.World);
				}
				else{
					charact.Move( axis * sensibility * Time.deltaTime );
				}
				break;
			case PropertiesInfluenced.TranslateLocal:
				if (charact==null){
					axisTransform.Translate(axis * sensibility * Time.deltaTime,Space.Self);
				}
				else{
					charact.Move( charact.transform.TransformDirection(axis) * sensibility * Time.deltaTime );
				}
				break;	
			case PropertiesInfluenced.Scale:
				axisTransform.localScale +=  axis * sensibility * Time.deltaTime;
				break;
		}
		
	}

	public bool IsRectUnderTouch( Finger gesture ){

		if (gesture.position.x < joystickCenter.x - zoneRadius)
			return false;
		if (gesture.position.x > joystickCenter.x + zoneRadius)
			return false;
		if (gesture.position.y < joystickCenter.y - zoneRadius)
			return false;
		if (gesture.position.y > joystickCenter.y + zoneRadius)
			return false;
		
		return true;
	}

}
