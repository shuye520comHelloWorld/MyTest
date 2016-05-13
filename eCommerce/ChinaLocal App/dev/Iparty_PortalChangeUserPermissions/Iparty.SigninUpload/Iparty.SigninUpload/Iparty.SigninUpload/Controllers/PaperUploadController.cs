using Iparty.SigninUpload.Common;
using Iparty.SigninUpload.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iparty.SigninUpload.Controllers
{
    [UserAuth]
    public class PaperUploadController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            ViewBag.User = System.Web.HttpContext.Current.User.Identity.Name;
            return View();
        }

        public ActionResult ChoseParty(string DirId)
        {
            List<DirInfo> Dirs = PartyDAO.checkDir(DirId);

            return View(Dirs);
        }

        public ActionResult uploadView(Guid PartyKey)
        {
            List<PartyInfo> Partys = PartyDAO.getParty(PartyKey);
            List<Customer> cus = PartyDAO.getInputCustomers(PartyKey);
            ViewBag.Customers = cus;
            if (Partys.Count > 0)
            {
                return View(Partys[0]);
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetParty(string DirId)
        {

            List<DirInfo> Dirs = PartyDAO.checkDir(DirId);
            if (Dirs.Count < 1)
            {
                return Json(new { result = false, errMsg = "无法关联到相关活动，请检查后重试！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = true, name = Dirs[0].Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult addNewTime(Guid PartyKey, string StartTime, string EndTime)
        {
            DateTime start = DateTime.Parse(StartTime);
            DateTime end = DateTime.Parse(EndTime);

            if (PartyDAO.addActualTime(PartyKey, start, end) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult addOneUser(Guid PartyKey, string Name, string Phone, string InviterId)
        {

            List<Customer> c = PartyDAO.getCustomer(PartyKey, Phone);
            if (c.Count > 0)
            {
                #region 线上报名顾客
                Customer cu = c[0];

                if (cu.Status == "04" && cu.CheckInType == "06")
                {
                    return Json(new { result = false, status = "paperChecked", msg = ConfigurationManager.AppSettings["paperChecked"] }, JsonRequestBehavior.AllowGet);
                }

                if (cu.Status == "01" || cu.Status == "04")
                {
                    //已到场/现场报名
                    return Json(new { result = false, status = "checked", msg = ConfigurationManager.AppSettings["checked"] }, JsonRequestBehavior.AllowGet);
                }

                //未到场姓名相同
                if (cu.Status == "00" && cu.Name.Trim() == Name.Trim() && cu.DirectSellerID == InviterId.Trim()) //未到场姓名相符
                {
                    CacheHelper.Insert<bool>(PartyKey + Phone, true, 5);
                    return Json(new { result = true, status = "invited", name = Name, phone = Phone, inviter = cu.DirectSellerID });
                }
                else
                {
                    CacheHelper.Insert<bool>(PartyKey + Phone, true, 5);
                    return Json(new { result = true, status = "confirm", name = cu.Name, sellerid = cu.DirectSellerID });
                }
                #endregion
            }

            else
            {
                List<partyUnitee> pu = PartyDAO.getPartyUnite(PartyKey);
                if (pu.Count > 0)
                {
                    #region 邀约人不存在或 邀约人不属于本沙龙
                    if (!string.IsNullOrEmpty(InviterId))
                    {
                        if (InviterId.Trim().Length != 12)
                        {
                            return Json(new { result = false, status = "notunit", msg = ConfigurationManager.AppSettings["notunit"] });
                        }

                        List<Consultant> rec = PartyDAO.getinviter(InviterId);
                        if (rec.Count > 0)
                        {

                            if (pu.FindAll(e => e.ParUnitId == rec[0].UnitID || e.UnitID == rec[0].UnitID).Count > 0)
                            {
                                if (rec[0].Level < 15)
                                {
                                    return Json(new { result = false, status = "notunit", msg = ConfigurationManager.AppSettings["notunit"] });
                                }
                            }
                            else
                            {
                                return Json(new { result = false, status = "notunit", msg = ConfigurationManager.AppSettings["notunit"] });
                            }
                        }
                        else
                        {
                            return Json(new { result = false, status = "notunit", msg = ConfigurationManager.AppSettings["notunit"] });
                        }
                    }
                    #endregion

                    List<Consultant> lc = PartyDAO.getConsultant(Phone);
                    if (lc.Count > 0)
                    {
                        //30级及以上BC  拒绝
                        if (lc[0].Level >= 30)
                        {
                            return Json(new { result = false, status = "up30", msg = ConfigurationManager.AppSettings["up30"] });
                        }

                        // VIP、BC但不属于本场活动沙龙  拒绝
                        if (pu.FindAll(e => e.ParUnitId == lc[0].UnitID || e.UnitID == lc[0].UnitID).Count < 1)
                        {
                            return Json(new { result = false, status = "notmyunit", msg = ConfigurationManager.AppSettings["notmyunit"] });
                        }
                        else  //是自己活动场的VIP/BC 弹窗提示
                        {
                            CacheHelper.Insert<bool>(PartyKey + Phone, true, 5);

                            if (Name.Trim() == lc[0].Name.Trim() && Phone == lc[0].PhoneNumber && InviterId == lc[0].RecruiterOrDirId)
                            {
                                return Json(new { result = true, status = "myVIPSame", name = lc[0].Name, phone = Phone, inviter = lc[0].RecruiterOrDirId });
                            }
                            else
                            {
                                return Json(new { result = true, status = "myVIP", name = lc[0].Name, phone = Phone, inviter = lc[0].RecruiterOrDirId });
                            }
                        }
                    }
                }

            }

            CacheHelper.Insert<bool>(PartyKey + Phone, true, 5);
            return Json(new { result = true, status = "recheck", name = Name, phone = Phone, inviter = InviterId });
        }

        [HttpPost]
        public JsonResult addCustomer(Guid PartyKey, string Name, string Phone, string InviterId)
        {
            if (CacheHelper.Get<bool>(PartyKey + Phone))
            {
                int res = PartyDAO.addCustomer(PartyKey, Name, Phone, InviterId, System.Web.HttpContext.Current.User.Identity.Name);
                if (res == 1)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (res == -2)
                    {
                        return Json(new { result = false, msg = "party活动不存在，请选择正确的活动场次！" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (res == -3)
                    {
                        return Json(new { result = false, msg = "该顾客已存在，请填写下一位来宾信息！" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                Json(new { result = false, msg = "您还没有进行录入校验，请按照正确步骤进行录入来宾信息！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult updateCustomer(Guid PartyKey, string Phone)
        {
            if (CacheHelper.Get<bool>(PartyKey + Phone))
            {
                int res = PartyDAO.updateCustomer(PartyKey, Phone);
                if (res == 1)
                {
                    List<Customer> cu = PartyDAO.getCustomer(PartyKey, Phone);
                    return Json(new { result = true, name = cu[0].Name, sellerid = cu[0].DirectSellerID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (res == -2)
                    {
                        return Json(new { result = false, msg = "输入用户手机号不正确或party活动不存在，请输入正确的内容！" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (res == -3)
                    {
                        return Json(new { result = false, msg = "该用户已签到过，请填写下一位来宾信息！" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                Json(new { result = false, msg = "您还没有进行录入校验，请按照正确步骤进行录入来宾信息！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }


        public ActionResult test()
        {
            return View();
        }



    }
}
