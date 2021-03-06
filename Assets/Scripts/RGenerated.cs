// ----- Generated by R.Unity ----- //
using UnityEngine;

namespace R {
	public static class Tag {
		public static readonly string Untagged = "Untagged";
		public static readonly string Respawn = "Respawn";
		public static readonly string Finish = "Finish";
		public static readonly string EditorOnly = "EditorOnly";
		public static readonly string MainCamera = "MainCamera";
		public static readonly string Player = "Player";
		public static readonly string GameController = "GameController";
	}

	public static class Scene {
		public const string SampleScene = "SampleScene";
	}

	public static class Layer {
		public static readonly string Default = "Default";
		public static readonly LayerMask DefaultMask = 1 << 0;
		public static readonly string TransparentFX = "TransparentFX";
		public static readonly LayerMask TransparentFXMask = 1 << 1;
		public static readonly string IgnoreRaycast = "Ignore Raycast";
		public static readonly LayerMask IgnoreRaycastMask = 1 << 2;
		public static readonly string Water = "Water";
		public static readonly LayerMask WaterMask = 1 << 4;
		public static readonly string UI = "UI";
		public static readonly LayerMask UIMask = 1 << 5;
	}

	public static class Animator {
	}

	public static class SortingLayer {
		public static readonly string Default = "Default";
		public static readonly string[] All = new string[]{ Default };
	}

}
