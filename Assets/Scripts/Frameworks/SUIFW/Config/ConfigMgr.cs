/***
 * 标题：
 * 基于Json配置文件的通用配置管理器
 * 
 * 功能：
 * 通过面向接口思想，根据Json文件路径，实例化此类
 * 然后就可以根据AppSetting字典，找到Json文件中的所有元素（Key和Value）
 * keyValueInfoObj.ConfigInfo：Json文件中的ConfigInfo列表，列表里面是KeyValue键值对元素
 *
 * 用法：
 * IconfigMgr configMgr = new ConfigMgr(jsonPath);		//初始化工作
 * string keyname1 = configMgr.AppSetting["keyname1"];	//得到指定Key的Value
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
using UnityEngine;

//using Kernel;
//using Global;

namespace SUIFW {
	///<summary>
	///
	///</summary>
	public class ConfigMgr : IconfigMgr{

		private static Dictionary<string, string> _AppSetting;

		/// <summary>
		/// 只读属性：应用设置
		/// 功能：得到键值对集合数据
		/// </summary>
		public Dictionary<string, string> AppSetting {
			get { return _AppSetting; }
		}


		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="jsonPath">Json文件路径</param>
		public ConfigMgr(string jsonPath){
			_AppSetting = new Dictionary<string, string>();
			//初始化解析Json数据，加载到集合中
			InitAndAnalysisJson(jsonPath);
		}


		/// <summary>
		/// 得到配置文件（AppSetting）最大的数量
		/// </summary>
		/// <returns></returns>
		public int GetAppSettingMaxNumber(){
			if (_AppSetting != null && _AppSetting.Count > 0) {
				return _AppSetting.Count;
			}
			return 0;
		}


		#region 【私有方法】

		/// <summary>
		/// 初始化解析Json数据，加载到集合中
		/// </summary>
		/// <param name="jsonPath"></param>
		private void InitAndAnalysisJson(string jsonPath){
			TextAsset jsonFile = null;
			KeyValueInfo keyValueInfoObj = null;

			//参数检查
			if (string.IsNullOrEmpty(jsonPath))
				return;
			//解析Json配置文件
			try {
				//加载配置文件
				jsonFile = Resources.Load<TextAsset>(jsonPath);
				keyValueInfoObj =  JsonUtility.FromJson<KeyValueInfo>(jsonFile.text);
			}
			catch {
				throw new JsonAnalysisException(GetType() + "/InitAndAnalysisJson()/JsonAnalysisException()!"+"\tjsonPath = "+jsonPath);
				//抛出自定义异常
			}
			//把这些数据加载到AppSetting字典中
			foreach (var nodeInfo in keyValueInfoObj.ConfigInfo) {
					_AppSetting.Add(nodeInfo.Key,nodeInfo.Value);
			}

		}

		#endregion

	}
}