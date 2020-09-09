/**
 * Copyright (c) 2017 The Campfire Union Inc - All Rights Reserved.
 *
 * Licensed under the MIT license. See LICENSE file in the project root for
 * full license information.
 *
 * Email:   info@campfireunion.com
 * Website: https://www.campfireunion.com
 */

using UnityEngine;
using TMPro;
using VRStandardAssets.Utils;

namespace VRKeys {

	/// <summary>
	/// Enter key that calls Submit() on the keyboard.
	/// </summary>
	public class EnterKey : Key {

        #if NESTLE_RV
        private Login login;
        #endif

        [SerializeField]
        private TextMeshPro warningText;

        public override void HandleTriggerEnter (Collider other) {
			keyboard.Submit ();
        }

		public override void UpdateLayout (Layout translation) {
			label.text = translation.enterButtonLabel;
		}

        protected override void Update()
        {
            base.Update();
            if (alsoUseThisInKeyboard != KeyCode.F15 && Input.GetKeyDown(alsoUseThisInKeyboard))
                OnInteractionTrigger(InteractionModes.Click);
        }



        public override void OnInteractionTrigger(InteractionModes mode)
        {

#if NESTLE_RV
            DoLogin();
#endif            
           
        }

#if NESTLE_RV
        void DoLogin()
        {
            keyboard.ShowInfoMessage("Aguarde, estamos realizando o login...");
            //this.login = new Login(keyboard.text, warningText, keyboard);
            Login.Instance.username = keyboard.text;
            Login.Instance.keyboard = keyboard;
            Login.Instance.Run();
            //login.Run();
        }
#endif

    }
}