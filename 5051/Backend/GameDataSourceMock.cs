using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _5051.Models;
namespace _5051.Backend
{
    /// <summary>
    /// Backend Mock DataSource for Games, to manage them
    /// </summary>
    public class GameDataSourceMock : IGameInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile GameDataSourceMock instance;
        private static readonly object syncRoot = new Object();

        private GameDataSourceMock() { }

        public static GameDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new GameDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Game List
        /// </summary>
        private List<GameModel> GameList = new List<GameModel>();

        /// <summary>
        /// Makes a new Game
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Game Passed In</returns>
        public GameModel Create(GameModel data)
        {
            GameList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public GameModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = GameList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public GameModel Update(GameModel data)
        {
            //Might be an impossible null check
            if (data == null)
            {
                return null;
            }
            var myReturn = GameList.Find(n => n.Id == data.Id);

            if (myReturn == null)
            {
                return null;
            }
            myReturn.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = GameList.Find(n => n.Id == Id);
            var myReturn = GameList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Games</returns>
        public List<GameModel> Index()
        {
            return GameList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            LoadDataSet(DataSourceDataSetEnum.Default);
        }

        /// <summary>
        /// Clears the Data
        /// </summary>
        private void DataSetClear()
        {
            GameList.Clear();
        }

        /// <summary>
        /// The Defalt Data Set
        /// </summary>
        private void DataSetDefault()
        {
            DataSetClear();
            var temp = new GameModel
            {
                RunDate = DateTimeHelper.Instance.GetDateTimeNowUTC()
            };
            Create(temp);
        }

        /// <summary>
        /// Data set for demo
        /// </summary>
        private void DataSetDemo()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Unit Test data set
        /// </summary>
        private void DataSetUnitTest()
        {
            DataSetDefault();
        }

        /// <summary>
        /// Set which data to load
        /// </summary>
        /// <param name="setEnum"></param>
        public void LoadDataSet(DataSourceDataSetEnum setEnum)
        {
            switch (setEnum)
            {
                case DataSourceDataSetEnum.Demo:
                    DataSetDemo();
                    break;

                case DataSourceDataSetEnum.UnitTest:
                    DataSetUnitTest();
                    break;

                case DataSourceDataSetEnum.Default:
                default:
                    DataSetDefault();
                    break;
            }
        }
    }
}