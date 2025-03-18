using RenkeiDB.Data;
using RenkeiDB.Dto.ValidateRules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// ログインユーザー情報を表すDTO
    /// </summary>
    public class LoginUserDto
    {
        public LoginUserDto()
        {
        }
        public LoginUserDto(M_LoginUser _loginUser, M_CompanyUser _companyUser, M_Company _company)
        {
            this.LoginUser = _loginUser;
            this.CompanyUser = _companyUser;
            this.Company = _company;
        }

        public M_LoginUser LoginUser { get; set; }
        public M_CompanyUser CompanyUser { get; set; }
        public M_Company Company { get; set; }
    }

    /// <summary>
    /// ログインリクエスト情報を表すDTO
    /// </summary>
    public class LoginBodyDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [NotEmpty]
        [Display(Name = "ログインId")]
        public string loginId { get; set; }
        [NotEmpty]
        [Display(Name = "パスワード")]
        public string password { get; set; }
        [NotEmpty]
        [AllowedValues(DefaultValue.GuardCompany, DefaultValue.GuardCustomer)]
        [Display(Name = "guard")]
        public string guard { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }

    /// <summary>
    /// グループ情報を表すDTO
    /// </summary>
    public class GroupDto
    {
        public int Id { get; set; }
        public int GroupKubun { get; set; }
        public string GroupName { get; set; }
        public string DisplayName { get; set; }
        [JsonIgnore]
        public int SortOrder { get; set; }

    }

    /// <summary>
    /// 支店情報を表すDTO
    /// </summary>
    public class BranchDto
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchNameAbbr { get; set; }
    }

    /// <summary>
    /// 会社情報を表すDTO
    /// </summary>
    public class CompanyDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNameDisplay { get; set; }
        public bool OwnerFlg { get; set; }
    }

    /// <summary>
    /// 会社ユーザー情報を表すDTO
    /// </summary>
    public class CompanyUserDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DisplayName { get; set; }
        public CompanyDto Company { get; set; }
        public BranchDto Branch { get; set; }
        public List<GroupDto> Groups { get; set; }
    }

    /// <summary>
    /// ユーザー情報を表すDTO
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public CompanyUserDto CompanyUser { get; set; }
    }

    /// <summary>
    /// ログインユーザー情報を表すDTO
    /// </summary>
    public class LoginUserInfoDto
    {
        public LoginUserInfoDto()
        {
        }
        public LoginUserInfoDto(
            M_LoginUser _loginUser,
            M_CompanyUser _companyUser,
            M_Company _company,
            M_CompanyBranch _branch,
            M_CompanyUser_Group _companyUserGroup
        )
        {
            this.LoginUser = _loginUser;
            this.CompanyUser = _companyUser;
            this.Company = _company;
            this.BranchCompany = _branch;
            this.CompanyUserGroup = _companyUserGroup;
        }

        public M_LoginUser LoginUser { get; set; }
        public M_CompanyUser CompanyUser { get; set; }
        public M_Company Company { get; set; }
        public M_CompanyBranch BranchCompany { get; set; }
        public M_CompanyUser_Group CompanyUserGroup { get; set; }
    }

    public class GroupsLoginUserInfoDto
    {
        public M_LoginUser LoginUser { get; set; }
        public M_CompanyUser CompanyUser { get; set; }
        public M_Company Company { get; set; }
        public M_CompanyBranch BranchCompany { get; set; }
        public List<M_CompanyUser_Group> CompanyUserGroups { get; set; }
    }
}
