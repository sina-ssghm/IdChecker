using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel.CreateApplicationViewModel
{
  
    public class RequestData
    {
        public string Application_Title { get; set; }
        public string Date_Format { get; set; }
        public string Expiration { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_Address { get; set; }
        public string Phone_Number { get; set; }
        public bool Send_Email { get; set; }
        public bool Send_SMS { get; set; }
        public bool Create_View { get; set; }
        public string View_Password { get; set; }
        public bool Create_PDF { get; set; }
        public string PDF_Password { get; set; }
    }

    public class Expiration
    {
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public class Result
    {
        public string Application_ID { get; set; }
        public int Application_Status_Code { get; set; }
        public string Application_Status_Text { get; set; }
        public string API_Code { get; set; }
        public string API_Name { get; set; }
        public string Request_Date { get; set; }
        public string Request_Time { get; set; }
        public string Request_IP_Address { get; set; }
        public string Request_Username { get; set; }
        public RequestData Request_Data { get; set; }
        public Expiration Expiration { get; set; }
        public string Reference_ID { get; set; }
        public string Branch_ID { get; set; }
        public string Personnel_Group_ID { get; set; }
        public string Personnel_ID { get; set; }
        public string Ext_Field_01 { get; set; }
        public string Ext_Field_02 { get; set; }
        public string Ext_Field_03 { get; set; }
        public string Ext_Field_04 { get; set; }
        public string Ext_Field_05 { get; set; }
        public string Ext_Field_06 { get; set; }
        public string Ext_Field_07 { get; set; }
        public string Ext_Field_08 { get; set; }
        public string Ext_Field_09 { get; set; }
        public string Ext_Field_10 { get; set; }
    }

    public class Message
    {
        public string Log_Transaction_ID { get; set; }
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Description { get; set; }
    }

    public class CreateApplicationViewModel
    {
        public Result Result { get; set; }
        public Message Message { get; set; }
    }
}
