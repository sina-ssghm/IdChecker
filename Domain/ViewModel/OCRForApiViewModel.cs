
using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.ViewModel
{
    public class Result
    {
        //public OCRForApiViewModel OCR { get; set; }
        public Elements Elements { get; set; }
        public Data Classification { get; set; }
        public List<OCRFileForApiViewModel> OCRFile { get; set; }
    }
    public class Element { public string Title { get; set; } public string Value { get; set; } public string Key { get; set; } = ""; public string Col { get; set; } = ""; public string Priority { get; set; } = ""; public string Faicon { get; set; } = ""; public string Group_Key { get; set; } = ""; public string Element_Type_Code { get; set; } = ""; public string Element_Type_Text { get; set; } = ""; public string Related_Key { get; set; } = ""; public string Size { get; set; } = ""; public string Callback_URL { get; set; } = ""; public string Executed { get; set; } = ""; public string Execute_Date { get; set; } = ""; public string Execute_Time { get; set; } = ""; }


    public class Element_List
    {
        public Element Element_1 { get; set; }
        public Element Element_2 { get; set; }
        public Element Element_3 { get; set; }
        public Element Element_4 { get; set; }
        public Element Element_5 { get; set; }
        public Element Element_6 { get; set; }
        public Element Element_7 { get; set; }
        public Element Element_8 { get; set; }
        public Element Element_9 { get; set; }
    }

    public class Elements
    {
        public string Count { get; set; } = "9";
        public Element_List List { get; set; }

    }


    public class OCRForApiViewModel
    {

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string BirthDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Address { get; set; }
        public string TransactionId { get; set; }

    }
    public class ResultOCRAndFilesViewModel
    {
        public Result Result { get; set; }
        public Transaction Transaction { get; set; }
        public Message Message { get; set; }


    }
    public class OCRFileForApiViewModel
    {
        public string Image_ID { get; set; }
        public string File_Name { get; set; }

    }
    public class Transaction
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Transaction_Id { get; set; }
    }

    public class Message
    {
        public bool Error { get; set; }
        public string Description { get; set; }
    }

}
