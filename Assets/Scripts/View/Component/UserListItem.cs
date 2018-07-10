/***
 * 标题：
 * 视图层，用户列表中的一条记录
 * 
 * 功能：
 * 显示用户列表中的一条记录
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

namespace PureMVCApp{
	/// <summary>
	/// 
	/// </summary>
	public class UserListItem : MonoBehaviour {

		public Text Txt_UserName;
		public Text Txt_Sex;
		public Text Txt_Department;
		public Text Txt_PhoneNum;
		public Text Txt_Email;

		/// <summary>
		/// 显示用户列表中的一条记录
		/// </summary>
		/// <param name="userData"></param>
		public void DisplayUserListItem(UserVO userData){
			if (userData == null)
				return;

			if (Txt_UserName != null)
				Txt_UserName.text = userData.UserName;
			if (Txt_Sex != null)
				Txt_Sex.text = userData.Sex == PersonSex.Male?"男":"女";
			if (Txt_Department != null)
				Txt_Department.text = userData.Department;
			if (Txt_PhoneNum != null)
				Txt_PhoneNum.text = userData.PhoneNum;
			if (Txt_Email != null)
				Txt_Email.text = userData.Email;
		}


	}
}