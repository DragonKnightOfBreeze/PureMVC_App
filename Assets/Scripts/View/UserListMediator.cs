
/***
 * 标题：
 * 视图层，用户列表信息操作类
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
using UnityEngine;
//using static PureMVCApp.ProConsts;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 视图层，用户列表信息操作类
	/// </summary>
	public class UserListMediator : Mediator {

		//字段
		private new const string NAME = "UserListMediator";
		private UserProxy _UserProxy;
		//当前用户选择的记录
		private UserVO _CurUserVO;

		//只读属性
		private UserList _UserListProp {
			//相当于模型层的data
			get { return base.ViewComponent as UserList; }	
		}

		/// <summary>
		/// 空的构造方法
		/// （让mediator一开始就可以注册，通过在其他类发送来初始化）
		/// </summary>
		public UserListMediator() : base(NAME){}



		/// <summary>
		/// 初始化用户列表数据
		/// </summary>
		/// <param name="userListObj"></param>
		internal void InitUserListMediator(UserList userListObj){
			if (userListObj == null)
				return;

			base.MediatorName = NAME;
			base.ViewComponent = userListObj;

			//委托注册
			userListObj.NewUser += HandleNewUser;
			userListObj.DeleteUser += HandleDeleteUser;
			//加载显示初始视图内容
			userListObj.LoadAndShowUserListInfo(_UserProxy.Users);
		}

		/// <summary>
		/// 本类实例在注册时触发
		/// </summary>
		public override void OnRegister(){
			//得到“用户代理”引用
			_UserProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
		}

		/// <summary>
		/// 列举出所有关注的消息
		/// </summary>
		/// <returns></returns>
		public override string[] ListNotificationInterests(){
			string[] strList = new string[] {
				ProConsts.MSG_Not_InitUserListMediator,
				ProConsts.MSG_Not_SendCurUserInfoToMediator,
				ProConsts.MSG_Not_AddUserInfoToList,
				ProConsts.MSG_Not_UpdateUserInfoToList
			};
			return strList;
		}

		/// <summary>
		/// 处理所有关注的消息
		/// </summary>
		/// <param name="notification"></param>
		public override void HandleNotification(INotification notification){
			switch (notification.Name) {
				//初始化
				case ProConsts.MSG_Not_InitUserListMediator:
					//调用初始化方法
					UserList userListObj = notification.Body as UserList;
					InitUserListMediator(userListObj);
					break;

				//处理用户选择的数据积记录
				case ProConsts.MSG_Not_SendCurUserInfoToMediator:
					UserVO userVO = notification.Body as UserVO;
					HandleSelectedUserInfo(userVO);
					break;

				//从UserFormMediator发来的消息：增加新用户
				case ProConsts.MSG_Not_AddUserInfoToList:
					UserVO userVOObj = notification.Body as UserVO;
					SubmitNewUser(userVOObj);
					break;

				//从UserFormMediator发来的消息：更新用户
				case ProConsts.MSG_Not_UpdateUserInfoToList:
					UserVO userVOObj2 = notification.Body as UserVO;
					SubmitUpdateUserInfo(userVOObj2);
					break;


				default:
					break;
			}
		}


		#region 【私有方法】

		/// <summary>
		/// 处理新用户操作
		/// </summary>
		private void HandleNewUser(){
			//发送消息给“用户（窗体）表单”视图
			SendNotification(ProConsts.MSG_Not_AddUserInfo);
		}

		/// <summary>
		/// 处理“删除用户”操作
		/// </summary>
		private void HandleDeleteUser(){
			if (_UserProxy == null || _CurUserVO == null)
				return;

			//代理层，调用删除方法
			_UserProxy.DeleteUserItems(_CurUserVO);
			//刷新用户列表
			_UserListProp.LoadAndShowUserListInfo(_UserProxy.Users);
			//通知“用户窗体”操作类，清空窗体信息
			SendNotification(ProConsts.MSG_Not_ClearUserInfo);
		}

		/// <summary>
		/// 提交（处理）增加新用户操作
		/// </summary>
		/// <param name="newUserVO"></param>
		private void SubmitNewUser(UserVO newUserVO){
			//调用模型层代理方法，增加一条记录
			if (_UserProxy == null || newUserVO == null)
				return;

			_UserProxy.AddUserItem(newUserVO);
			//刷新窗体信息
			_UserListProp.LoadAndShowUserListInfo(_UserProxy.Users);
		}

		/// <summary>
		/// 提交（处理）更新用户信息操作
		/// </summary>
		/// <param name="updateUserInfo"></param>
		private void SubmitUpdateUserInfo(UserVO updateUserInfo){
			if (_UserProxy == null || updateUserInfo == null)
				return;

			_UserProxy.UpdateUserItems(updateUserInfo);
			//刷新窗体信息
			_UserListProp.LoadAndShowUserListInfo(_UserProxy.Users);
		}
		

		private void HandleSelectedUserInfo(UserVO userVO){
			if (userVO == null)
				return;

			//保存当前用户选择的记录
			_CurUserVO = userVO;
			//显示窗体的“删除”按钮
			_UserListProp.UnfreezeBtn_Delete();
			//发送消息到“用户信息窗体”类
			SendNotification(ProConsts.MSG_Not_SendCurUserInfoToUserForm,_CurUserVO);
		}

		

		#endregion




	}
}