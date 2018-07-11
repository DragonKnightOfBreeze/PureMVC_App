/***
 * 标题：
 * 视图层，用户信息窗体
 * 
 * 功能：
 * 显示用户的单条信息（为了查看、更新）
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
	/// 视图层，用户列表信息
	/// </summary>
	public class UserForm : MonoBehaviour {

		//委托
		public Action Act_Confirm;

		//用户控件
		public InputField Inp_FirstName;
		public InputField Inp_LastName;
		public Toggle Tog_Male;
		public Toggle Tog_Female;
		public InputField Inp_Department;
		public InputField Inp_PhoneNum;
		public InputField Inp_Email;
		public Button Btn_Confirm;

		//用户实体类
		private UserVO _UserVO;

		//只读属性
		public UserVO UserVO {
			get { return _UserVO; }
		}



		void Start() {
			//注册按钮事件
			Btn_Confirm.onClick.AddListener(HandleBtn_Confirm);
			//冻结确认按钮
			Btn_Confirm.interactable = false;

			//监听
			Inp_FirstName.onValueChanged.AddListener(ClickInputControl);
		}

		/// <summary>
		/// 显示用户窗体信息
		/// </summary>
		/// <param name="userVOObj"></param>
		public void ShowUserFormInfo(UserVO userVOObj){
			if (userVOObj == null)
				return;
			_UserVO = userVOObj;

			//控件显示
			Inp_FirstName.text = userVOObj.FirstName;
			Inp_LastName.text = userVOObj.LastName;
			if (userVOObj.Sex == PersonSex.Male) {
				Tog_Male.isOn = true;
			} else {
				Tog_Female.isOn = true;
			}

			Inp_Department.text = userVOObj.Department;
			Inp_PhoneNum.text = userVOObj.PhoneNum;
			Inp_Email.text = userVOObj.Email;

		}

		/// <summary>
		/// 显示用户窗体信息
		/// </summary>
		/// <param name="userFormType"></param>
		/// <param name="userVO"></param>
		public void ShowUserForm(UserFormType userFormType, UserVO userVO){
			switch (userFormType) {
				case UserFormType.Create:
					//空，清空窗体内控件的内容
					ClearForm();
					break;
				case UserFormType.Update:
					//显示窗体信息
					ShowUserFormInfo(userVO);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 清空窗体
		/// </summary>
		public void ClearForm() {
			_UserVO = null;
			Inp_FirstName.text = "";
			Inp_LastName.text = "";
			Tog_Male.isOn = true;
			Inp_Department.text = "";
			Inp_PhoneNum.text = "";
			Inp_Email.text = "";
			//冻结确认按钮
			Btn_Confirm.interactable = false;
		}



		#region 【私有方法】

		/// <summary>
		/// 处理确认按钮
		/// </summary>
		private void HandleBtn_Confirm(){
			//窗体数据合法性检查
			if (!CheckUserForm())
				return;

			//调用委托（非空）
			Act_Confirm?.Invoke();
		}

		/// <summary>
		/// 检查用户窗体信息（即不允许用户输入空信息）
		/// </summary>
		/// <returns></returns>
		private bool CheckUserForm() {
			if (_UserVO == null) {
				_UserVO = new UserVO();
			}
			//获取数据
			//TODO：这段代码是不是多此一举或者需要重构？？？
			_UserVO.FirstName = Inp_FirstName.text;
			_UserVO.LastName = Inp_LastName.text;
			_UserVO.Sex = Tog_Male.isOn ? PersonSex.Male : PersonSex.Female;
			_UserVO.Department = Inp_Department.text;
			_UserVO.PhoneNum = Inp_PhoneNum.text;
			_UserVO.Email = Inp_Email.text;

			return _UserVO.IsValid;
		}

		/// <summary>
		/// 检查控件输入信息
		/// </summary>
		private void ClickInputControl(string input){
			if (string.IsNullOrWhiteSpace(input)) {
				//解冻
				Btn_Confirm.interactable = true;
			}
		}

		#endregion



	}
}