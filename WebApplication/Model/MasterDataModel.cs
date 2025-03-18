using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Dto;

namespace WebApplication.Model
{
    /// <summary>
    /// マスタデータモデル
    /// </summary>
    public class MasterDataModel : BaseModel
    {
        public MasterDataModel(ApplicationDbContext context)
        {
            _context = context;
        }

        #region M_Company
        /// <summary>
        /// M_Companyマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="m_Company">会社データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCompanyData(Data.M_Company m_Company)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_Company.Company_ID == 0)
                    {
                        //m_Company.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Companies.Add(m_Company);
                    }
                    else
                    {
                        var data = await _context.M_Companies.FirstOrDefaultAsync(m => m.Company_ID == m_Company.Company_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, m_Company);
                        //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Companies.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Company

        #region M_CompanyUser
        /// <summary>
        /// M_CompanyUserマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_CompanyUser">会社ユーザーデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCompanyUserMasterData(Data.M_CompanyUser M_CompanyUser)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_CompanyUser.User_ID == 0)
                    {
                        M_CompanyUser.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_CompanyUsers.Add(M_CompanyUser);
                    }
                    else
                    {
                        Data.M_CompanyUser data = await _context.M_CompanyUsers.FirstOrDefaultAsync(m => m.Company_ID == M_CompanyUser.Company_ID &&
                                                                            m.User_ID == M_CompanyUser.User_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, M_CompanyUser);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_CompanyUsers.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_CompanyUser

        #region M_CompanyUser_Group
        /// <summary>
        /// M_CompanyUser_Groupの並び順変更処理
        /// </summary>
        /// <param name="dto">会社ユーザーグループデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateCompanyUserGroupListSortOrder(Dto.CompanyUserGroupDto dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_CompanyUser_Group> M_CompanyUser_Groups = await _context.M_CompanyUser_Groups.Where(m => m.Company_ID == dto.CompanyID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_CompanyUser_Group> M_CompanyUser_GroupsUpdate = dto.CompanyUserGroupList.OrderBy(m => m.SortOrder).ToList();


                using var tran = _context.Database.BeginTransaction();
                try
                {

                    foreach (Data.M_CompanyUser_Group target in M_CompanyUser_GroupsUpdate)
                    {
                        Data.M_CompanyUser_Group M_CompanyUser_Group = M_CompanyUser_Groups.FirstOrDefault(m => m.Group_ID == target.Group_ID);
                        if (M_CompanyUser_Group != null && M_CompanyUser_Group.SortOrder != target.SortOrder)
                        {
                            M_CompanyUser_Group.SortOrder = target.SortOrder;

                            _context.M_CompanyUser_Groups.Update(M_CompanyUser_Group);
                        }

                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="dto">会社ユーザーグループデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCompanyUserGroupMasterData(Dto.CompanyUserGroupDto dto)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    Data.M_CompanyUser_Group data = new();

                    if (dto.CompanyUserGroupData.Group_ID == 0)
                    {
                        IEnumerable<Data.M_CompanyUser_Group> list = await _context.M_CompanyUser_Groups.Where(m => m.Company_ID == dto.CompanyID).ToListAsync();
                        int iMax = 0;
                        if (list.Count() > 0) { iMax = list.Max(m => m.SortOrder); }
                        dto.CompanyUserGroupData.SortOrder = iMax + 1;
                        dto.CompanyUserGroupData.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_CompanyUser_Groups.Add(dto.CompanyUserGroupData);
                        _context.SaveChanges();

                        foreach(var item in dto.CompanyUserGroupUserList)
                        {
                            item.Group_ID = dto.CompanyUserGroupData.Group_ID;
                        }

                    }
                    else
                    {
                        data = await _context.M_CompanyUser_Groups.FirstOrDefaultAsync(m => m.Group_ID == dto.CompanyUserGroupData.Group_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, dto.CompanyUserGroupData);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_CompanyUser_Groups.Update(data);
                        _context.SaveChanges();
                    }



                    List<Data.M_CompanyUser_GroupUser> users = await _context.M_CompanyUser_GroupUsers.Where(m => m.Group_ID == dto.CompanyUserGroupData.Group_ID).ToListAsync();
                    if (users != null)
                    {
                        _context.M_CompanyUser_GroupUsers.RemoveRange(users);
                    }

                    await _context.M_CompanyUser_GroupUsers.AddRangeAsync(dto.CompanyUserGroupUserList);


                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }


        /// <summary>
        /// M_CompanyUser_Groupマスタの削除処理
        /// </summary>
        /// <param name="CompanyUserGroup">会社ユーザーグループデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> DeleteCompanyUserGroupMasterData(Data.M_CompanyUser_Group CompanyUserGroup)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    Data.M_CompanyUser_Group data = await _context.M_CompanyUser_Groups.FirstOrDefaultAsync(m => m.Group_ID == CompanyUserGroup.Group_ID);
                    if (data == null)
                    {
                        throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                    }


                    /*
                     * データの整合性チェック
                     */

                    //論理削除処理
                    data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    data.Del_Flg = true;

                    _context.M_CompanyUser_Groups.Update(data);
                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_CompanyUser_Group

        #region M_CompanyDriver
        /// <summary>
        /// M_CompanyDriverマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_CompanyDriver">会社ドライバーデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCompanyDriverMasterData(Data.M_CompanyDriver M_CompanyDriver)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_CompanyDriver.Driver_ID == 0)
                    {
                        M_CompanyDriver.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_CompanyDrivers.Add(M_CompanyDriver);
                    }
                    else
                    {
                        Data.M_CompanyDriver data = await _context.M_CompanyDrivers.FirstOrDefaultAsync(m => m.Company_ID == M_CompanyDriver.Company_ID &&
                                                                            m.Driver_ID == M_CompanyDriver.Driver_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, M_CompanyDriver);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_CompanyDrivers.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_CompanyDriver

        #region M_CompanyBranch
        /// <summary>
        /// M_CompanyBranchのSortOrderの変更処理
        /// </summary>
        /// <param name="param">会社支店データ</param>
        /// <returns>結果</returns>
        internal async Task<MsterDataCommonResultValDto> UpdateSortOrderForCompanyBranch(List<M_CompanyBranch> param)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_CompanyBranch> masterData = await _context.M_CompanyBranches.Where(m => m.Company_ID == param.First().Company_ID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_CompanyBranch> targetData = param.OrderBy(m => m.SortOrder).ToList();


                using var tran = _context.Database.BeginTransaction();
                try
                {

                    foreach (Data.M_CompanyBranch target in targetData)
                    {

                        Data.M_CompanyBranch m_CompanyBranch = masterData.FirstOrDefault(m => m.Branch_ID == target.Branch_ID);
                        if (m_CompanyBranch != null && m_CompanyBranch.SortOrder != target.SortOrder)
                        {
                            m_CompanyBranch.SortOrder = target.SortOrder;

                            _context.M_CompanyBranches.Update(m_CompanyBranch);
                        }

                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        

        /// <summary>
        /// M_CompanyBranchの追加・更新処理
        /// SortOrder == 0は新規追加、0以外は更新
        /// </summary>
        /// <param name="m_CompanyBranch">会社支店データ</param>
        /// <returns>結果</returns>
        internal async Task<MsterDataCommonResultValDto> InsertUpdateDataForCompanyBranch(M_CompanyBranch param)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (param.SortOrder == 0)
                    {
                        IEnumerable<Data.M_CompanyBranch> list = await _context.M_CompanyBranches.Where(m => m.Company_ID == param.Company_ID).ToListAsync();

                        int? iMax = 0;
                        if (list.Count() > 0) { iMax = list.Max(m => m.SortOrder); }
                        param.SortOrder = (int)iMax + 1;
                        param.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                        Data.M_CompanyBranch data = new();
                        CopyProperty(data, param);
                        _context.M_CompanyBranches.Add(data);
                    }
                    else
                    {
                        Data.M_CompanyBranch data = await _context.M_CompanyBranches.FirstOrDefaultAsync(m => m.Branch_ID == param.Branch_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, param);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_CompanyBranches.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_CompanyBranch

        #region M_Kata
        /// <summary>
        /// M_Kataマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="m_Kata">型データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateKataMasterData(Data.M_Katum m_Kata)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_Kata.SortOrder == 0)
                    {
                        IEnumerable<Data.M_Katum> list = await _context.M_Kata.Where(m => m.Company_ID == m_Kata.Company_ID).ToListAsync();
                        int? iMax = 0;
                        if (list.Count() > 0) { iMax = list.Max(m => m.SortOrder); }
                        m_Kata.SortOrder = (int)iMax + 1;
                        //m_Kata.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Kata.Add(m_Kata);
                    }
                    else
                    {
                        var data = await _context.M_Kata.FirstOrDefaultAsync(m => m.Company_ID == m_Kata.Company_ID && m.Kata_ID == m_Kata.Kata_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, m_Kata);
                        //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Kata.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

        /// <summary>
        /// M_Kataマスタの並び順変更処理
        /// </summary>
        /// <param name="dto">型データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateKataListSortOrder(List<Data.M_Katum> dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_Katum> m_Katas = await _context.M_Kata.Where(m => m.Company_ID == dto[0].Company_ID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_Katum> m_KatasUpdate = dto.OrderBy(m => m.SortOrder).ToList();

                using var tran = _context.Database.BeginTransaction();
                try
                {
                    foreach (Data.M_Katum target in m_KatasUpdate)
                    {
                        Data.M_Katum m_Kata = m_Katas.FirstOrDefault(m => m.Kata_ID == target.Kata_ID);
                        if (m_Kata != null && m_Kata.SortOrder != target.SortOrder)
                        {
                            m_Kata.SortOrder = target.SortOrder;
                            _context.M_Kata.Update(m_Kata);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Kata

        #region M_Syaryo
        /// <summary>
        /// M_Syaryoの並び順変更処理
        /// </summary>
        /// <param name="dto">車両データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateSyaryoListSortOrder(Dto.SaryoListDto dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_Syaryo> m_Syaryos = await _context.M_Syaryos.Where(m => m.Company_ID == dto.CompanyID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_Syaryo> m_SyaryosUpdate = dto.M_SyaryoList.OrderBy(m => m.SortOrder).ToList();

                using var tran = _context.Database.BeginTransaction();
                try
                {
                    foreach (Data.M_Syaryo target in m_SyaryosUpdate)
                    {

                        //Data.M_Syaryo m_Syaryo = m_Syaryos.FirstOrDefault(m => m.SYASYU == target.SYASYU && m.KATA == target.KATA);
                        Data.M_Syaryo m_Syaryo = m_Syaryos.FirstOrDefault(m => m.Syaryo_ID == target.Syaryo_ID);
                        if (m_Syaryo != null && m_Syaryo.SortOrder != target.SortOrder)
                        {
                            m_Syaryo.SortOrder = target.SortOrder;

                            _context.M_Syaryos.Update(m_Syaryo);
                        }

                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

        /// <summary>
        /// M_Syaryoマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="m_Syaryo">車両データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateSyaryoMasterData(Data.M_Syaryo m_Syaryo)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_Syaryo.Syaryo_ID == 0)
                    {
                        IEnumerable<Data.M_Syaryo> list = await _context.M_Syaryos.Where(m => m.Company_ID == m_Syaryo.Company_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Any()) { iMax = list.Max(m => m.SortOrder); }
                        m_Syaryo.SortOrder = iMax + 1;
                        m_Syaryo.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Syaryos.Add(m_Syaryo);
                    }
                    else
                    {
                        var data = await _context.M_Syaryos.FirstOrDefaultAsync(m => m.Syaryo_ID == m_Syaryo.Syaryo_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, m_Syaryo);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Syaryos.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }


        /// <summary>
        /// M_Syaryoマスタの削除処理
        /// </summary>
        /// <param name="m_Syaryo">車両データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> DeleteSyaryoMasterData(Data.M_Syaryo m_Syaryo)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var data = await _context.M_Syaryos.FirstOrDefaultAsync(
                        m => m.Company_ID == m_Syaryo.Company_ID &&
                        m.SYASYU == m_Syaryo.SYASYU && m.KATA == m_Syaryo.KATA)
                        ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");

                    /*
                     * データの整合性チェック
                     */

                    //論理削除処理
                    data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    data.DEL_FLG = true;

                    _context.M_Syaryos.Update(data);
                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Syaryo

        #region M_SyaryoSize
        /// <summary>
        /// M_SyaryoSizeマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="m_SyaryoSize">車両サイズデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateSyaryoSizeMasterData(Data.M_SyaryoSize m_SyaryoSize)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_SyaryoSize.SortOrder == 0)
                    {
                        IEnumerable<Data.M_SyaryoSize> list = await _context.M_SyaryoSizes.Where(m => m.Company_ID == m_SyaryoSize.Company_ID).ToListAsync();
                        
                        int? iMax = 0;
                        if (list.Any()) { iMax = list.Max(m => m.SortOrder); }
                        m_SyaryoSize.SortOrder = (int)iMax + 1;
                        m_SyaryoSize.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_SyaryoSizes.Add(m_SyaryoSize);
                    }
                    else
                    {
                        var data = await _context.M_SyaryoSizes.FirstOrDefaultAsync(
                            m => m.Company_ID == m_SyaryoSize.Company_ID &&
                            m.SIZE == m_SyaryoSize.SIZE)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, m_SyaryoSize);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_SyaryoSizes.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

        /// <summary>
        /// M_SyaryoSizeマスタの並び順変更処理
        /// </summary>
        /// <param name="dto">車両サイズデータリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateSyaryoSizeListSortOrder(Dto.SaryoSizeListDto dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_SyaryoSize> m_SyaryoSizes = await _context.M_SyaryoSizes.Where(m => m.Company_ID == dto.CompanyID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_SyaryoSize> m_SyaryoSizesUpdate = dto.M_SyaryoSizeList.OrderBy(m => m.SortOrder).ToList();

                using var tran = _context.Database.BeginTransaction();
                try
                {
                    foreach (Data.M_SyaryoSize target in m_SyaryoSizesUpdate)
                    {
                        var m_SyaryoSize = m_SyaryoSizes.FirstOrDefault(m => m.SIZE == target.SIZE);
                        if (m_SyaryoSize != null && m_SyaryoSize.SortOrder != target.SortOrder)
                        {
                            m_SyaryoSize.SortOrder = target.SortOrder;

                            _context.M_SyaryoSizes.Update(m_SyaryoSize);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_SyaryoSize

        #region M_SyaryoCost
        /// <summary>
        /// M_SyaryoCostのデータ更新
        /// 車種・型キーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_SyaryoCost">車両コストデータリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateSyaryoCostData(List<Data.M_SyaryoCost> m_SyaryoCost)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_SyaryoCost == null || m_SyaryoCost.Count == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_SyaryoCost> data = await _context.M_SyaryoCosts.Where(m => m.Company_ID == m_SyaryoCost[0].Company_ID &&
                                                                                        m.Syaryo_ID == m_SyaryoCost[0].Syaryo_ID).ToListAsync();
                    if (data != null)
                    {
                        _context.M_SyaryoCosts.RemoveRange(data);
                    }
                    
                    await _context.M_SyaryoCosts.AddRangeAsync(m_SyaryoCost);

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_SyaryoCost

        #region M_SyaryoManagement
        /// <summary>
        /// M_SyaryoManagementマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_SyaryoManagement">車両管理データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateSyaryoManagementMasterData(Data.M_SyaryoManagement M_SyaryoManagement)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    M_Syaryo syaryoData = await _context.M_Syaryos.FirstOrDefaultAsync(m => m.Syaryo_ID == M_SyaryoManagement.Syaryo_ID);

                    if (syaryoData != null)
                    {
                        M_SyaryoManagement.Syasyu = syaryoData.SYASYU;
                        M_SyaryoManagement.Kata = syaryoData.KATA;
                    }

                    if (M_SyaryoManagement.SyaryoManagement_ID == 0)
                    {
                        //M_SyaryoManagement.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_SyaryoManagements.Add(M_SyaryoManagement);
                    }
                    else
                    {
                        var data = await _context.M_SyaryoManagements.FirstOrDefaultAsync(m => m.SyaryoManagement_ID == M_SyaryoManagement.SyaryoManagement_ID);

                        CopyProperty(data, M_SyaryoManagement);
                        //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_SyaryoManagements.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_SyaryoManagement

        #region M_DefaultMoney
        /// <summary>
        /// M_DefaultMoneyのデータ更新
        /// エリア・車種サイズキーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_DefaultMoney">デフォルト金額データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateDefaultMoneyData(List<Data.M_DefaultMoney> m_DefaultMoney)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_DefaultMoney == null || m_DefaultMoney.Count == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_DefaultMoney> data = await _context.M_DefaultMoneys.Where(m => m.Area == m_DefaultMoney[0].Area &&
                                                                            m.SyasyuSize == m_DefaultMoney[0].SyasyuSize).ToListAsync();
                    if (data != null)
                    {
                        _context.M_DefaultMoneys.RemoveRange(data);
                    }

                    await _context.M_DefaultMoneys.AddRangeAsync(m_DefaultMoney);

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_DefaultMoney

        #region M_DefaultMoney_WaitTimeForEria
        /// <summary>
        /// M_DefaultMoney_WaitTimeForEriaのデータ更新
        /// エリア・車種サイズキーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_DefaultMoney_WaitTimeForEria">デフォルト金額待機時間データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateDefaultMoneyWaitTimeForEriaData(List<Data.M_DefaultMoney_WaitTimeForArea> m_DefaultMoney_WaitTimeForEria)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_DefaultMoney_WaitTimeForEria == null || m_DefaultMoney_WaitTimeForEria.Count == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {

                    foreach(var target in m_DefaultMoney_WaitTimeForEria)
                    {
                        var data = await _context.M_DefaultMoney_WaitTimeForAreas.FirstOrDefaultAsync(m => m.Area == target.Area && m.SyasyuSize == target.SyasyuSize);
                        if (data != null)
                        {
                            data.Amount = target.Amount;
                            _context.M_DefaultMoney_WaitTimeForAreas.Update(data);
                        } else
                        {
                            await _context.M_DefaultMoney_WaitTimeForAreas.AddRangeAsync(data);
                        }
                    }

                    //List<Data.M_DefaultMoney_WaitTimeForErium> data = await _context.M_DefaultMoney_WaitTimeForEria.Where(m => m.Eria == m_DefaultMoney_WaitTimeForEria[0].Eria &&
                    //                                                        m.SyasyuSize == m_DefaultMoney_WaitTimeForEria[0].SyasyuSize).ToListAsync();
                    //if (data != null)
                    //{
                    //    _context.M_DefaultMoney_WaitTimeForEria.RemoveRange(data);
                    //}

                    //await _context.M_DefaultMoney_WaitTimeForEria.AddRangeAsync(m_DefaultMoney_WaitTimeForEria);

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_DefaultMoney_WaitTimeForEria

        #region M_DefaultMoney_WaitTimeForCompany
        /// <summary>
        /// M_DefaultMoney_WaitTimeForCompanyのデータ更新
        /// CompanyIDキーでのDelete⇒Inset
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="m_DefaultMoney_WaitTimeForCompanies">デフォルト金額待機時間データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateDefaultMoneyWaitTimeForCompanyData(int iCompanyID, List<Data.M_DefaultMoney_WaitTimeForCompany> m_DefaultMoney_WaitTimeForCompanies)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (iCompanyID == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_DefaultMoney_WaitTimeForCompany> data = await _context.M_DefaultMoney_WaitTimeForCompanies.Where(m => m.Company_ID == iCompanyID).ToListAsync();
                    if (data != null)
                    {
                        _context.M_DefaultMoney_WaitTimeForCompanies.RemoveRange(data);
                    }

                    if (m_DefaultMoney_WaitTimeForCompanies != null && m_DefaultMoney_WaitTimeForCompanies.Count > 0)
                    {
                        await _context.M_DefaultMoney_WaitTimeForCompanies.AddRangeAsync(m_DefaultMoney_WaitTimeForCompanies);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_DefaultMoney_WaitTimeForCompany

        #region M_PersonnelExpense
        /// <summary>
        /// M_PersonnelExpenseのデータ更新
        /// CompanyIDキーでのDelete⇒Inset
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="m_PersonnelExpenses">人件費データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdatePersonnelExpenseData(int iCompanyID, List<Data.M_PersonnelExpense> m_PersonnelExpenses)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (iCompanyID == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_PersonnelExpense> data = await _context.M_PersonnelExpenses.Where(m => m.Company_ID == iCompanyID).ToListAsync();
                    if (data != null)
                    {
                        _context.M_PersonnelExpenses.RemoveRange(data);
                    }

                    if (m_PersonnelExpenses != null && m_PersonnelExpenses.Count > 0)
                    {
                        await _context.M_PersonnelExpenses.AddRangeAsync(m_PersonnelExpenses);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_PersonnelExpense

        #region M_FuelCost
        /// <summary>
        /// M_FuelCostのデータ更新
        /// CompanyIDキーでのDelete⇒Inset
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="m_FuelCosts">燃料費データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateFuelCostData(int iCompanyID, List<Data.M_FuelCost> m_FuelCosts)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (iCompanyID == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_FuelCost> data = await _context.M_FuelCosts.Where(m => m.Company_ID == iCompanyID).ToListAsync();
                    if (data != null)
                    {
                        _context.M_FuelCosts.RemoveRange(data);
                    }

                    if (m_FuelCosts != null && m_FuelCosts.Count > 0)
                    {
                        await _context.M_FuelCosts.AddRangeAsync(m_FuelCosts);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_FuelCost

        #region M_Customer
        /// <summary>
        /// 顧客登録画面データの登録更処理
        /// Customer_IDが0が登録、0以外が更新
        /// </summary>
        /// <param name="dto">顧客データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerDto(Dto.CustomerModalDto dto)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (dto.CustomerData.Customer_ID == 0)
                    {
                        dto.CustomerData.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customers.Add(dto.CustomerData);
                        _context.SaveChanges();
                    }
                    else
                    {
                        Data.M_Customer data = await _context.M_Customers.FirstOrDefaultAsync(m => m.Customer_ID == dto.CustomerData.Customer_ID);
                        if (data == null) { throw new Exception("対象データが存在しません。データ不整合が発生しています。"); }
                        CopyProperty(data, dto.CustomerData);
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customers.Update(data);
                    }

                    foreach (Data.M_Customer_Branch target in dto.CustomerBranchList)
                    {
                        if (target.Customer_Branch_ID == 0)
                        {
                            target.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            target.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            target.Customer_ID = dto.CustomerData.Customer_ID;
                            _context.M_Customer_Branches.Add(target);
                        }
                        else
                        {
                            Data.M_Customer_Branch data = await _context.M_Customer_Branches.FirstOrDefaultAsync(m => m.Customer_Branch_ID == target.Customer_Branch_ID);
                            if (data == null)
                            {
                                throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                            }
                            data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            data.Customer_Branch_Code = target.Customer_Branch_Code;
                            data.Customer_Branch_Name = target.Customer_Branch_Name;
                            data.Customer_Branch_Name_Abbr = target.Customer_Branch_Name_Abbr;
                            _context.M_Customer_Branches.Update(data);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }

            return resultVal;
        }
        #endregion M_Customer

        #region M_Customer_Branch
        /// <summary>
        /// 顧客支店顧客登録画面データの登録更処理
        /// </summary>
        /// <param name="dto">顧客支店データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerBranchDto(Dto.CustomerBranchModalDto dto)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    DateTime dateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    if (dto.CustomerBranchData.Customer_Branch_ID == 0)
                    {
                        ///仕様上このコードは到達されないはず
                        dto.CustomerBranchData.Insert_Datetime = dateTime;
                        _context.M_Customer_Branches.Add(dto.CustomerBranchData);
                        _context.SaveChanges();

                    }
                    else
                    {
                        Data.M_Customer_Branch data = await _context.M_Customer_Branches.FirstOrDefaultAsync(m => m.Customer_Branch_ID == dto.CustomerBranchData.Customer_Branch_ID);
                        if (data == null) { throw new Exception("対象データが存在しません。データ不整合が発生しています。"); }
                        CopyProperty(data, dto.CustomerBranchData, "Insert_Datetime,Insert_User");
                        data.Update_Datetime = dateTime;
                        _context.M_Customer_Branches.Update(data);
                    }

                    //振られたCustomer_Branch_IDを各リストに設定
                    if (dto.CustomerUriageCalcData != null) dto.CustomerUriageCalcData.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID;
                    if (dto.CustomerShiharaiCalcData != null) dto.CustomerShiharaiCalcData.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID;
                    if (dto.CustomerTollSeikyuKubunList != null) dto.CustomerTollSeikyuKubunList.ForEach(m => m.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID);
                    //if (dto.CustomerICSeikyuKubunList != null) dto.CustomerICSeikyuKubunList.ForEach(m => m.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID);

                    foreach (Data.M_Customer_Tantou tantou in dto.CustomerTantouList)
                    {
                        if (tantou.Tantou_ID == 0)
                        {
                            tantou.Insert_Datetime = dateTime;
                            tantou.Update_Datetime = dateTime;
                            tantou.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID;
                            _context.M_Customer_Tantous.Add(tantou);
                        }
                        else
                        {
                            Data.M_Customer_Tantou data = await _context.M_Customer_Tantous.FirstOrDefaultAsync(m => m.Tantou_ID == tantou.Tantou_ID);
                            if (data == null)
                            {
                                throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                            }
                            CopyProperty(data, tantou, "Insert_Datetime,Insert_User");
                            data.Update_Datetime = dateTime;
                            _context.M_Customer_Tantous.Update(data);
                        }

                    }

                    if (dto.CustomerUriageCalcData != null && dto.CustomerUriageCalcData.Customer_Branch_ID > 0)
                    {
                        Data.M_Customer_Uriage_Calc data = await _context.M_Customer_Uriage_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == dto.CustomerUriageCalcData.Customer_Branch_ID);
                        if (data != null)
                        {
                            _context.M_Customer_Uriage_Calcs.Update(dto.CustomerUriageCalcData);
                        }
                        else
                        {
                            await _context.M_Customer_Uriage_Calcs.AddRangeAsync(dto.CustomerUriageCalcData);
                        }
                    }

                    if (dto.CustomerShiharaiCalcData != null && dto.CustomerShiharaiCalcData.Customer_Branch_ID > 0)
                    {
                        Data.M_Customer_Shiharai_Calc data = await _context.M_Customer_Shiharai_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == dto.CustomerShiharaiCalcData.Customer_Branch_ID);
                        if (data != null)
                        {
                            _context.M_Customer_Shiharai_Calcs.Update(dto.CustomerShiharaiCalcData);
                        }
                        else
                        {
                            await _context.M_Customer_Shiharai_Calcs.AddRangeAsync(dto.CustomerShiharaiCalcData);
                        }
                    }

                    //if (dto.CustomerICSeikyuKubunList != null && dto.CustomerICSeikyuKubunList[0].Customer_Branch_ID > 0)
                    //{
                    //    List<Data.M_Customer_ICSeikyuKubun> data = await _context.M_Customer_ICSeikyuKubuns.Where(m => m.Customer_Branch_ID == dto.CustomerICSeikyuKubunList[0].Customer_Branch_ID).ToListAsync();
                    //    if (data != null)
                    //    {
                    //        _context.M_Customer_ICSeikyuKubuns.RemoveRange(data);
                    //    }

                    //    await _context.M_Customer_ICSeikyuKubuns.AddRangeAsync(dto.CustomerICSeikyuKubunList);
                    //}

                    
                    dto.CustomerTollSeikyuKubunList.ForEach(m => { m.Update_Datetime = dateTime; m.Insert_Datetime = dateTime; });

                    List<Data.M_Customer_TollSeikyuKubun> dataTollSeikyuKubu = await _context.M_Customer_TollSeikyuKubuns.Where(m => m.Customer_Branch_ID == dto.CustomerBranchData.Customer_Branch_ID).ToListAsync();
                    if (dataTollSeikyuKubu != null)
                    {
                        _context.M_Customer_TollSeikyuKubuns.RemoveRange(dataTollSeikyuKubu);
                    }

                    if (dto.CustomerTollSeikyuKubunList != null && dto.CustomerTollSeikyuKubunList.Count > 0)
                    {
                        await _context.M_Customer_TollSeikyuKubuns.AddRangeAsync(dto.CustomerTollSeikyuKubunList);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_Branch

        #region M_Customer_Tantou
        /// <summary>
        /// M_Customer_Tantouマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_Customer_Tantou">顧客担当データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerTantouData(Data.M_Customer_Tantou M_Customer_Tantou)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_Customer_Tantou.Tantou_ID == 0)
                    {
                        _context.M_Customer_Tantous.Add(M_Customer_Tantou);
                    }
                    else
                    {
                        var data = await _context.M_Customer_Tantous.FirstOrDefaultAsync(m => m.Tantou_ID == M_Customer_Tantou.Tantou_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, M_Customer_Tantou);
                        //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Tantous.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_Tantou

        #region M_Customer_TantouHaisyaGroup
        /// <summary>
        /// M_Customer_TantouHaisyaGroupのデータ更新
        /// エリア・車種サイズキーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_CustomerTantouHaisyaGroup">顧客担当配車グループデータリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerTantouHaisyaGroupData(List<Data.M_Customer_TantouHaisyaGroup> m_CustomerTantouHaisyaGroup)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_CustomerTantouHaisyaGroup == null || m_CustomerTantouHaisyaGroup.Count == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_Customer_TantouHaisyaGroup> data = await _context.M_Customer_TantouHaisyaGroups.Where(m => m.Customer_ID == m_CustomerTantouHaisyaGroup[0].Customer_ID).ToListAsync();
                    if (data != null)
                    {
                        _context.M_Customer_TantouHaisyaGroups.RemoveRange(data);
                    }

                    await _context.M_Customer_TantouHaisyaGroups.AddRangeAsync(m_CustomerTantouHaisyaGroup);

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_TantouHaisyaGroup

        #region M_Customer_Driver
        /// <summary>
        /// M_Customer_Driverマスタの登録更処理
        /// Customer_Driver_IDが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_Customer_Driver">顧客ドライバーデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerDriverData(Data.M_Customer_Driver M_Customer_Driver)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_Customer_Driver.Customer_Driver_ID == 0)
                    {
                        _context.M_Customer_Drivers.Add(M_Customer_Driver);
                    }
                    else
                    {
                        var data = await _context.M_Customer_Drivers.FirstOrDefaultAsync(m => m.Customer_Driver_ID == M_Customer_Driver.Customer_Driver_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, M_Customer_Driver);
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Drivers.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_Driver

        #region M_Customer_Driver_Syaryo
        /// <summary>
        /// M_Customer_Driver_Syaryoマスタの登録更処理
        /// Customer_DriverSyaryo_IDが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_Customer_Driver_Syaryo">顧客ドライバー車両データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerDriverSyaryoData(Data.M_Customer_Driver_Syaryo M_Customer_Driver_Syaryo)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_Customer_Driver_Syaryo.Customer_DriverSyaryo_ID == 0)
                    {
                        _context.M_Customer_Driver_Syaryos.Add(M_Customer_Driver_Syaryo);
                    }
                    else
                    {
                        var data = await _context.M_Customer_Driver_Syaryos.FirstOrDefaultAsync(m => m.Customer_DriverSyaryo_ID == M_Customer_Driver_Syaryo.Customer_DriverSyaryo_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, M_Customer_Driver_Syaryo);
                        //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Driver_Syaryos.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_Driver_Syaryo

        #region M_Customer_TollSeikyuKubun
        /// <summary>
        /// M_Customer_TollSeikyuKubunのデータ更新
        /// エリア・車種サイズキーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_CustomerTollSeikyuKubun">顧客通行料請求区分データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerTollSeikyuKubunData(List<Data.M_Customer_TollSeikyuKubun> m_CustomerTollSeikyuKubun)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_CustomerTollSeikyuKubun == null || m_CustomerTollSeikyuKubun.Count == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_Customer_TollSeikyuKubun> data = await _context.M_Customer_TollSeikyuKubuns.Where(m => m.Customer_Branch_ID == m_CustomerTollSeikyuKubun[0].Customer_Branch_ID).ToListAsync();
                    if (data != null)
                    {
                        _context.M_Customer_TollSeikyuKubuns.RemoveRange(data);
                    }

                    await _context.M_Customer_TollSeikyuKubuns.AddRangeAsync(m_CustomerTollSeikyuKubun);

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_TollSeikyuKubun

        #region M_Customer_ICSeikyuKubun
        /// <summary>
        /// M_Customer_ICSeikyuKubunのデータ更新
        /// エリア・車種サイズキーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_CustomerICSeikyuKubun">顧客IC請求区分データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerICSeikyuKubunData(List<Data.M_Customer_ICSeikyuKubun> m_CustomerICSeikyuKubun)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_CustomerICSeikyuKubun == null || m_CustomerICSeikyuKubun.Count == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_Customer_ICSeikyuKubun> data = await _context.M_Customer_ICSeikyuKubuns.Where(m => m.Customer_Branch_ID == m_CustomerICSeikyuKubun[0].Customer_Branch_ID).ToListAsync();
                    if (data != null)
                    {
                        _context.M_Customer_ICSeikyuKubuns.RemoveRange(data);
                    }

                    await _context.M_Customer_ICSeikyuKubuns.AddRangeAsync(m_CustomerICSeikyuKubun);

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_ICSeikyuKubun

        #region M_Customer_Uriage_Calc
        /// <summary>
        /// M_Customer_Uriage_Calcのデータ更新
        /// エリア・車種サイズキーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_CustomerUriageCalc">顧客売上計算データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerUriageCalcData(Data.M_Customer_Uriage_Calc m_CustomerUriageCalc)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_CustomerUriageCalc == null)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    Data.M_Customer_Uriage_Calc data = await _context.M_Customer_Uriage_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == m_CustomerUriageCalc.Customer_Branch_ID);
                    if (data != null)
                    {
                        _context.M_Customer_Uriage_Calcs.Update(m_CustomerUriageCalc);
                    } else
                    {
                        await _context.M_Customer_Uriage_Calcs.AddRangeAsync(m_CustomerUriageCalc);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_Uriage_Calc

        #region M_Customer_Shiharai_Calc
        /// <summary>
        /// M_Customer_Shiharai_Calcのデータ更新
        /// エリア・車種サイズキーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_CustomerShiharaiCalc">顧客支払い計算データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCustomerShiharaiCalcData(Data.M_Customer_Shiharai_Calc m_CustomerShiharaiCalc)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (m_CustomerShiharaiCalc == null)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    Data.M_Customer_Shiharai_Calc data = await _context.M_Customer_Shiharai_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == m_CustomerShiharaiCalc.Customer_Branch_ID);
                    if (data != null)
                    {
                        _context.M_Customer_Shiharai_Calcs.Update(m_CustomerShiharaiCalc);
                    }
                    else
                    {
                        await _context.M_Customer_Shiharai_Calcs.AddRangeAsync(m_CustomerShiharaiCalc);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer_Shiharai_Calc

        #region M_Customer TracmateLink
        /// <summary>
        /// トラックメイトからの連携データを登録更新する
        /// </summary>
        /// <param name="dto">顧客トラックメイトデータ</param>
        /// <returns>結果</returns>
        public async Task<MsterDataCommonResultValDto> InsertUpdateCustomerTracmateData(CustomerTruckmeteModalDto dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    DateTime dateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    if (dto.CustomerData.Customer_ID == 0)
                    {
                        dto.CustomerData.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        dto.CustomerData.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customers.Add(dto.CustomerData);
                        _context.SaveChanges();
                    }
                    else
                    {
                        //Data.M_Customer data = await _context.M_Customers.FirstOrDefaultAsync(m => m.Customer_ID == dto.CustomerData.Customer_ID);
                        //if (data == null) { throw new Exception("対象データが存在しません。データ不整合が発生しています。"); }
                        ////CopyProperty(data, dto.CustomerData);
                        //data.Customer_Name = dto.CustomerData.Customer_Name;
                        //data.Customer_Name_Abbr = dto.CustomerData.Customer_Name_Abbr;
                        //data.Customer_Name_Kana = dto.CustomerData.Customer_Name_Kana;
                        //data.PostCode = dto.CustomerData.PostCode;
                        //data.Address1 = dto.CustomerData.Address1;
                        //data.Address2 = dto.CustomerData.Address2;
                        //data.Customer_Name_Kana = dto.CustomerData.Customer_Name_Kana;
                        //data.Customer_Name_Kana = dto.CustomerData.Customer_Name_Kana;
                        //data.Customer_Name_Kana = dto.CustomerData.Customer_Name_Kana;
                        //data.Customer_Name_Kana = dto.CustomerData.Customer_Name_Kana;
                        //data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        //_context.M_Customers.Update(data);
                    }

                    if (dto.CustomerBranchData.Customer_Branch_ID == 0)
                    {
                        dto.CustomerBranchData.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        dto.CustomerBranchData.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        dto.CustomerBranchData.Customer_ID = dto.CustomerData.Customer_ID;
                        dto.CustomerBranchData.Seikyu_Atesaki = dto.CustomerBranchData.Customer_Branch_Name;
                        dto.CustomerBranchData.Shiharai_Atesaki = dto.CustomerBranchData.Customer_Branch_Name;
                        _context.M_Customer_Branches.Add(dto.CustomerBranchData);
                    }
                    else
                    {
                        Data.M_Customer_Branch data = await _context.M_Customer_Branches.FirstOrDefaultAsync(m => m.Customer_Branch_ID == dto.CustomerBranchData.Customer_Branch_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, dto.CustomerBranchData, "Insert_Datetime,Insert_User");
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Branches.Update(data);
                    }

                    _context.SaveChanges();

                    ////振られたCustomer_Branch_IDを各リストに設定
                    //if (dto.CustomerUriageCalcData != null) dto.CustomerUriageCalcData.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID;
                    //if (dto.CustomerShiharaiCalcData != null) dto.CustomerShiharaiCalcData.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID;
                    //if (dto.CustomerTollSeikyuKubunList != null) dto.CustomerTollSeikyuKubunList.ForEach(m => m.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID);
                    ////if (dto.CustomerICSeikyuKubunList != null) dto.CustomerICSeikyuKubunList.ForEach(m => m.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID);

                    foreach (Data.M_Customer_Tantou tantou in dto.CustomerTantouList)
                    {
                        if (tantou.Tantou_ID == 0)
                        {
                            tantou.Insert_Datetime = dateTime;
                            tantou.Update_Datetime = dateTime;
                            tantou.Customer_Branch_ID = dto.CustomerBranchData.Customer_Branch_ID;
                            tantou.Customer_ID = dto.CustomerBranchData.Customer_ID;
                            _context.M_Customer_Tantous.Add(tantou);
                        }
                        else
                        {
                            Data.M_Customer_Tantou data = await _context.M_Customer_Tantous.FirstOrDefaultAsync(m => m.Tantou_ID == tantou.Tantou_ID);
                            if (data == null)
                            {
                                throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                            }
                            CopyProperty(data, tantou, "Insert_Datetime,Insert_User");
                            data.Update_Datetime = dateTime;
                            _context.M_Customer_Tantous.Update(data);
                        }
                    }


                    //if (dto.CustomerUriageCalcData != null && dto.CustomerUriageCalcData.Customer_Branch_ID > 0)
                    //{
                    //    Data.M_Customer_Uriage_Calc data = await _context.M_Customer_Uriage_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == dto.CustomerUriageCalcData.Customer_Branch_ID);
                    //    if (data != null)
                    //    {
                    //        _context.M_Customer_Uriage_Calcs.Update(dto.CustomerUriageCalcData);
                    //    }
                    //    else
                    //    {
                    //        await _context.M_Customer_Uriage_Calcs.AddRangeAsync(dto.CustomerUriageCalcData);
                    //    }
                    //}

                    //if (dto.CustomerShiharaiCalcData != null && dto.CustomerShiharaiCalcData.Customer_Branch_ID > 0)
                    //{
                    //    Data.M_Customer_Shiharai_Calc data = await _context.M_Customer_Shiharai_Calcs.FirstOrDefaultAsync(m => m.Customer_Branch_ID == dto.CustomerShiharaiCalcData.Customer_Branch_ID);
                    //    if (data != null)
                    //    {
                    //        _context.M_Customer_Shiharai_Calcs.Update(dto.CustomerShiharaiCalcData);
                    //    }
                    //    else
                    //    {
                    //        await _context.M_Customer_Shiharai_Calcs.AddRangeAsync(dto.CustomerShiharaiCalcData);
                    //    }
                    //}

                    //if (dto.CustomerICSeikyuKubunList != null && dto.CustomerICSeikyuKubunList[0].Customer_Branch_ID > 0)
                    //{
                    //    List<Data.M_Customer_ICSeikyuKubun> data = await _context.M_Customer_ICSeikyuKubuns.Where(m => m.Customer_Branch_ID == dto.CustomerICSeikyuKubunList[0].Customer_Branch_ID).ToListAsync();
                    //    if (data != null)
                    //    {
                    //        _context.M_Customer_ICSeikyuKubuns.RemoveRange(data);
                    //    }

                    //    await _context.M_Customer_ICSeikyuKubuns.AddRangeAsync(dto.CustomerICSeikyuKubunList);
                    //}

                    //if (dto.CustomerTollSeikyuKubunList != null && dto.CustomerTollSeikyuKubunList.Count > 0)
                    //{
                    //    dto.CustomerTollSeikyuKubunList.ForEach(m => { m.Update_Datetime = dateTime; m.Insert_Datetime = dateTime; });

                    //    List<Data.M_Customer_TollSeikyuKubun> data = await _context.M_Customer_TollSeikyuKubuns.Where(m => m.Customer_Branch_ID == dto.CustomerTollSeikyuKubunList[0].Customer_Branch_ID).ToListAsync();
                    //    if (data != null)
                    //    {
                    //        _context.M_Customer_TollSeikyuKubuns.RemoveRange(data);
                    //    }

                    //    await _context.M_Customer_TollSeikyuKubuns.AddRangeAsync(dto.CustomerTollSeikyuKubunList);
                    //}

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Customer TracmateLink

        #region M_Anken_Excharge
        /*****************************************************************************
          M_Anken_Excharge
          *****************************************************************************/
        /// <summary>
        /// M_Anken_Exchargeマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_Anken_Excharge">案件追加料金データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateAnkenExchargeMasterData(Data.M_Anken_Excharge M_Anken_Excharge)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_Anken_Excharge.SortOrder == 0)
                    {
                        IEnumerable<Data.M_Anken_Excharge> list = await _context.M_Anken_Excharges.Where(m => m.Company_ID == M_Anken_Excharge.Company_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Any()) { iMax = list.Max(m => m.SortOrder); }
                        M_Anken_Excharge.SortOrder = iMax + 1;
                        M_Anken_Excharge.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Anken_Excharges.Add(M_Anken_Excharge);
                    }
                    else
                    {
                        var data = await _context.M_Anken_Excharges.FirstOrDefaultAsync(
                            m => m.Company_ID == M_Anken_Excharge.Company_ID &&
                            m.SIZE == M_Anken_Excharge.SIZE && m.Komoku_Key == M_Anken_Excharge.Komoku_Key)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, M_Anken_Excharge);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Anken_Excharges.Update(data);
                    }


                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

        ///// <summary>
        ///// M_Anken_Exchargeマスタの削除処理
        ///// </summary>
        ///// <param name="M_Anken_Excharge"></param>
        ///// <returns></returns>
        //public async Task<Dto.MsterDataCommonResultValDto> DeleteAnkenExchargeMasterData(Data.M_Anken_Excharge M_Anken_Excharge)
        //{

        //    Dto.MsterDataCommonResultValDto resultVal = new();

        //    try
        //    {
        //        using var tran = _context.Database.BeginTransaction();
        //        try
        //        {
        //            Data.M_Anken_Excharge data = await _context.M_Anken_Excharges.FirstOrDefaultAsync(m => m.Company_ID == M_Anken_Excharge.Company_ID &&
        //                                                                m.SIZE == M_Anken_Excharge.SIZE && m.Komoku_Key == M_Anken_Excharge.Komoku_Key);
        //            if (data == null)
        //            {
        //                throw new Exception("対象データが存在しません。データ不整合が発生しています。");
        //            }

        //            //論理削除処理
        //            data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        //            data.DEL_FLG = true;

        //            _context.M_Anken_Excharges.Update(data);
        //            _context.SaveChanges();

        //            tran.Commit();
        //            resultVal.RetrunFlg = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultVal.ErrrMessage = ex.Message;
        //        resultVal.RetrunFlg = false;
        //    }
        //    finally
        //    {

        //    }

        //    return resultVal;
        //}


        /// <summary>
        /// M_Anken_Exchargeマスタの並び順変更処理
        /// </summary>
        /// <param name="dto">案件追加料金データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateAnkenExchargeSortOrder(AnkenExchargeListDto dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_Anken_Excharge> m_Anken_Excharge_s = await _context.M_Anken_Excharges.Where(m => m.Company_ID == dto.CompanyID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_Anken_Excharge> M_Anken_ExchargeUpdate = dto.M_AnkenExchargeList.OrderBy(m => m.SortOrder).ToList();


                using var tran = _context.Database.BeginTransaction();
                try
                {
                    foreach (Data.M_Anken_Excharge target in M_Anken_ExchargeUpdate)
                    {

                        var m_Anken_Excharge = m_Anken_Excharge_s.FirstOrDefault(m => m.SIZE == target.SIZE && m.Komoku_Key == target.Komoku_Key);
                        if (m_Anken_Excharge != null && m_Anken_Excharge.SortOrder != target.SortOrder)
                        {
                            m_Anken_Excharge.SortOrder = target.SortOrder;
                            _context.M_Anken_Excharges.Update(m_Anken_Excharge);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Anken_Excharge

        #region M_LoginUser_Role
        /*****************************************************************************
          M_LoginUser_Role
          *****************************************************************************/
        /// <summary>
        /// M_LoginUser_Roleマスタの登録更処理
        /// Roleが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_LoginUser_Role">ログインユーザーロールデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateLoginUserRoleMasterData(Data.M_LoginUser_Role　m_LoginUser_Role)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_LoginUser_Role.Role == 0)
                    {
                        IEnumerable<Data.M_LoginUser_Role> list = await _context.M_LoginUser_Roles.Where(m => m.Company_ID == m_LoginUser_Role.Company_ID).ToListAsync();
                        int? iMax = 0;
                        if (list.Count() > 0) { iMax = list.Max(m => m.Role); }
                        m_LoginUser_Role.Role = (int)iMax + 1;
                        m_LoginUser_Role.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_LoginUser_Roles.Add(m_LoginUser_Role);
                    }
                    else
                    {
                        var data = await _context.M_LoginUser_Roles.FirstOrDefaultAsync(
                            m => m.Company_ID == m_LoginUser_Role.Company_ID &&
                            m.Role == m_LoginUser_Role.Role)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, m_LoginUser_Role);
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_LoginUser_Roles.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_LoginUser_Role

        #region M_LoginUser
        /*****************************************************************************
          M_LoginUser
          *****************************************************************************/
        /// <summary>
        /// M_LoginUserマスタの登録更処理
        /// Roleが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_LoginUser">ログインユーザーデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateLoginUserMasterData(Data.M_LoginUser m_LoginUser)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_LoginUser.LoginUser_ID == 0)
                    {
                        m_LoginUser.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_LoginUsers.Add(m_LoginUser);
                    }
                    else
                    {
                        var data = await _context.M_LoginUsers.FirstOrDefaultAsync(m => m.LoginUser_ID == m_LoginUser.LoginUser_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        string pass = data.Password;
                        CopyProperty(data, m_LoginUser);
                        data.Password = pass;
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_LoginUsers.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

        /// <summary>
        /// M_LoginUserマスタのパスワード更新
        /// </summary>
        /// <param name="M_LoginUser">ログインユーザーデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateLoginUserPass(Data.M_LoginUser m_LoginUser)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_LoginUser.LoginUser_ID == 0)
                    {
                        throw new Exception("登録データに不具合があります。システム管理者に問い合わせてください。");
                    }
                    else
                    {
                        var data = await _context.M_LoginUsers.FirstOrDefaultAsync(m => m.LoginUser_ID == m_LoginUser.LoginUser_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        data.Password = m_LoginUser.Password;
                        data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_LoginUsers.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_LoginUser

        #region M_CompanyDriver_Syaryo
        /*****************************************************************************
          M_CompanyDriver_Syaryo
          *****************************************************************************/
        /// <summary>
        /// M_CompanyDriver_Syaryoマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_CompanyDriver_Syaryo">会社ドライバー車両データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateCompanyDriverSyaryoMasterData(List<Data.M_CompanyDriver_Syaryo> M_CompanyDriver_Syaryos)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.M_CompanyDriver_Syaryo> syaryo = await _context.M_CompanyDriver_Syaryos.Where(m => m.Driver_ID == M_CompanyDriver_Syaryos[0].Driver_ID).ToListAsync();


                    foreach(Data.M_CompanyDriver_Syaryo target in M_CompanyDriver_Syaryos)
                    {
                        if (target.DriverSyaryo_ID == 0)
                        {
                            //M_CompanyDriver_Syaryo.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            _context.M_CompanyDriver_Syaryos.Add(target);
                        }
                        else
                        {
                            Data.M_CompanyDriver_Syaryo data = await _context.M_CompanyDriver_Syaryos.FirstOrDefaultAsync(m => m.DriverSyaryo_ID == target.DriverSyaryo_ID);
                            CopyProperty(data, target);
                            //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            _context.M_CompanyDriver_Syaryos.Update(data);
                        }

                        _context.SaveChanges();

                    }

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_CompanyDriver_Syaryo

        #region M_Yosya
        /// <summary>
        /// 傭車乗務員マスタの登録更処理
        /// 各IDが0が登録、0以外が更新
        /// </summary>
        /// <param name="m_Yosya">傭車データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateYosyaData(Dto.YosyaModalDto dto)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (dto.CustomerDriverData.Customer_Driver_ID == 0)
                    {
                        dto.CustomerDriverData.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        dto.CustomerDriverData.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Drivers.Add(dto.CustomerDriverData);
                    }
                    else
                    {
                        Data.M_Customer_Driver data = await _context.M_Customer_Drivers.FirstOrDefaultAsync(m => m.Customer_Driver_ID == dto.CustomerDriverData.Customer_Driver_ID);
                        if (data == null) throw new Exception("対象データが存在しません。データ不整合が発生しています。");

                        CopyProperty(data, dto.CustomerDriverData, "Customer_Driver_ID,Customer_ID,Customer_Branch_ID,Insert_Datetime,Insert_User");
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Drivers.Update(data);
                    }

                    _context.SaveChanges();


                    if (dto.CustomerDriverSyaryoData.Customer_DriverSyaryo_ID == 0)
                    {
                        dto.CustomerDriverSyaryoData.Customer_Driver_ID = dto.CustomerDriverData.Customer_Driver_ID;
                        dto.CustomerDriverSyaryoData.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        dto.CustomerDriverSyaryoData.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Driver_Syaryos.Add(dto.CustomerDriverSyaryoData);
                    }
                    else
                    {
                        Data.M_Customer_Driver_Syaryo data = await _context.M_Customer_Driver_Syaryos.FirstOrDefaultAsync(m => m.Customer_Driver_ID == dto.CustomerDriverData.Customer_Driver_ID);
                        if (data == null) throw new Exception("対象データが存在しません。データ不整合が発生しています。");

                        CopyProperty(data, dto.CustomerDriverSyaryoData, "Customer_DriverSyaryo_ID,Customer_Driver_ID,Customer_ID,Customer_Branch_ID,Insert_Datetime,Insert_User");
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Customer_Driver_Syaryos.Update(data);
                    }

                    if (dto.CustomerDataTantouHaisyaGroupList != null)
                    {
                        List<Data.M_Customer_TantouHaisyaGroup> group = await _context.M_Customer_TantouHaisyaGroups.Where(m => m.Customer_ID == dto.CustomerDriverData.Customer_ID).ToListAsync();
                        if (group != null || group.Count > 0) _context.M_Customer_TantouHaisyaGroups.RemoveRange(group);
                        _context.SaveChanges();

                        foreach (var item in dto.CustomerDataTantouHaisyaGroupList)
                        {
                            _context.M_Customer_TantouHaisyaGroups.Add(item);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Yosya

        #region M_SyasyuKubun
        /*****************************************************************************
          M_SyasyuKubun
          *****************************************************************************/
        /// <summary>
        /// M_SyasyuKubunマスタの並び順変更処理
        /// </summary>
        /// <param name="dto">車種区分データリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateSyasyuKubunListSortOrder(Dto.SyasyuKubunListDto dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_SyasyuKubun> M_SyasyuKubuns = await _context.M_SyasyuKubuns.Where(m => m.Company_ID == dto.CompanyID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_SyasyuKubun> M_SyasyuKubunsUpdate = dto.SyasyuKubunList.OrderBy(m => m.SortOrder).ToList();

                using var tran = _context.Database.BeginTransaction();
                try
                {

                    foreach (Data.M_SyasyuKubun target in M_SyasyuKubunsUpdate)
                    {
                        var M_SyasyuKubun = M_SyasyuKubuns.FirstOrDefault(m => m.SyasyuKubun_ID == target.SyasyuKubun_ID);
                        if (M_SyasyuKubun != null && M_SyasyuKubun.SortOrder != target.SortOrder)
                        {
                            M_SyasyuKubun.SortOrder = target.SortOrder;

                            _context.M_SyasyuKubuns.Update(M_SyasyuKubun);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

        /// <summary>
        /// M_SyasyuKubunマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_SyasyuKubun">車種区分データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateSyasyuKubunMasterData(Data.M_SyasyuKubun M_SyasyuKubun)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_SyasyuKubun.SyasyuKubun_ID == 0)
                    {
                        IEnumerable<Data.M_SyasyuKubun> list = await _context.M_SyasyuKubuns.Where(m => m.Company_ID == M_SyasyuKubun.Company_ID
                                                                                            && m.SIZE == M_SyasyuKubun.SIZE
                                                                                            && m.Kata_ID == M_SyasyuKubun.Kata_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Any()) { iMax = list.Max(m => m.SortOrder); }
                        M_SyasyuKubun.SortOrder = iMax + 1;
                        //M_SyasyuKubun.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_SyasyuKubuns.Add(M_SyasyuKubun);
                    }
                    else
                    {
                        Data.M_SyasyuKubun data = await _context.M_SyasyuKubuns.FirstOrDefaultAsync(m => m.SyasyuKubun_ID == M_SyasyuKubun.SyasyuKubun_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, M_SyasyuKubun);
                        //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_SyasyuKubuns.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }

 

        /// <summary>
        /// M_SyasyuKubunマスタの削除処理
        /// </summary>
        /// <param name="M_SyasyuKubun">車種区分データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> DeleteSyasyuKubunMasterData(Data.M_SyasyuKubun M_SyasyuKubun)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var data = await _context.M_SyasyuKubuns.FirstOrDefaultAsync(
                        m => m.Company_ID == M_SyasyuKubun.Company_ID &&
                        m.SyasyuKubun_ID == M_SyasyuKubun.SyasyuKubun_ID)
                        ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");

                    /*
                     * データの整合性チェック
                     */

                    //論理削除処理
                    //data.UP_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    data.Del_Flg = true;

                    _context.M_SyasyuKubuns.Update(data);
                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_SyasyuKubun

        #region M_Luggage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_Luggage">荷物データ</param>
        /// <returns>結果</returns>
        public async Task<MsterDataCommonResultValDto> InsertUpdateLuggageMasterData(M_Luggage m_Luggage)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_Luggage.SortOrder == 0)
                    {
                        IEnumerable<Data.M_Luggage> list = await _context.M_Luggages.Where(m => m.Company_ID == m_Luggage.Company_ID
                                                                                && m.Luggage_Group_ID == m_Luggage.Luggage_Group_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Any()) { iMax = list.Max(m => m.SortOrder); }
                        m_Luggage.SortOrder = iMax + 1;
                        m_Luggage.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Luggages.Add(m_Luggage);

                    } else
                    {
                        var data = await _context.M_Luggages.FirstOrDefaultAsync(
                            m => m.Company_ID == m_Luggage.Company_ID &&
                            m.Luggage_ID == m_Luggage.Luggage_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        data.Luggage_Name = m_Luggage.Luggage_Name;
                        data.Unit_Name = m_Luggage.Unit_Name;
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Luggage

        #region M_Burden
        /// <summary>
        /// M_Burdenマスタの追加処理
        /// </summary>
        /// <param name="m_Burden">負担データ</param>
        /// <returns>結果</returns>
        public async Task<MsterDataCommonResultValDto> InsertUpdateBurdenMasterData(M_Burden m_Burden)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_Burden.SortOrder == 0)
                    {
                        IEnumerable<Data.M_Burden> list = await _context.M_Burdens.Where(m => m.Company_ID == m_Burden.Company_ID
                                                                                && m.Burden_Group_ID == m_Burden.Burden_Group_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Any()) { iMax = list.Max(m => m.SortOrder); }
                        m_Burden.SortOrder = iMax + 1;
                        m_Burden.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Burdens.Add(m_Burden);

                    } else
                    {
                        var data = await _context.M_Luggages.FirstOrDefaultAsync(
                            m => m.Company_ID == m_Burden.Company_ID &&
                            m.Luggage_ID == m_Burden.Burden_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        data.Luggage_Name = m_Burden.Burden_Name;
                        data.Unit_Name = m_Burden.Unit_Name;
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Burden

        #region M_Equipment
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_Equipment">機器データ</param>
        /// <returns>結果</returns>
        public async Task<MsterDataCommonResultValDto> InsertUpdateEquipmentMasterData(M_Equipment m_Equipment)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (m_Equipment.SortOrder == 0)
                    {
                        IEnumerable<Data.M_Equipment> list = await _context.M_Equipments.Where(m => m.Company_ID == m_Equipment.Company_ID
                                                                                && m.Equipment_Group_ID == m_Equipment.Equipment_Group_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Any()) { iMax = list.Max(m => m.SortOrder); }
                        m_Equipment.SortOrder = iMax + 1;
                        m_Equipment.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Equipments.Add(m_Equipment);

                    }
                    else
                    {
                        var data = await _context.M_Equipments.FirstOrDefaultAsync(
                            m => m.Company_ID == m_Equipment.Company_ID &&
                            m.Equipment_ID == m_Equipment.Equipment_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        data.Equipment_Name = m_Equipment.Equipment_Name;
                        data.Unit_Name = m_Equipment.Unit_Name;
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Equipment

        #region M_Report_Output_Item
        /// <summary>
        /// M_Report_Output_Itemマスタの登録更処理
        /// </summary>
        /// <param name="datas">レポート出力項目データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertReportOutputItems(CreateReportOutputItemDto datas)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            if (datas.CompanyID == 0)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<M_Report_Output_Item> oldData = await _context.M_Report_Output_Items.Where(m => m.User_ID == datas.UserID && m.Company_ID == datas.CompanyID).ToListAsync();

                    if (oldData != null)
                    {
                        _context.M_Report_Output_Items.RemoveRange(oldData);
                    }

                    await _context.M_Report_Output_Items.AddRangeAsync(datas.ReportOutputItems);

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Report_Output_Item

        #region M_Vender
        /// <summary>
        /// M_Venderマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_Vender">ベンダーデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateVenderData(Dto.VenderModalDto dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (dto.VenderData.Vender_ID == 0)
                    {
                        dto.VenderData.Insert_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Venders.Add(dto.VenderData);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var data = await _context.M_Venders.FirstOrDefaultAsync(m => m.Vender_ID == dto.VenderData.Vender_ID)
                            ?? throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        CopyProperty(data, dto.VenderData);
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Venders.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {

            }

            return resultVal;
        }
        #endregion M_Vender

        #region M_Luggage_Group
        /*****************************************************************************
          M_Luggage_Group
          *****************************************************************************/
        /// <summary>
        /// M_Luggage_Groupマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_Luggage_Group">荷物グループデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateLuggageGroupMasterData(Data.M_Luggage_Group M_Luggage_Group)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_Luggage_Group.Luggage_Group_ID == 0)
                    {
                        IEnumerable<Data.M_Luggage_Group> list = await _context.M_Luggage_Groups.Where(m => m.Company_ID == M_Luggage_Group.Company_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Count() > 0) { iMax = list.Max(m => m.SortOrder); }
                        M_Luggage_Group.SortOrder = iMax + 1;
                        M_Luggage_Group.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Luggage_Groups.Add(M_Luggage_Group);
                    }
                    else
                    {
                        Data.M_Luggage_Group data = await _context.M_Luggage_Groups.FirstOrDefaultAsync(m => m.Company_ID == M_Luggage_Group.Company_ID &&
                                                                            m.Luggage_Group_ID == M_Luggage_Group.Luggage_Group_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, M_Luggage_Group);
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Luggage_Groups.Update(data);
                    }


                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }

            return resultVal;
        }


        /// <summary>
        /// M_Luggage_Groupマスタの並び順変更処理
        /// </summary>
        /// <param name="dto">荷物グループデータリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateLuggageGroupListSortOrder(List<Data.M_Luggage_Group> dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_Luggage_Group> m_Anken_Excharge_s = await _context.M_Luggage_Groups.Where(m => m.Company_ID == dto[0].Company_ID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_Luggage_Group> M_Luggage_GroupUpdate = dto.OrderBy(m => m.SortOrder).ToList();


                using var tran = _context.Database.BeginTransaction();
                try
                {
                    foreach (Data.M_Luggage_Group target in M_Luggage_GroupUpdate)
                    {

                        Data.M_Luggage_Group m_Anken_Excharge = m_Anken_Excharge_s.FirstOrDefault(m => m.Luggage_Group_ID == target.Luggage_Group_ID);
                        if (m_Anken_Excharge != null && m_Anken_Excharge.SortOrder != target.SortOrder)
                        {
                            m_Anken_Excharge.SortOrder = target.SortOrder;
                            _context.M_Luggage_Groups.Update(m_Anken_Excharge);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }

            return resultVal;
        }
        #endregion M_Luggage_Group

        #region M_Equipment_Group
        /*****************************************************************************
          M_Equipment_Group
          *****************************************************************************/
        /// <summary>
        /// M_Equipment_Groupマスタの登録更処理
        /// SortOrderが0が登録、0以外が更新
        /// </summary>
        /// <param name="M_Equipment_Group">機器グループデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateEquipmentGroupMasterData(Data.M_Equipment_Group M_Equipment_Group)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (M_Equipment_Group.Equipment_Group_ID == 0)
                    {
                        IEnumerable<Data.M_Equipment_Group> list = await _context.M_Equipment_Groups.Where(m => m.Company_ID == M_Equipment_Group.Company_ID).ToListAsync();
                        int iMax = 0;
                        if (list.Count() > 0) { iMax = list.Max(m => m.SortOrder); }
                        M_Equipment_Group.SortOrder = iMax + 1;
                        M_Equipment_Group.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Equipment_Groups.Add(M_Equipment_Group);
                    }
                    else
                    {
                        Data.M_Equipment_Group data = await _context.M_Equipment_Groups.FirstOrDefaultAsync(m => m.Company_ID == M_Equipment_Group.Company_ID &&
                                                                            m.Equipment_Group_ID == M_Equipment_Group.Equipment_Group_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, M_Equipment_Group);
                        data.Update_Datetime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        _context.M_Equipment_Groups.Update(data);
                    }


                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }

            return resultVal;
        }


        /// <summary>
        /// M_Equipment_Groupマスタの並び順変更処理
        /// </summary>
        /// <param name="dto">機器グループデータリスト</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateEquipmentGroupListSortOrder(List<Data.M_Equipment_Group> dto)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {

                IEnumerable<Data.M_Equipment_Group> m_Anken_Excharge_s = await _context.M_Equipment_Groups.Where(m => m.Company_ID == dto[0].Company_ID).OrderBy(m => m.SortOrder).ToListAsync();

                List<Data.M_Equipment_Group> M_Equipment_GroupUpdate = dto.OrderBy(m => m.SortOrder).ToList();


                using var tran = _context.Database.BeginTransaction();
                try
                {
                    foreach (Data.M_Equipment_Group target in M_Equipment_GroupUpdate)
                    {

                        Data.M_Equipment_Group m_Anken_Excharge = m_Anken_Excharge_s.FirstOrDefault(m => m.Equipment_Group_ID == target.Equipment_Group_ID);
                        if (m_Anken_Excharge != null && m_Anken_Excharge.SortOrder != target.SortOrder)
                        {
                            m_Anken_Excharge.SortOrder = target.SortOrder;
                            _context.M_Equipment_Groups.Update(m_Anken_Excharge);
                        }
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }

            return resultVal;
        }
        #endregion M_Equipment_Group

        #region M_Senzoku
        /// <summary>
        /// M_Senzokuマスタの登録更処理
        /// Senzoku_IDが0が登録、0以外が更新
        /// </summary>
        /// <param name="senzoku">専属データ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateSenzokuData(Data.M_Senzoku senzoku)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (senzoku.SenzokuID == 0)
                    {
                        _context.M_Senzokus.Add(senzoku);
                    }
                    else
                    {
                        Data.M_Senzoku data = await _context.M_Senzokus.FirstOrDefaultAsync(m => m.SenzokuID == senzoku.SenzokuID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, senzoku, "SenzokuID,Company_ID");
                        _context.M_Senzokus.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }

            return resultVal;
        }
        #endregion M_Senzoku

        #region M_Senzoku_Driver
        /// <summary>
        /// M_Senzoku_Driverマスタの登録更処理
        /// Senzoku_Driver_IDが0が登録、0以外が更新
        /// </summary>
        /// <param name="senzoku">専属ドライバーデータ</param>
        /// <returns>結果</returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateSenzokuDriverData(Data.M_Senzoku_Driver senzoku)
        {

            Dto.MsterDataCommonResultValDto resultVal = new();

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    if (senzoku.Senzoku_Driver_ID == 0)
                    {
                        _context.M_Senzoku_Drivers.Add(senzoku);
                    }
                    else
                    {
                        Data.M_Senzoku_Driver data = await _context.M_Senzoku_Drivers.FirstOrDefaultAsync(m => m.Senzoku_Driver_ID == senzoku.Senzoku_Driver_ID);
                        if (data == null)
                        {
                            throw new Exception("対象データが存在しません。データ不整合が発生しています。");
                        }
                        CopyProperty(data, senzoku, "Senzoku_Driver_ID,SenzokuID,Company_ID");
                        _context.M_Senzoku_Drivers.Update(data);
                    }

                    _context.SaveChanges();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }

            return resultVal;
        }
        #endregion M_Senzoku_Driver
    }
}
