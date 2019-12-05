using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeautyBusiness.Models;
using System.Data;
using System.Data.SqlClient;

namespace BeautyBusiness.DataAccess
{
    public class AlbumService:Base
    {
       
        public int UploadAlbums(AlbumMaster objAlbumMaster)
        {
            SqlConnection con = new SqlConnection(conn);
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = con;
                scmd.CommandType = CommandType.Text;
                 scmd.CommandText = "INSERT INTO BeautyGallery(ImageName,Photo,userid,SubServiceId) VALUES(@ImageName,@Photo,@userid,@SubServviceID)";
                scmd.Parameters.AddWithValue("@ImageName", objAlbumMaster.ImageName);
                scmd.Parameters.AddWithValue("@Photo", objAlbumMaster.Image);
                scmd.Parameters.AddWithValue("@userid", objAlbumMaster.Userid);
                scmd.Parameters.AddWithValue("@SubServviceID", objAlbumMaster.SubServiceID);
                con.Open();
                int status = scmd.ExecuteNonQuery();
                con.Close();
                return status;
            }
        }
        public List<GallaryView> GetGallaryViewList()
        {
            SqlConnection scon = new SqlConnection(conn);
            List<GallaryView> objGallaryViewList = new List<GallaryView>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "select ImageId, ImageName, photo , Subservice_category.SubServiceId , Service_category.ServiceId ,ServiceName, SubServiceName "
                   +" from BeautyGallery inner join Subservice_category on beautyGallery.SubServiceId = Subservice_category.SubServiceId "
                   + " inner join Service_category on Service_category.ServiceId = SubService_category.ServiceId";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    GallaryView objGallaryViewMaster = new GallaryView();
                    objGallaryViewMaster.ImageId = Convert.ToInt32(sdr["ImageId"]);
                    objGallaryViewMaster.ImageName = sdr["ImageName"].ToString();
                    objGallaryViewMaster.Image = (byte[])sdr["Photo"];
                    objGallaryViewMaster.ServiceId= Convert.ToInt32(sdr["ServiceId"]);
                    objGallaryViewMaster.SubServiceId= Convert.ToInt32(sdr["SubServiceId"]);
                    objGallaryViewMaster.ServiceName= sdr["ServiceName"].ToString();
                    objGallaryViewMaster.SubServiceName= sdr["SubServiceName"].ToString();
                 
                    objGallaryViewList.Add(objGallaryViewMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objGallaryViewList.ToList(); ;
            }


        }
        public List<AlbumMaster> GetAlbums()
        {
            SqlConnection scon = new SqlConnection(conn);
            List<AlbumMaster> objAlbumList = new List<AlbumMaster>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM BeautyGallery";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    AlbumMaster objAlbumMaster = new AlbumMaster();
                    objAlbumMaster.ImageId = Convert.ToInt32(sdr["ImageId"]);
                    objAlbumMaster.ImageName = sdr["ImageName"].ToString();
                    objAlbumMaster.Image = (byte[])sdr["Photo"];
                    objAlbumMaster.Userid= Convert.ToInt32(sdr["userid"]);
                    objAlbumList.Add(objAlbumMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objAlbumList.ToList(); ;
            }
        }
        public byte[] GetImageFromDataBase(int id)
        {
            SqlConnection scon = new SqlConnection(conn);
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT Photo FROM BeautyGallery where ImageId=@ImageId";
                scmd.Parameters.AddWithValue("@ImageId", id);
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                AlbumMaster objAlbum = new AlbumMaster();
                while (sdr.Read())
                {
                    objAlbum.Image = (byte[])sdr["Photo"];
                }
                return objAlbum.Image;
            }
        }

    }
}