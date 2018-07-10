/***
 * 标题：
 * 模型层代理类
 * 
 * 功能：
 * 针对数据实体层进行各种操作
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
using PureMVC.Patterns.Proxy;
using UnityEngine;

//using Kernel;
//using Global;

namespace PureMVCApp {
	/// <summary>
	/// 模型层代理类
	/// </summary>
	public class UserProxy:Proxy {
		public new const string NAME = "UserProxy";

		//只读属性
		public IList<UserVO> Users {
			get { return base.Data as IList<UserVO>; }
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public UserProxy():base(NAME,new List<UserVO>()){
			AddUserItem(new UserVO("太阳战士","索拉尔",PersonSex.Male,"太阳战士","罗德兰专线","白标蜡记石"));
			AddUserItem(new UserVO("太阳骑士", "王老菊", PersonSex.Male, "太阳战士", "古神专线", "白标蜡记石"));
			AddUserItem(new UserVO("月亮骑士", "黑桐谷歌", PersonSex.Male, "亚楠猎人", "亚楠猎人专线", "猎人的铃铛"));
		}

		/// <summary>
		/// 增加用户数据
		/// </summary>
		public void AddUserItem(UserVO user){

			//不为空
			if (user != null) {
				Users.Add(user);
			}
		}

		/// <summary>
		/// 更新数据
		/// </summary>
		/// <param name="user"></param>
		public void UpdateUserItems(UserVO user){
			for (int i = 0; i < Users.Count; i++) {
				if (Users[i].Equals(user)) {
					//更新
					Users[i] = user;
					break;
				}
			}
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="user"></param>
		public void DeleteUserItems(UserVO user){
			for (int i = 0; i < Users.Count; i++) {
				if (Users[i].Equals(user)) {
					Users.RemoveAt(i);
					break;
				}
			}
		}


	}
}