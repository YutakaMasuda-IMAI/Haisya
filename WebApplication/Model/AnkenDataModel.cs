using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// 案件データモデルクラス
    /// </summary>
    public class AnkenDataModel: BaseModel
    {
        public AnkenDataModel(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// T_Ankenの新規登録
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T_Anken> AddNewAnkenData(AnkenDataModelDto data)
        {
            int ankenId = 0;
            int ankenOrder = 1;
            bool flgNew = false;

            if (data.T_Anken.Anken_ID == 0)
            {
                flgNew = true;
                DateTime targetDate = (DateTime)data.T_Anken_PointList[0].PointDate;
                string ankenNo = GetAnkenNo(targetDate);
                data.T_Anken.Anken_No = ankenNo;
                data.T_Anken.Anken_Latest_Order = ankenOrder;
                
                //Anken_IDの取得
                ankenId = GetAnkenIDToAddNew(data.T_Anken);
                data.T_Anken.Anken_ID = ankenId;
            } else
            {
                ankenId = data.T_Anken.Anken_ID;
                ankenOrder = UpdateAnkenOrder(ankenId);
            }

            using var tran = _context.Database.BeginTransaction();
            try
            {
                if (flgNew)
                {
                    //T_Ankenの更新
                    T_Anken anken = await _context.T_Ankens.FirstOrDefaultAsync(m => m.Anken_ID == ankenId);
                    anken.SenzokuID = data.T_Anken.SenzokuID;
                    anken.Senzoku_Driver_ID = data.T_Anken.Senzoku_Driver_ID;
                }

                // T_Anken_Detail作成
                T_Anken_Detail t_Anken_Detail = new();
                CopyProperty(t_Anken_Detail, data.T_Anken_Detail);
                t_Anken_Detail.Anken_ID = ankenId;
                t_Anken_Detail.Anken_Order = ankenOrder;
                _context.T_Anken_Details.Add(t_Anken_Detail);

                // T_Anken_Publish作成
                var t_Anken_Publish1 = await _context.T_Anken_Publishes.FirstOrDefaultAsync(m => m.Anken_ID == data.T_Anken.Anken_ID);
                if (t_Anken_Publish1 != null)
                {
                    _context.T_Anken_Publishes.Remove(t_Anken_Publish1);
                }
                T_Anken_Publish t_Anken_Publish = new();
                CopyProperty(t_Anken_Publish, data.T_Anken_Publish);
                t_Anken_Publish.Anken_ID = ankenId;
                _context.T_Anken_Publishes.Add(t_Anken_Publish);

                // T_Anken_Remarks作成
                if (data.T_Anken_Remarks != null) { 
                    T_Anken_Remark t_Anken_Remarks = new();
                    CopyProperty(t_Anken_Remarks, data.T_Anken_Remarks);
                    t_Anken_Remarks.Anken_ID = ankenId;
                    t_Anken_Remarks.Anken_Order = ankenOrder;
                    _context.T_Anken_Remarks.Add(t_Anken_Remarks);
                }

                // T_Anken_Luggage作成
                if (data.T_Anken_LuggageList != null) { 
                    foreach(T_Anken_Luggage t_Anken_Luggage in data.T_Anken_LuggageList)
                    {
                    t_Anken_Luggage.Anken_ID = ankenId;
                    t_Anken_Luggage.Anken_Order = ankenOrder;
                    }

                    await _context.T_Anken_Luggages.AddRangeAsync(data.T_Anken_LuggageList);
                }

                // T_Anken_Equipment作成
                if (data.T_Anken_EquipmentList != null)
                {
                    foreach(T_Anken_Equipment t_Anken_Equipment in data.T_Anken_EquipmentList)
                    {
                    t_Anken_Equipment.Anken_ID = ankenId;
                    t_Anken_Equipment.Anken_Order = ankenOrder;
                    }

                    await _context.T_Anken_Equipments.AddRangeAsync(data.T_Anken_EquipmentList);
                }

                // T_Anken_Point 作成
                foreach (var target in data.T_Anken_PointList)
                {
                    T_Anken_Point t_Anken_Point = new();
                    CopyProperty(t_Anken_Point, target);
                    t_Anken_Point.Anken_ID = ankenId;
                    t_Anken_Point.Anken_Order = ankenOrder;
                    _context.T_Anken_Points.Add(t_Anken_Point);
                }

                // T_Anken_OyaKokyaku 作成
                if (data.T_Anken_OyaKokyakuList != null)
                {
                    foreach (var target in data.T_Anken_OyaKokyakuList)
                    {
                        T_Anken_OyaKokyaku t_Anken_OyaKokyaku = new();
                        CopyProperty(t_Anken_OyaKokyaku, target);
                        t_Anken_OyaKokyaku.Anken_ID = ankenId;
                        t_Anken_OyaKokyaku.Anken_Order = ankenOrder;
                        _context.T_Anken_OyaKokyakus.Add(t_Anken_OyaKokyaku);
                    }
                }

                // T_Anken_Excharge 作成
                if (data.T_Anken_ExchargeList != null) { 
                    foreach (var target in data.T_Anken_ExchargeList)
                    {
                        T_Anken_Excharge t_Anken_Excharge = new();
                        CopyProperty(t_Anken_Excharge, target);
                        t_Anken_Excharge.Anken_ID = ankenId;
                        t_Anken_Excharge.Anken_Order = ankenOrder;
                        _context.T_Anken_Excharges.Add(t_Anken_Excharge);
                    }
                }

                // T_Anken_Display 作成
                if (data.T_Anken_DisplayList != null)
                {
                    foreach (T_Anken_Display target in data.T_Anken_DisplayList)
                    {
                        T_Anken_Display t_Anken_Display = await _context.T_Anken_Displays.FirstOrDefaultAsync(m => m.Anken_ID == target.Anken_ID && m.Daisuu_Sort == target.Daisuu_Sort && m.Anken_Key == target.Anken_Key);
                        if (t_Anken_Display == null)
                        {
                            t_Anken_Display = new();
                            CopyProperty(t_Anken_Display, target);
                            t_Anken_Display.Anken_ID = ankenId;
                            _context.T_Anken_Displays.Add(t_Anken_Display);
                        }
                        else
                        {
                            CopyProperty(t_Anken_Display, target, "AnkenDisplay_ID,Anken_ID,Anken_Key,Insert_Datetime,Insert_User");
                        }
                    }
                }

                _context.SaveChanges();

                //専属の場合は即配車処理を実施
                if (data.T_Anken.SenzokuID > 0)
                {
                    //傭車データの配車情報登録処理
                    M_Senzoku_Driver driver = await _context.M_Senzoku_Drivers.FirstOrDefaultAsync(m => m.SenzokuID == data.T_Anken.SenzokuID);
                    await RegsterHaisyaToJisya(true, data.T_Anken, data.T_Anken_Detail.Update_User, driver.Driver_ID, driver.DriverSyaryo_ID);
                } 
                if (data.T_Anken_Detail.HaisyaPlanKubun == 5 && data.T_Anken_Detail.HaisyaDriverID > 0)
                {
                    await RegsterHaisyaToJisya(true, data.T_Anken, data.T_Anken_Detail.Update_User, data.T_Anken_Detail.HaisyaDriverID, data.T_Anken_Detail.HaisyaDriverSyaryoID);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                throw;
            }
            finally
            {
            }

            return data.T_Anken;
        }

        /// <summary>
        /// 案件データ一式の更新処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T_Anken> UpdateAnkenData(AnkenDataModelDto data)
        {
            DateTime dateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            int ankenId = data.T_Anken.Anken_ID;
            int ankenOrder = (int)data.T_Anken.Anken_Latest_Order;

            using var tran = _context.Database.BeginTransaction();
            try
            {
                //T_Anken
                T_Anken t_Ankenl = await _context.T_Ankens.FirstOrDefaultAsync(x => x.Anken_ID == ankenId);
                if (t_Ankenl == null)
                {
                    throw new Exception("T_Anken_Detailデータ不正");
                }
                t_Ankenl.Anken_Status = data.T_Anken.Anken_Status;


                // T_Anken_Detail作成
                T_Anken_Detail t_Anken_Detail = await _context.T_Anken_Details.FirstOrDefaultAsync(x => x.Anken_ID == ankenId && x.Anken_Order == ankenOrder);
                if (t_Anken_Detail == null)
                {
                    throw new Exception("T_Anken_Detailデータ不正");
                }
                
                CopyProperty(t_Anken_Detail, data.T_Anken_Detail, "Insert_Datetime,Insert_User");
                t_Anken_Detail.Anken_ID = ankenId;
                t_Anken_Detail.Anken_Order = ankenOrder;
                //_context.T_Anken_Details.(t_Anken_Detail);

                // T_Anken_Publish作成
                Data.T_Anken_Publish t_Anken_Publish = await _context.T_Anken_Publishes.FirstOrDefaultAsync(x => x.Anken_ID == ankenId);
                if (t_Anken_Publish == null)
                {
                    Data.T_Anken_Publish t_Anken_PublishAdd = new();
                    this.CopyProperty(t_Anken_PublishAdd, data.T_Anken_Publish);
                    t_Anken_PublishAdd.Anken_ID = ankenId;
                    _context.T_Anken_Publishes.Add(t_Anken_PublishAdd);
                    //throw new Exception("T_Anken_Publishデータ不正");
                } else
                {
                    this.CopyProperty(t_Anken_Publish, data.T_Anken_Publish);
                    t_Anken_Publish.Anken_ID = ankenId;
                }

                // T_Anken_Remarks作成
                if (data.T_Anken_Remarks != null) { 
                Data.T_Anken_Remark t_Anken_Remark = await _context.T_Anken_Remarks.FirstOrDefaultAsync(x => x.Anken_ID == ankenId && x.Anken_Order == ankenOrder);
                    if (t_Anken_Remark == null)
                    {
                        Data.T_Anken_Remark t_Anken_Remark_Add = new();
                        this.CopyProperty(t_Anken_Remark_Add, data.T_Anken_Remarks);
                        t_Anken_Remark_Add.Anken_ID = ankenId;
                        t_Anken_Remark_Add.Anken_Order = ankenOrder;
                        _context.T_Anken_Remarks.Add(t_Anken_Remark_Add);
                    }
                    else
                    {
                        this.CopyProperty(t_Anken_Remark, data.T_Anken_Remarks);
                        t_Anken_Remark.Anken_ID = ankenId;
                        t_Anken_Remark.Anken_Order = ankenOrder;
                    }
                }

                // T_Anken_Luggage作成
                if (data.T_Anken_LuggageList != null) { 

                    foreach (Data.T_Anken_Luggage item in data.T_Anken_LuggageList)
                    {
                        item.Anken_ID = ankenId;
                        item.Anken_Order = ankenOrder;
                    }

                    List<Data.T_Anken_Luggage> t_Anken_Luggage = await _context.T_Anken_Luggages.Where(x => x.Anken_ID == ankenId && x.Anken_Order == ankenOrder).ToListAsync();
                    if (t_Anken_Luggage != null)
                    {
                        _context.T_Anken_Luggages.RemoveRange(t_Anken_Luggage);
                    }

                    await _context.T_Anken_Luggages.AddRangeAsync(data.T_Anken_LuggageList);
                }

                // T_Anken_Equipment作成
                if (data.T_Anken_EquipmentList != null)
                {
                    foreach (Data.T_Anken_Equipment item in data.T_Anken_EquipmentList)
                    {
                        item.Anken_ID = ankenId;
                        item.Anken_Order = ankenOrder;
                    }

                    List<Data.T_Anken_Equipment> t_Anken_Equipment = await _context.T_Anken_Equipments.Where(x => x.Anken_ID == ankenId && x.Anken_Order == ankenOrder).ToListAsync();
                    if (t_Anken_Equipment != null)
                    {
                        _context.T_Anken_Equipments.RemoveRange(t_Anken_Equipment);
                    }

                    await _context.T_Anken_Equipments.AddRangeAsync(data.T_Anken_EquipmentList);
                }

                // T_Anken_Point 作成
                List<T_Anken_Point> t_Anken_Points = await _context.T_Anken_Points.Where(x => x.Anken_ID == ankenId && x.Anken_Order == ankenOrder).ToListAsync();
                foreach (T_Anken_Point target in t_Anken_Points)
                {
                    _context.T_Anken_Points.Remove(target);
                }

                foreach (T_Anken_Point target in data.T_Anken_PointList)
                {
                    Data.T_Anken_Point t_Anken_Point = new();
                    this.CopyProperty(t_Anken_Point, target);
                    t_Anken_Point.Anken_ID = ankenId;
                    t_Anken_Point.Anken_Order = ankenOrder;
                    _context.T_Anken_Points.Add(t_Anken_Point);
                }

                // T_Anken_OyaKokyaku 作成
                List<T_Anken_OyaKokyaku> t_Anken_OyaKokyakus = await _context.T_Anken_OyaKokyakus.Where(x => x.Anken_ID == ankenId && x.Anken_Order == ankenOrder).ToListAsync();
                foreach (T_Anken_OyaKokyaku target in t_Anken_OyaKokyakus)
                {
                    _context.T_Anken_OyaKokyakus.Remove(target);
                }

                if (data.T_Anken_OyaKokyakuList != null)
                {
                    foreach (T_Anken_OyaKokyaku target in data.T_Anken_OyaKokyakuList)
                    {
                        Data.T_Anken_OyaKokyaku t_Anken_OyaKokyaku = new();
                        this.CopyProperty(t_Anken_OyaKokyaku, target, "Insert_Datetime,Insert_User");
                        t_Anken_OyaKokyaku.Anken_ID = ankenId;
                        t_Anken_OyaKokyaku.Anken_Order = ankenOrder;
                        _context.T_Anken_OyaKokyakus.Add(t_Anken_OyaKokyaku);
                    }
                }

                // T_Anken_Excharg 作成
                List<Data.T_Anken_Excharge> t_Anken_Excharge_s = await _context.T_Anken_Excharges.Where(x => x.Anken_ID == ankenId && x.Anken_Order == ankenOrder).ToListAsync();
                foreach (Data.T_Anken_Excharge target in t_Anken_Excharge_s)
                {
                    _context.T_Anken_Excharges.Remove(target);
                }

                if (data.T_Anken_ExchargeList != null)
                {
                    foreach (Data.T_Anken_Excharge target in data.T_Anken_ExchargeList)
                    {
                        Data.T_Anken_Excharge t_Anken_Excharge = new();
                        this.CopyProperty(t_Anken_Excharge, target);
                        t_Anken_Excharge.Anken_ID = ankenId;
                        t_Anken_Excharge.Anken_Order = ankenOrder;
                        _context.T_Anken_Excharges.Add(t_Anken_Excharge);
                    }
                }

                // T_Anken_Display  作成
                foreach(Data.T_Anken_Display target in data.T_Anken_DisplayList)
                {
                    Data.T_Anken_Display t_Anken_Display = await _context.T_Anken_Displays.FirstOrDefaultAsync(m => m.Anken_ID == target.Anken_ID && m.Daisuu_Sort == target.Daisuu_Sort && m.Anken_Key == target.Anken_Key);
                    if (t_Anken_Display == null)
                    {
                        t_Anken_Display = new();
                        this.CopyProperty(t_Anken_Display, target);
                        t_Anken_Display.Anken_ID = ankenId;
                        _context.T_Anken_Displays.Add(t_Anken_Display);
                    }
                    else
                    {
                        this.CopyProperty(t_Anken_Display, target, "AnkenDisplay_ID,Anken_ID,Anken_Key,Insert_Datetime,Insert_User");
                    }
                }

                _context.SaveChanges();

                //専属の場合は即配車処理を実施
                if (data.T_Anken.SenzokuID > 0)
                {
                    //傭車データの配車情報更新処理
                    M_Senzoku_Driver driver = await _context.M_Senzoku_Drivers.FirstOrDefaultAsync(m => m.SenzokuID == data.T_Anken.SenzokuID);
                    await RegsterHaisyaToJisya(false, data.T_Anken, data.T_Anken_Detail.Update_User, driver.Driver_ID, driver.DriverSyaryo_ID);
                }
                if (data.T_Anken_Detail.HaisyaPlanKubun == 5 && data.T_Anken_Detail.HaisyaDriverID > 0)
                {
                    await RegsterHaisyaToJisya(false, data.T_Anken, data.T_Anken_Detail.Update_User, data.T_Anken_Detail.HaisyaDriverID, data.T_Anken_Detail.HaisyaDriverSyaryoID);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                throw;
            }
            finally
            {
            }

            return data.T_Anken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flgAddNew"></param>
        /// <param name="anken"></param>
        /// <param name="Update_User"></param>
        /// <returns></returns>
        private async Task RegsterHaisyaToJisya(bool flgAddNew, Data.T_Anken anken, int Update_User, int DriverID, int DriverSyaryoID)
        {
            DateTime dateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            int ankenId = anken.Anken_ID;

            T_Haisya returnHaisya = null;

            M_CompanyDriver_Syaryo syaryo = await _context.M_CompanyDriver_Syaryos.FirstOrDefaultAsync(m => m.DriverSyaryo_ID == DriverSyaryoID);
            List<T_Anken_Display> DisplayList = await _context.T_Anken_Displays.Where(m => m.Anken_ID == ankenId).ToListAsync();
            foreach (var display in DisplayList)
            {
                HaisyaDataModel.HaisyaDataModelDto haisyaDto = new()
                {
                    Haisya = new()
                    {
                        Anken_ID = anken.Anken_ID,
                        Company_ID = anken.Company_ID,
                        AnkenDisplay_ID = display.AnkenDisplay_ID,
                        Day = display.Day,
                        Haisya_Kubun = 1,
                        Driver_ID = DriverID,
                        DriverSyaryo_ID = DriverSyaryoID,
                        SyaryoManagement_ID = syaryo.SyaryoManagement_ID,
                        SyaryoManagement_ID1 = syaryo.SyaryoManagement_ID1,
                        Insert_Datetime = dateTime,
                        Insert_User = Update_User,
                        Update_Datetime = dateTime,
                        Update_User = Update_User,
                    },
                };

                using HaisyaDataModel haisyamodel = new(_context);
                if (flgAddNew)
                {
                    returnHaisya = await haisyamodel.AddNewHaisyaData(haisyaDto, true);
                } else
                {
                    Data.T_Haisya haisya = await _context.T_Haisyas.FirstOrDefaultAsync(m => m.AnkenDisplay_ID == haisyaDto.Haisya.AnkenDisplay_ID);
                    if (haisya != null)
                    {
                        haisyaDto.Haisya.Haisya_ID = haisya.Haisya_ID;
                    }

                    returnHaisya = await haisyamodel.UpdateHaisyaData(haisyaDto, true);
                }
            }
        }

        #region T_Point
        /// <summary>
        /// T_Pointのデータ更新
        /// Point_IDキーでのDelete⇒Inset
        /// </summary>
        /// <param name="t_Point"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdatePointData(Data.T_Point t_Point)
        {
            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            if (t_Point.Address == null)
            {
                resultVal.ErrrMessage = "パラメーターエラー";
                return resultVal;
            }

            try
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    List<Data.T_Point> data = await _context.T_Points.Where(m => m.Address_Code == t_Point.Address_Code).ToListAsync();
                    if (t_Point.User_ID > 0) { data = data.Where(m => m.User_ID == t_Point.User_ID).ToList(); }
                    if (t_Point.Group_ID > 0) { data = data.Where(m => m.Group_ID == t_Point.Group_ID).ToList(); }
                    if (data != null && data.Count > 0)
                    {
                        Data.T_Point t_Point1 = data.FirstOrDefault();
                        CopyProperty(t_Point1, t_Point, "Point_ID");
                        t_Point1.Insert_Datetime = DateTime.Now;
                    } else
                    {
                        Data.T_Point t_Point1 = new();
                        CopyProperty(t_Point1, t_Point);
                        t_Point1.Insert_Datetime = DateTime.Now;
                        await _context.T_Points.AddRangeAsync(t_Point1);
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
        #endregion M_Point

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tsumiDate"></param>
        /// <returns></returns>
        public string GetAnkenNo(DateTime tsumiDate)
        {
            try
            {
                var returnVal = _context.SP_T_Anken_Nos.FromSqlRaw("EXECUTE [dbo].[SP_T_Anken_No] " +
                                        "@FROM_DATETIME = {0}, @ZERO_UME = {1}", DateTime.Parse(tsumiDate.ToString("yyyy/MM/dd")), 6).AsEnumerable();

                var resultData = returnVal.FirstOrDefault();

                string returnData = null;

                if (resultData.Result == null)
                {
                    throw new Exception("SP_T_Anken_Noエラー");
                }
                else
                {
                    returnData = tsumiDate.AddMonths(-3).Year.ToString() + "-" + resultData.Result;
                }

                return returnData;

            } catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ankenNo"></param>
        /// <param name="ankenStatus"></param>
        /// <returns></returns>
        public int GetAnkenIDToAddNew(T_Anken t_Anken)
        {
            try
            {
                var returnVal = _context.SP_T_Ankens.FromSqlRaw("EXECUTE [dbo].[SP_T_Anken] " +
                                        "@KUBUN = {0}, @ANKEN_NO = {1}, @ANKEN_STATUS = {2}, @ANKEN_ORDER = {3}, @COMPANY_ID = {4}, @BRANCH_ID = {5}",
                                        1, t_Anken.Anken_No, t_Anken.Anken_Status, t_Anken.Anken_Latest_Order, t_Anken.Company_ID, t_Anken.Branch_ID).AsEnumerable();

                var resultData = returnVal.FirstOrDefault();

                if (resultData.Result == null)
                {
                    throw new Exception("SP_T_Ankenエラー");
                }
                return (int)resultData.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ankenId"></param>
        /// <returns></returns>
        public int UpdateAnkenOrder(int ankenId)
        {

            var returnVal = _context.SP_T_Ankens.FromSqlRaw(@"EXECUTE [dbo].[SP_T_Anken] " +
                                            "@KUBUN = {0}, @ANKEN_ID = {1}", 2, ankenId).AsNoTracking().AsEnumerable();

            var resultData = returnVal.FirstOrDefault();

            if (resultData.Result == null)
            {
                throw new Exception("SP_T_Ankenエラー");
            }
            return (int)resultData.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ankenId"></param>
        /// <param name="ankenStatus"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAnkenStatus(string ankenId, int ankenStatus)
        {
            SP_ResultForInt resultData = await _context.SP_T_Ankens.FromSqlRaw("EXECUTE [dbo].[SP_T_Anken] " +
                                        "@KUBUN = {0}, @ANKEN_ID = {1}, @ANKEN_STATUS = {2}", 3, ankenId, ankenStatus).AsNoTracking().FirstOrDefaultAsync();
            return resultData.Result == 1;
        }

        /// <summary>
        /// Proc_V_AnkenDataListからデータを返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="CcompanyID"></param>
        /// <param name="branchID"></param>
        /// <param name="TakeNum"></param>
        /// <param name="RirekiKubun"></param>
        /// <param name="AnkenID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<V_AnkenDataList>> GetAnkenDataList(int CcompanyID, int CustomerID, int branchID, string targetDate, 
                                    string targetDateFrom, string targetDateTo, int SenzokuID = 0, int TakeNum = 0, int RirekiKubun = 0,
                                    int AnkenID = 0)
        {
            string sql = string.Format("EXECUTE [dbo].[Proc_V_AnkenDataList] @COMPANY_ID = {0}", CcompanyID);
            if (targetDate != null && (targetDateFrom == null || targetDateTo == null || (targetDate == targetDateFrom && targetDate == targetDateTo)))
            {
                sql += string.Format(", @TARGET_DATE='{0}'", targetDate.Replace("-", "/"));
            }
            if (targetDateFrom != null)
            {
                sql += string.Format(", @TARGET_DATE_FROM = '{0}'", targetDateFrom.Replace("-", "/"));
            }
            if (targetDateTo != null)
            {
                sql += string.Format(", @TARGET_DATE_TO = '{0}'", targetDateTo.Replace("-", "/"));
            }
            if (SenzokuID > 0) { sql += string.Format(", @SENZOKU_ID = {0}", SenzokuID); }
            if (CustomerID > 0) { sql += string.Format(", @CUSTOMER_ID = {0}", CustomerID); }
            if (branchID > 0) { sql += string.Format(", @BRANCH_ID = {0}", branchID); }
            sql += string.Format(", @RIREKI_KUBUN = {0}", RirekiKubun);
            sql += string.Format(", @ANKEN_ID = {0}", AnkenID);

            IEnumerable<V_AnkenDataList> resultData = await _context.V_AnkenDataLists.FromSqlRaw(sql).AsNoTracking().ToListAsync();

            if (resultData == null) return null;
            if (TakeNum > 0) resultData = resultData.Take(TakeNum);

            return resultData;
        }
    }

    public class AnkenDataModelDto
    {

        public Data.T_Anken T_Anken { set; get; }

        public Data.T_Anken_Detail T_Anken_Detail { set; get; }

        public Data.T_Anken_Publish T_Anken_Publish { set; get; }

        public Data.T_Anken_Remark T_Anken_Remarks { set; get; }

        public List<Data.T_Anken_Luggage> T_Anken_LuggageList { set; get; }

        public List<Data.T_Anken_OyaKokyaku> T_Anken_OyaKokyakuList { set; get; }

        public List<Data.T_Anken_Point> T_Anken_PointList { set; get; }

        public List<Data.T_Anken_Excharge> T_Anken_ExchargeList { set; get; }

        public List<Data.T_Anken_Equipment> T_Anken_EquipmentList { set; get; }

        public Data.T_Anken_Riyounso T_Anken_Riyounso { set; get; }

        public List<Data.T_Anken_Riyounso_Point> T_Anken_Riyounso_PointList { set; get; }

        public List<T_Anken_Display> T_Anken_DisplayList { set; get; }

        public V_LoginUser V_LoginUser { set; get; }

        public M_Customer_Tantou M_Customer_Tantou { set; get; }

        [StringLength(255)]
        public string AnkenRemarks { get; set; }

    }

    public class PointDto
    {

        public int PointId { get; set; }

        public int Kubun { get; set; }

        public int Point_Order { get; set; }

        public string TitleDisplay { get; set; }

        public string Address { get; set; }

        public string Address_Code { get; set; }

        public string Address_Level { get; set; }

        public string Lng { get; set; }

        public string Lat { get; set; }

        public string BuildingName { get; set; }

        public string BuildingZid { get; set; }

        public string BuildingZid_Attr { get; set; }

        public string BuildingNameRead { get; set; }

        public string Post_code { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }


        /// <summary>画面表示タイトル</summary>
        public string Title { get; set; }

        public string Type { get; set; }

        public bool PointAddNewBtnEnable { get; set; }

        public bool PointDelBtnEnable { get; set; }


        //////////////////ポイントマスタの登録情報を設定////////////////////
        /// <summary>ポイント名称</summary>
        public string PointName { get; set; }


        /// <summary>ポイント日時</summary>
        public DateTime? PointDate { get; set; }
        /// <summary>ポイント日時</summary>
        public string PointTime { get; set; }
        /// <summary>ポイント日時区分（頃/まで/丁度/）</summary>
        public int? PointTimeKubun { get; set; }
        /// <summary>ポイント暫定確定区分</summary>
        public int? PointStatusKubun { get; set; }
        /// <summary>ポイント道路タイプ区分</summary>
        public string PointRoadTypeKubun { get; set; }
        /// <summary>ポイントタイプ区分</summary>
        public int? PointPointTypeKubun { get; set; }

        public string TollDisplay { get; set; }

        public double? TollDisplayHeight { get; set; }

        public bool? FlgGenchiKakunin { get; set; }

        /// <summary>スイッチポイントフラグ</summary>
        [Display(Name = "スイッチポイント")]
        public bool Switch_Flg { get; set; }

        /// <summary>積替えポイントフラグ</summary>
        [Display(Name = "積替えポイント")]
        public bool ReShip_Flg { get; set; }

    }
}
