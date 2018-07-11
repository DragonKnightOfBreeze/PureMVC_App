/***
 * 标题：
 * 
 * 
 * 功能：
 * 
 * 
 * 用法：
 * 
 * 
 * 思路：
 * 
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 
	/// </summary>
	public class TestToggle : MonoBehaviour {

		public Toggle toggleObj;

		void Start(){
			toggleObj.onValueChanged.AddListener(OnValueChangedByToggle);
		}

		private void OnValueChangedByToggle(bool isChanged){
			if(isChanged)
				print("选择勾选");
			else {
				print("不选择勾选");
			}
		}



	}
}