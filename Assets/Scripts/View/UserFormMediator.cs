/***
 * 标题：
 * 用户信息Mediator
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
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using PureMVCApp;
using UnityEngine;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 用户表单类型
	/// </summary>
	public enum UserFormType {
		Create,
		Update
	}


	/// <summary>
	/// 用户表单Mediator
	/// </summary>
	public class UserFormMediator : Mediator {
		private new const string NAME = "UserFormMediator";

		//默认操作模式
		private UserFormType _UserFormType = UserFormType.Create;
		//只读属性
		private UserForm _UserFormProp {
			get { return base.ViewComponent as UserForm;}
		}

		/// <summary>
		/// 空的构造函数
		/// </summary>
		public UserFormMediator():base(NAME){}

		/// <summary>
		/// 列举出所有关注的消息
		/// </summary>
		/// <returns></returns>
		public override string[] ListNotificationInterests(){
			string[] strList = new string[] {
				ProConsts.MSG_Not_InitUserFormMediator,
				ProConsts.MSG_Not_SendCurUserInfoToUserForm,
				ProConsts.MSG_Not_AddUserInfo,
				ProConsts.MSG_Not_ClearUserInfo
			};
			return strList;
		}


		/// <summary>
		/// 处理所有关注的消息
		/// </summary>
		/// <param name="notification"></param>
		public override void HandleNotification(INotification notification){
			switch (notification.Name) {

				//初始化信息
				case ProConsts.MSG_Not_InitUserFormMediator:
					UserForm userFormObj = notification.Body as UserForm;
					InitUserFormMediator(userFormObj);
					break;

				//处理用户选择信息（更新数据）
				case ProConsts.MSG_Not_SendCurUserInfoToUserForm:
					_UserFormType = UserFormType.Update;
					UserVO userVoObj = notification.Body as UserVO; 
					DisplayAndUpdateUserForm(UserFormType.Update,userVoObj);
					break;
				
				//增加用户信息
				case ProConsts.MSG_Not_AddUserInfo:
					_UserFormType = UserFormType.Create;
					DisplayAndUpdateUserForm(UserFormType.Create);
					break;

				//清除用户信息（一条）
				case ProConsts.MSG_Not_ClearUserInfo:
					_UserFormType = UserFormType.Create;
					_UserFormProp.ClearForm();
					break;

				default: 
					break;
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="userFormObj"></param>
		internal void InitUserFormMediator(UserForm userFormObj) {
			if (userFormObj == null)
				return;
			base.MediatorName = NAME;
			base.ViewComponent = userFormObj;
			//委托注册
			//在这里注册！！！
			_UserFormProp.Act_Confirm = SubmitUserInfo;
		}

		/// <summary>
		/// 更新与显示用户的表单窗体信息
		/// </summary>
		/// <param name="userFormType"></param>
		/// <param name="userVOObj"></param>
		private void DisplayAndUpdateUserForm(UserFormType userFormType , UserVO userVOObj = null){
			_UserFormProp?.ShowUserForm(userFormType,userVOObj);
		}

		/// <summary>
		/// 提交：用户点击“确认按钮”，提交信息
		/// </summary>
		private void SubmitUserInfo(){
			switch (_UserFormType) {
				case UserFormType.Create:
					AddNewUserInfo();
					break;
				case UserFormType.Update:
					UpdateUserInfo(_UserFormProp?.UserVO);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 提交：增加新用户
		/// 功能：往用户列表Mediator发送消息，增加一条新记录，且显示
		/// </summary>
		private void AddNewUserInfo(){
			SendNotification(ProConsts.MSG_Not_AddUserInfoToList,_UserFormProp.UserVO);
		}

		/// <summary>
		/// 提交：更新用户信息
		/// 功能：往用户列表Mediator发送消息，更新记录
		/// TODO：有必要带参数吗？
		/// </summary>
		private void UpdateUserInfo(UserVO userVOObj){
			SendNotification(ProConsts.MSG_Not_UpdateUserInfoToList,userVOObj);
		}

	}
}