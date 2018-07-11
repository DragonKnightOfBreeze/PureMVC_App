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
using PureMVC.Patterns.Facade;
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

		//用户实体类（当前选择的）
		private UserVO _CurUserVO;
		//选择控件
		private Toggle _ToggleUserItem;

		void Start(){
			//得到Toggle引用
			_ToggleUserItem = this.GetComponent<Toggle>();
			//注册Toggle事件
			_ToggleUserItem.onValueChanged.AddListener(OnValueChangedByToggle);
		}





		/// <summary>
		/// 显示用户列表中的一条记录
		/// </summary>
		/// <param name="userData"></param>
		public void ShowUserListItem(UserVO userData){
			if (userData == null)
				return;
			//保存当前信息
			_CurUserVO = userData;

			//显示内容
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

		/// <summary>
		/// 用户选择事件
		/// </summary>
		/// <param name="isSelected">是否已被选择</param>
		private void OnValueChangedByToggle(bool isSelected){
			if (isSelected) {
				//要把用户选择的信息，发送到目标位置（列表信息Mediator）
				Facade.GetInstance(()=>new AppFacade()).SendNotification(ProConsts.MSG_Not_SendCurUserInfoToMediator,_CurUserVO);

			}
		}


	}
}