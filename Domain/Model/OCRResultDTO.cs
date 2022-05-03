using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.Model
{

    public class FinalResultOCRDTO
    {
        public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        public string LastName{ get; set; }
        public string FullName{ get; set; }
        public string DocumentNumber { get; set; }
        public string BirthDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Address { get; set; }

        public string FrontUrl { get; set; }
        public string BackUrl { get; set; }
        public string FaceUrl { get; set; }
        public string SignatureUrl { get; set; }
        public string JsonResultIDv { get; set; }
        public string TransactionId { get; set; }
        public string ImageFrontId { get; set; }
        public string ImageBackId { get; set; }
        public string ImageFaseId { get; set; }
        public string ImageSignatureId { get; set; }
        public string Classification { get; set; }
        public string ApplicationId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
    public class OCRDTO
    {
        public string FilePath { get; set; }
        public Result Result { get; set; }
    }
    public class Transaction
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int Status_Code { get; set; }
        public string Status_Result { get; set; }
        public string Username { get; set; }
        public string Date_Format { get; set; }
        public string Request_Date { get; set; }
        public string Request_Time { get; set; }
        public string Request_IP { get; set; }
        public int Attached_File { get; set; }
        public bool Async { get; set; }
        public string Callback_URL { get; set; }
    }

    public class Data
    {
        public string Country_Code { get; set; }
        public string Country_Name { get; set; }
        public string State_Code { get; set; }
        public string State_Name { get; set; }
        public string Licence_Type_Code { get; set; }
        public string Licence_Type_Name { get; set; }
        public string Template_Name { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Given_Name { get; set; }
        public string Surname { get; set; }
        public string Full_Name { get; set; }
        public string Document_Number { get; set; }
        public string Birth_Date { get; set; }
        public string Expiry_Date { get; set; }
        public string Address { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string Address_State { get; set; }
        public string Address_City { get; set; }
        public string Address_Postal_Code { get; set; }
        public string Condition_Code { get; set; }
        public string Condition_Description { get; set; }
        public string Class_Code { get; set; }
        public string Class_Description { get; set; }
        public string Address_Street { get; set; }
        public string Auto_Address { get; set; }
        public string Card_Number { get; set; }
        public string Unit_Number { get; set; }
    }

    public class Upload
    {
        public string Front_image { get; set; }
        public string Back_image { get; set; }
    }

    public class Processed
    {
        public string Front_image { get; set; }
        public string Back_image { get; set; }
        public string Face { get; set; }
        public string Signature { get; set; }
    }

    public class Files
    {
        public Upload Upload { get; set; }
        public Processed Processed { get; set; }
    }

    public class Result
    {
        public Transaction Transaction { get; set; }
        public Data Data { get; set; }
        public Files Files { get; set; }
    }

    public class Message
    {
        public object Transaction_ID { get; set; }
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Description { get; set; }
    }

    public class Root
    {
        public Result Result { get; set; }
        public Message Message { get; set; }
    }
}
