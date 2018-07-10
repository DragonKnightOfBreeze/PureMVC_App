/***
 * 标题：
 * 视图层，用户列表信息脚本
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
	/// 视图层，用户列表信息脚本
	/// </summary>
	public class UserList : MonoBehaviour {

		//系统委托
		//职责的分离
		public Action NewUser;		//新建用户
		public Action SelectUser;	//选择用户信息
		public Action DeleteUser;	//删除用户信息

		//用户列表
		public UserListItem UserListItemPrefab;
		//文本控件
		public Text Txt_UserListNum;	//用户列表数量
		//按钮控件
		public Button Btn_New;		//新用户
		public Button Btn_Delete;	//删除用户

		//用户列表信息集合（建议定义集合类时，一开始就初始化）
		private List<UserListItem> _UserListInfo = new List<UserListItem>();

		void Start(){
			//初始化字段
			Txt_UserListNum.text = "0";

			//设置隐藏
			UserListItemPrefab.gameObject.SetActive(false);
			//按钮事件的注册
			Btn_New.onClick.AddListener(ClickBtn_New);
			Btn_Delete.onClick.AddListener(ClickBtn_Delete);
		}

		/// <summary>
		/// 提取显示用户列表信息
		/// </summary>
		/// <param name="listInfo">需要显示的消息集合</param>
		public void LoadAndShowUserListInfo(IList<UserVO> userVOs){
			//清空列表信息
			ClearItems();
			//克隆与显示列表信息
			foreach (var userVO in userVOs) {
				UserListItem item = CloneUserVOInfo();
				item.DisplayUserListItem(userVO);
				//加入集合保存
				_UserListInfo.Add(item);
			}
			//统计数量
			Txt_UserListNum.text = _UserListInfo.Count.ToString();
		}

		/// <summary>
		/// 冻结删除按钮
		/// </summary>
		public void FreezeBtn_Delete(){
			Btn_Delete.interactable = false;
		}

		/// <summary>
		/// 解冻删除按钮
		/// </summary>
		public void UnfreezeBtn_Delete() {
			Btn_Delete.interactable = true;
		}

		#region 【私有方法】

		private void ClickBtn_New(){
			NewUser?.Invoke();	//调用委托
		}

		private void ClickBtn_Delete(){
			DeleteUser?.Invoke();	//调用委托
		}

		/// <summary>
		/// 清空列表信息
		/// </summary>
		private void ClearItems(){
			if (_UserListInfo == null)
				return;

			foreach (var item in _UserListInfo) 
				//清空对应的游戏对象
				Destroy(item.gameObject);
			//清空集合洪震南的数据
			_UserListInfo.Clear();
		}

		private UserListItem CloneUserVOInfo(){
			UserListItem item = null;
			//参数检查
			if (UserListItemPrefab != null) {
				item = Instantiate(UserListItemPrefab);
				//设置父节点
				item.transform.SetParent(UserListItemPrefab?.transform.parent);
				//显示克隆的节点
				item.gameObject.SetActive(true);
				//定义尺寸与相对位置（有组件保证不重叠）
				item.transform.localScale = Vector3.one;
				item.transform.localPosition = Vector3.zero;
			}
			return item;
		}



		#endregion

	}
}