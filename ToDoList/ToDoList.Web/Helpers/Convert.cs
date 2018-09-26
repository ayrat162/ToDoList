using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ToDoList.Models.DTO;
using ToDoList.Web.Models;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Helpers
{
    public static class Convert
    {
        public static byte[] File2Picture(HttpPostedFileBase uploadImage)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
            return imageData;
        }
    }
}