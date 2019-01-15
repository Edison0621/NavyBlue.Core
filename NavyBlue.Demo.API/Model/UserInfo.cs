// *****************************************************************************************************************
// Project          : NavyBlue
// File             : UserInfo.cs
// Created          : 2019-01-10  19:09
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:55
// *****************************************************************************************************************
// <copyright file="UserInfo.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System.ComponentModel.DataAnnotations;

namespace NavyBlue.Demo.API.Model
{
    public class UserInfo
    {
        [Required] public string UserName { get; set; }
    }
}