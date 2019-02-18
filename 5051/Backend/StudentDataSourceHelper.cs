using System;
using System.Collections.Generic;

using _5051.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace _5051.Backend
{
    /// <summary>
    /// Backend Table DataSource for Students, to manage them
    /// </summary>
    public class StudentDataSourceHelper
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile StudentDataSourceHelper instance;
        private static readonly object syncRoot = new Object();

        private StudentDataSourceHelper() { }

        public static StudentDataSourceHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new StudentDataSourceHelper();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Student List
        /// </summary>
        private List<StudentModel> DataList = new List<StudentModel>();

        /// <summary>
        /// Clear the Data List, and build up a new one
        /// </summary>
        /// <returns></returns>
        public List<StudentModel> GetDefaultDataSet()
        {
            DataList.Clear();

            StudentModel data;

            // Mike has Full Truck and Tokens
            data = new StudentModel("Mike");
            data.Tokens = 1000;
            data.Password = data.Name;
            DataList.Add(data);

            // Doug has full truck, but no Tokens
            data = new StudentModel("Doug");
            data.Tokens = 0;
            data.Password = data.Name;
            DataList.Add(data);

            // Jea has default Empty truck, and Tokens
            data = new StudentModel("Jea");
            data.Tokens = 100;
            data.Password = data.Name;
            DataList.Add(data);

            // Jea has default Empty truck, and No Tokens
            data = new StudentModel("Sue");
            data.Tokens = 0;
            data.Password = data.Name;
            DataList.Add(data);

            // Stan has Full Truck and 1 Token
            data = new StudentModel("Stan");
            data.Tokens = 1;
            data.Password = data.Name;
            DataList.Add(data);

            return DataList;
        }

    }
}