#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
/*******************************************************************************
The content of this file includes portions of the proprietary AUDIOKINETIC Wwise
Technology released in source code form as part of the game integration package.
The content of this file may not be used without valid licenses to the
AUDIOKINETIC Wwise Technology.
Note that the use of the game engine is subject to the Unity(R) Terms of
Service at https://unity3d.com/legal/terms-of-service
 
License Usage
 
Licensees holding valid licenses to the AUDIOKINETIC Wwise Technology may use
this file in accordance with the end user license agreement provided with the
software or, alternatively, in accordance with the terms contained
in a written agreement between you and Audiokinetic Inc.
Copyright (c) 2024 Audiokinetic Inc.
*******************************************************************************/

[UnityEngine.AddComponentMenu("Wwise/AkState")]
[UnityEngine.DefaultExecutionOrder(-20)]
/// @brief This will call \c AkSoundEngine.SetState() whenever the selected Unity event is triggered. For example this component could be set on a Unity collider to trigger when an object enters it.
/// \sa 
/// - <a href="https://www.audiokinetic.com/library/edge/?source=SDK&id=soundengine__states.html" target="_blank">Integration Details - States</a> (Note: This is described in the Wwise SDK documentation.)
public class AkState : AkDragDropTriggerHandler
#if UNITY_EDITOR
	, AK.Wwise.IMigratable
#endif
{
	public AK.Wwise.State data = new AK.Wwise.State();
	protected override AK.Wwise.BaseType WwiseType { get { return data; } }

	public override void HandleEvent(UnityEngine.GameObject in_gameObject)
	{
		data.SetValue();
	}

	#region Obsolete
	[System.Obsolete(AkSoundEngine.Deprecation_2018_1_6)]
	public int valueID { get { return (int)(data == null ? AkSoundEngine.AK_INVALID_UNIQUE_ID : data.Id); } }

	[System.Obsolete(AkSoundEngine.Deprecation_2018_1_6)]
	public int groupID { get { return (int)(data == null ? AkSoundEngine.AK_INVALID_UNIQUE_ID : data.GroupId); } }

	[System.Obsolete(AkSoundEngine.Deprecation_2018_1_6)]
	public byte[] valueGuid
	{
		get
		{
			if (data == null)
				return null;

			var objRef = data.ObjectReference;
			return !objRef ? null : objRef.Guid.ToByteArray();
		}
	}

	[System.Obsolete(AkSoundEngine.Deprecation_2018_1_6)]
	public byte[] groupGuid
	{
		get
		{
			if (data == null)
				return null;

			var objRef = data.GroupWwiseObjectReference;
			return !objRef ? null : objRef.Guid.ToByteArray();
		}
	}
	#endregion

	#region WwiseMigration
#pragma warning disable 0414 // private field assigned but not used.
	[UnityEngine.HideInInspector]
	[UnityEngine.SerializeField]
	[UnityEngine.Serialization.FormerlySerializedAs("valueID")]
	private int valueIdInternal;
	[UnityEngine.HideInInspector]
	[UnityEngine.SerializeField]
	[UnityEngine.Serialization.FormerlySerializedAs("groupID")]
	private int groupIdInternal;
	[UnityEngine.HideInInspector]
	[UnityEngine.SerializeField]
	[UnityEngine.Serialization.FormerlySerializedAs("valueGuid")]
	private byte[] valueGuidInternal;
	[UnityEngine.HideInInspector]
	[UnityEngine.SerializeField]
	[UnityEngine.Serialization.FormerlySerializedAs("groupGuid")]
	private byte[] groupGuidInternal;
#pragma warning restore 0414 // private field assigned but not used.

#if UNITY_EDITOR
	bool AK.Wwise.IMigratable.Migrate(UnityEditor.SerializedObject obj)
	{
		if (!AkUtilities.IsMigrationRequired(AkUtilities.MigrationStep.WwiseTypes_v2018_1_6))
			return false;

		return AK.Wwise.TypeMigration.ProcessDoubleGuidType(obj.FindProperty("data.WwiseObjectReference"), WwiseObjectType.State,
			obj.FindProperty("valueGuidInternal"), obj.FindProperty("valueIdInternal"),
			obj.FindProperty("groupGuidInternal"), obj.FindProperty("groupIdInternal"));
	}
#endif

	#endregion
}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.