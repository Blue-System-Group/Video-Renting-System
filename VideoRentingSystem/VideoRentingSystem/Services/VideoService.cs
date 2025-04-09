using System.Data;
using System;
using VideoRentingSystem.Data;
using VideoRentingSystem.DataStructures;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.Services
{
    public class VideoService
    {
        private readonly DataAccess _dataAccess; // Data access layer for database operations
        private readonly VideoBST _videoTree; // Binary Search Tree for video management

        // constructor
        public VideoService()
        {
            _dataAccess = new DataAccess();
            _videoTree = VideoBST.Instance;
        }

        // method to load video data from the database into the BST.
        public void LoadData()
        {
            //before loading data, clear the BST
            _videoTree.Clear();
            DataTable videoTable = _dataAccess.GetData("SELECT * FROM Videos");
            foreach (DataRow row in videoTable.Rows)
            {
                Video video = new Video
                {
                    ID = Convert.ToInt32(row["VideoID"]),
                    Title = row["Title"].ToString(),
                    Genre = row["Genre"].ToString(),
                    ReleaseDate = Convert.ToDateTime(row["ReleaseDate"]),
                    Availability = Convert.ToBoolean(row["Availability"])
                };
                _videoTree.Insert(video);  // Add to BST
            }
            //Console.WriteLine("Data loaded successfully!");
        }
    }
}
