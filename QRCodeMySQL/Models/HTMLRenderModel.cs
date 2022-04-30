using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QRCodeMySQL.Models
{
    public class HTMLRenderModel
    {
        public string renderTableColumn(string id,string qrCodeString)
        {
            return "<td>" + id + "</td>"+ "<td>" + qrCodeString + "</td>";
        }

        public string renderTableRaw(List<QRCodeModel> qrCodeModelList)
        {
            string result = "";
            foreach (QRCodeModel qrCodeModel in qrCodeModelList)
            {
                result+="<tr>"+this.renderTableColumn(qrCodeModel.id,qrCodeModel.qrCodeString)+ "</tr>";
            }
            return result;
        }
    }
}