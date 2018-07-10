/***
 * 标题：
 * 通用配置管理器接口
 * 
 * 功能：
 * 基于“键值对”配置文件的通用解析
 * 
 * 
 * 思路：
 * 
 * 
 * 改进：
 * 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

//using Kernel;
//using Global;

namespace SUIFW {

	///<summary>
	/// 接口：通用配置管理器接口
	///</summary>
	public interface IconfigMgr{
		/// <summary>
		/// 只读属性：应用设置
		/// 功能：得到键值对集合数据
		/// </summary>
		Dictionary<string,string> AppSetting { get; }

		/// <summary>
		/// 得到配置文件（AppSetting）最大的数量
		/// </summary>
		/// <returns></returns>
		int GetAppSettingMaxNumber();
	}

	/// <summary>
	/// 键值对信息类
	/// </summary>
	[Serializable]
	internal class KeyValueInfo {
		/// <summary>
		/// 配置信息
		/// </summary>
		public List<KeyValueNode> ConfigInfo = null;
	}

	/// <summary>
	/// 键值对结点类
	/// </summary>
	[Serializable]
	internal class KeyValueNode {
		public string Key = null;
		public string Value = null;
	}
}