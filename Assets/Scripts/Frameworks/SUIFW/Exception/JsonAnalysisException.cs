/***
 * 标题：
 * Json解析异常
 * 
 * 功能：
 * 专门负责对于Json由于路径错误，或者文件格式错误造成的异常，进行捕获。
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
	///异常：Json解析异常
	///</summary>
	public class JsonAnalysisException : Exception {
		public JsonAnalysisException() : base() { }

		public JsonAnalysisException(string message) : base(message){ }


	}
}