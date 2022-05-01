using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace QRCodeMySQL.Models
{
    public class MySQLModel
    {
        public string connString;
        public MySqlConnection conn;

        public MySQLModel()
        {
            connString = "server=127.0.0.1;port=3306;user id=root;password=root;database=mvcdb;charset=utf8;";
            conn = new MySqlConnection();
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
            {
                testDatabase();
                conn.Open();
            }

        }

        //測試資料庫
        public void testDatabase()
        {
            //如果找不到目標資料表，則重建資料表
            try
            {
                getQRCodeData();
            }
            catch (Exception ex)
            {
                conn.Close();
                buildDatabase();
            }
        }

        //重建資料庫
        public void buildDatabase()
        {
            connString = "server=127.0.0.1;port=3306;user id=root;password=root;charset=utf8;";
            conn = new MySqlConnection();
            conn.ConnectionString = connString;
            conn.Open();
            string sql = @"CREATE DATABASE IF NOT EXISTS `mvcdb`;USE `mvcdb`;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            sql = @"
                CREATE TABLE IF NOT EXISTS `qr-code` (
                  `id` int(11) NOT NULL AUTO_INCREMENT,
                  `qr-code-string` varchar(50) DEFAULT NULL,
                  PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
            ";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //插入QR coede
        public void setQRCodeData(string QRCodeString)
        {
            string sql = @"INSERT INTO `qr-code` (`qr-code-string`) VALUES (@QRCodeString)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@QRCodeString", MySqlDbType.VarChar);
            cmd.Parameters["@QRCodeString"].Value = QRCodeString;

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        //取得QR coede
        public List<QRCodeModel> getQRCodeData()
        {
            string sql = @"SELECT * FROM `qr-code` ";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            DataTable dataTable = new DataTable();
            MySqlDataReader sdr = cmd.ExecuteReader();

            List<QRCodeModel> qRCodeModelList = new List<QRCodeModel>();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    QRCodeModel qrCodeModel = new QRCodeModel();
                    qrCodeModel.id = sdr["id"].ToString();
                    qrCodeModel.qrCodeString = sdr["qr-code-string"].ToString();
                    qRCodeModelList.Add(qrCodeModel);
                }
            }

            conn.Close();

            return qRCodeModelList;
        }

    }
}