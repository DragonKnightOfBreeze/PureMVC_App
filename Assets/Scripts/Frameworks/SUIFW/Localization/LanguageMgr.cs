/***
 * 标题：
 * 语言国际化
 * 
 * 功能：
 * 使得我们发布的游戏，可以根据不同的国家，显示不同的语言信息。
 * 
 * 思路：
 * 
 * 
 * 改进：
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Kernel;
//using Global;

namespace SUIFW {
	/// <summary>
	/// 类：语言管理器
	/// </summary>
	public class LanguageMgr {
		//本类实例
		private static LanguageMgr _Instance;
		//语言翻译的缓存集合
		private Dictionary<string, string> _DicLanguageCache;
		//当前选择的配置文件
		private string _CurLanguageConfig = SysDefine.PATH_LanguageConfig_CN;
		
		public string CurLanguageConfig {
			get { return _CurLanguageConfig; }
			set { _CurLanguageConfig = value; }
		}



		private LanguageMgr(){
			_DicLanguageCache = new Dictionary<string, string>();
			//初始化语言缓存集合
			InitLanguageCache();
		}

		/// <summary>
		/// 公共方法：得到本类实例
		/// </summary>
		/// <returns></returns>
		public static LanguageMgr GetInstance(){
			if (_Instance == null) {
				_Instance = new LanguageMgr();
			}
			return _Instance;
		}



		#region 【私有方法】

		/// <summary>
		/// 初始化语言缓存集合
		/// </summary>
		private void InitLanguageCache(){
			IconfigMgr configMgr = new ConfigMgr(_CurLanguageConfig);
			_DicLanguageCache = configMgr.AppSetting;
		}

		/// <summary>
		/// 得到指定ID的文本
		/// </summary>
		/// <param name="id">文本的ID（Key）</param>
		/// <returns></returns>
		public string GetText(string id){
			//参数检查
			if (string.IsNullOrEmpty(id))
				return null;

			string result = string.Empty;	//查询结果
			//查询处理
			if (_DicLanguageCache != null && _DicLanguageCache.Count > 0) {
				_DicLanguageCache.TryGetValue(id, out result);
				if (!string.IsNullOrEmpty(result)) {
					return result;
				}
			}
			Debug.LogWarning(GetType() + "/ShowText()"+"\t查询失败！");
			return null;
		}

		#endregion


	}
}