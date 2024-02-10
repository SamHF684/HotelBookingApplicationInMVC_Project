using DB_con;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Numerics;
using System.Security.Policy;
using System.Xml.Linq;

namespace Hotels_Booking.Models
{
    public class Online_Booking : IDisposable
    {

        #region Properites
        public int C_Id { get; set; }
        public string C_Name { get; set; }

        public string H_Name { get; set; }
        public int H_Id { get; set; }
        public string C_Add1 { get; set; }
        public string C_Add2 { get; set; }
        public string C_Email { get; set; }
        public string C_Mobile { get; set; }
        public DateTime C_DoB { get; set; }
        public string C_Document { get; set; }
        public string C_Gender { get; set; }
        public bool ISActive { get; set; }
        #endregion

        #region "constructors"

        ConnectionClass obj_con = null;
        public Online_Booking()
        {
            obj_con = new ConnectionClass();
        }
        #endregion
        #region Method

        public List<Online_Booking> List()
        {
            try
            {
                obj_con.clearParameter();
                DataTable dt = obj_con.ExecuteReaderTable("Sp_GetCustomerList", CommandType.StoredProcedure);
                return ConvertToList(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Online_Booking> ConvertToList(DataTable dt)
        {
            try
            {
                List<Online_Booking> obj_List = new List<Online_Booking>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Online_Booking obj = new Online_Booking();
                    obj.C_Name = Convert.ToString(dt.Rows[i]["C_Name"]);
                    obj.C_Id = Convert.ToInt32(dt.Rows[i]["C_Id"]);
                    obj.H_Id = Convert.ToInt16(dt.Rows[i]["H_Id"]);
                    obj.C_Add1 = Convert.ToString(dt.Rows[i]["C_Add1"]);
                    obj.C_Add2 = Convert.ToString(dt.Rows[i]["C_Add2"]);
                    obj.C_Email = Convert.ToString(dt.Rows[i]["C_Email"]);
                    obj.C_Mobile = Convert.ToString(dt.Rows[i]["C_Mobile"]);
                    obj.C_DoB = Convert.ToDateTime(dt.Rows[i]["C_DoB"]);
                    obj.C_Document = Convert.ToString(dt.Rows[i]["C_Document"]);
                    obj.C_Gender = Convert.ToString(dt.Rows[i]["C_Gender"]);
                    obj.ISActive = Convert.ToBoolean(dt.Rows[i]["ISActive"]);
                    obj_List.Add(obj);
                }
                return obj_List;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        void IDisposable.Dispose()
        {
            System.GC.SuppressFinalize(this);

        }
        public void Insert(Online_Booking Book)

        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@C_Name", Book.C_Name);
                obj_con.addParameter("@H_Id", Book.H_Id);
                obj_con.addParameter("@C_Add1", Book.C_Add1);
                obj_con.addParameter("@C_Add2", Book.C_Add2);
                obj_con.addParameter("@C_Email", Book.C_Email);
                obj_con.addParameter("@C_Mobile", Book.C_Mobile);
                obj_con.addParameter("@C_DoB", Book.C_DoB);
                obj_con.addParameter("@C_Document", Book.C_Document);
                obj_con.addParameter("@C_Gender", Book.C_Gender);
                obj_con.addParameter("@ISActive", Book.ISActive);
                obj_con.ExecuteNoneQuery("Sp_InsertCustomerDetail", System.Data.CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Online_Booking SelectDataByID(Int32? C_Id)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@C_Id", C_Id);
                DataTable dt = obj_con.ExecuteReaderTable("Sp_SelectCustomerDataById", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return ConvertToOjbect(dt);


            }
            catch (Exception ex)
            {
                throw new Exception("Sp_SelectCustomerDataById:" + ex.Message);
            }
        }

        public Online_Booking ConvertToOjbect(DataTable dt)
        {
            Online_Booking obj_Customer = new Online_Booking();



            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (Convert.ToString(dt.Rows[i]["C_Name"]) != "")
                {
                    obj_Customer.C_Name = Convert.ToString(dt.Rows[i]["C_Name"]);
                }

                if (Convert.ToString(dt.Rows[i]["H_Name"]) != "")
                {

                    obj_Customer.H_Name = Convert.ToString(dt.Rows[i]["H_Name"]);
                }
                if (Convert.ToString(dt.Rows[i]["H_Id"]) != "")
                {

                    obj_Customer.H_Id = Convert.ToInt32(dt.Rows[i]["H_Id"]);
                }

                if (Convert.ToString(dt.Rows[i]["C_Add1"]) != "")
                {

                    obj_Customer.C_Add1 = Convert.ToString(dt.Rows[i]["C_Add1"]);
                }
                if (Convert.ToString(dt.Rows[i]["C_Add2"]) != "")
                {

                    obj_Customer.C_Add2 = Convert.ToString(dt.Rows[i]["C_Add2"]);
                }
                if (Convert.ToString(dt.Rows[i]["C_Email"]) != "")
                {

                    obj_Customer.C_Email = Convert.ToString(dt.Rows[i]["C_Email"]);
                }
                if (Convert.ToString(dt.Rows[i]["C_Mobile"]) != "")
                {

                    obj_Customer.C_Mobile = Convert.ToString(dt.Rows[i]["C_Mobile"]);
                }
                if (Convert.ToString(dt.Rows[i]["C_DoB"]) != "")
                {

                    obj_Customer.C_DoB = Convert.ToDateTime(dt.Rows[i]["C_DoB"]);
                }
                if (Convert.ToString(dt.Rows[i]["C_Document"]) != "")
                {

                    obj_Customer.C_Document = Convert.ToString(dt.Rows[i]["C_Document"]);
                }
                if (Convert.ToString(dt.Rows[i]["C_Gender"]) != "")
                {

                    obj_Customer.C_Gender = Convert.ToString(dt.Rows[i]["C_Gender"]);
                }
                if (Convert.ToString(dt.Rows[i]["ISActive"]) != "")
                {

                    obj_Customer.ISActive = Convert.ToBoolean(dt.Rows[i]["ISActive"]);
                }

            }
            return obj_Customer;
        }


        public int Update(Online_Booking B)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@C_Id", B.C_Id);
                obj_con.addParameter("@C_Name", B.C_Name);
                obj_con.addParameter("@H_Id", B.H_Id);
                obj_con.addParameter("@C_Add1", B.C_Add1);
                obj_con.addParameter("@C_Add2", B.C_Add2);
                obj_con.addParameter("@C_Email", B.C_Email);
                obj_con.addParameter("@C_Mobile", B.C_Mobile);
                obj_con.addParameter("@C_DoB", B.C_DoB);
                obj_con.addParameter("@C_Document", B.C_Document);
                obj_con.addParameter("@C_Gender", B.C_Gender);
                obj_con.addParameter("@ISActive", B.ISActive);
                obj_con.BeginTransaction();
                obj_con.ExecuteNoneQuery("Sp_UpdateHotelCustomerDetails", System.Data.CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                return B.C_Id = Convert.ToInt32(obj_con.getValue("@C_Id"));
            }


            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_UpdateHotelCustomerDetails:" + ex.Message);

            }

        }

        public void Delete(Int32 C_Id)

        {
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@C_Id", C_Id);
                obj_con.ExecuteNoneQuery("Sp_DeleteHotelCustomerDataByID ", System.Data.CommandType.StoredProcedure);
                obj_con.CommitTransaction();
            }

            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_DeleteHotelCustomerDataByID :" + ex.Message);
            }

        }

        //public DataSet GetHotelName()
        //{
        //    try
        //    {
        //        obj_con.clearParameter();

        //        DataTable dt = (DataTable)obj_con.ExecuteReader("Sp_SelectAllHotel_Name", CommandType.StoredProcedure);
        //        obj_con.CommitTransaction();
        //        obj_con.closeConnection();
        //        DataSet ds = new DataSet();
        //        ds.Tables.Add(dt);
        //        foreach (DataTable table in ds.Tables)
        //        {
        //            foreach (DataRow dr in table.Rows)
        //            {
        //                var H_Name = dr["H_Name"].ToString();
        //            }
        //        }

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Sp_SelectAllHotel_Name:" + ex.Message);
        //    }
        //}
        public List<SelectListItem> GetHotelName()
        {
            try
            {
                obj_con.clearParameter();
                DataTable dt = obj_con.ExecuteReaderTable("Sp_SelectAllHotels_Name", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                List<SelectListItem> lst = new List<SelectListItem>();
                SelectListItem sli1 = new SelectListItem();
                //sli1.Text = "H_Name";
                sli1.Value = "H_Id";
                lst.Add(sli1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Text = Convert.ToString(dt.Rows[i][1]);
                    sli.Value = Convert.ToString(dt.Rows[i][0]);
                    lst.Add(sli);
                }
                return lst;
            }

            catch (Exception ex)
            {
                throw new Exception("Sp_SelectAllHotels_Name:" + ex.Message);
            }
        }




    }

}
