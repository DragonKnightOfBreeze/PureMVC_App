/***
 * 标题：
 * 模型层，数据实体类
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

//using Kernel;
//using Global;

namespace PureMVCApp {

	/// <summary>
	/// 枚举：性别
	/// </summary>
	public enum PersonSex {
		Male,
		Female
	}

	/// <summary>
	/// 模型层，数据实体类
	/// </summary>
	public class UserVO {

		private string _FirstName;
		private string _LastName;

		//属性
		public string UserName {
			get { return _FirstName + _LastName; }
		}
		public PersonSex Sex { get; set; }
		public string Department { get;  }
		public string PhoneNum { get;  }
		public string Email { get;  }

		//只读属性：有效性
		public bool IsValid {
			get { return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Department); }
		}



		public UserVO(){}

		public UserVO(string fname, string lname, PersonSex sex, string department, string phoneNum, string email){
			if (!string.IsNullOrEmpty(fname))
				_FirstName = fname;
			if (!string.IsNullOrEmpty(lname))
				_LastName = lname;
			Sex = sex;
			if (!string.IsNullOrEmpty(department))
				Department = department;
			if (!string.IsNullOrEmpty(phoneNum))
				PhoneNum = phoneNum;
			if (!string.IsNullOrEmpty(email))
				Email = email;
		}



		/// <summary>
		/// 对象比较方法
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other){
			
			UserVO otherVO = other as UserVO;
			if (otherVO != null) {
				if (this.UserName == otherVO.UserName) {
					return true;
				}
			}
			return false;
		}
		





	}
}