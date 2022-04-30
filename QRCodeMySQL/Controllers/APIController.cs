using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCodeMySQL.Models;

namespace QRCodeMySQL.Controllers
{
    public class APIController : Controller
    {
        /**
         * 將接收到的QR code進行儲存
         * storeQRCode         
         * string data
         */
        [HttpPost]
        public ActionResult storeQRCode(string data)
        {

            MySQLModel mySQLModel = new MySQLModel();
            mySQLModel.setQRCodeData(data);

            var jsonData = new { Status = "Success" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /**
         * 將所有儲存的QR code做成HTML表格進行回傳
         */
        [HttpGet]
        public ActionResult getQRCode()
        {
            MySQLModel mySQLModel = new MySQLModel();
            List<QRCodeModel>  qRCodeModelList = mySQLModel.getQRCodeData();
            HTMLRenderModel htmlRender=new HTMLRenderModel();
            string returnResult=htmlRender.renderTableRaw(qRCodeModelList);
            return Content(returnResult);
        }
    }
}