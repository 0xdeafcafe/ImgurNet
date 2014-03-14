using System;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using ImgurNet.Models;

namespace ImgurNet
{
	public class Imgur
	{
		public IAuthentication Authentication { get; private set; }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="authentication"></param>
		public Imgur(IAuthentication authentication)
		{
			Authentication = authentication;
		}
	}
}
