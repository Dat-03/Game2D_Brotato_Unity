﻿using UnityEngine;

namespace SlimUI.ModernMenu{
	[CreateAssetMenu(menuName = "ThemeSettings")]
	[System.Serializable]
	public class ThemedUIData : ScriptableObject {
		[System.Serializable]
		public class Custom1{
			[Header("Text")]	
			public Color graphic1;
			public Color32 text1;
		}

		[System.Serializable]
		public class Custom2{
			[Header("Text")]	
			public Color graphic2;
			public Color32 text2;
		}

		[System.Serializable]
		public class Custom3{
			[Header("Text")]	
			public Color graphic3;
			public Color32 text3;
		}
        [System.Serializable]
        public class Custom4
        {
            [Header("Text")]
            public Color graphic4;
            public Color32 text4;
        }
        [System.Serializable]
        public class Custom5
        {
            [Header("Text")]
            public Color graphic5;
            public Color32 text5;
        }
        [System.Serializable]
        public class Custom6
        {
            [Header("Text")]
            public Color graphic6;
            public Color32 text6;
        }



        [Header("PRESETS")]
		public Custom1 custom1;
		public Custom2 custom2;
		public Custom3 custom3;
		public Custom4 custom4;
		public Custom5 custom5;
		public Custom6 custom6;

        [HideInInspector]
		public Color currentColor;
		[HideInInspector]
		public Color32 textColor;
	}
}