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

        // method to add a new video to the BST and database.
        public void AddVideo(Video video)
        {
            // Add video to the BST
            _videoTree.Insert(video);

            // Insert video record into the database
            string query = "INSERT INTO Videos (Title, Genre,ReleaseDate, Availability) VALUES (@Title, @Genre,@ReleaseDate, 1)";
            _dataAccess.ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Title", video.Title);
                cmd.Parameters.AddWithValue("@Genre", video.Genre);
                cmd.Parameters.AddWithValue("@ReleaseDate", video.ReleaseDate);
            });

            Console.WriteLine("Video added successfully!");
        }

        // method to remove a video from the BST and database.
        public void RemoveVideo(int videoId)
        {
            // Remove video from BST
            bool removed = _videoTree.Delete(videoId);

            if (!removed)
            {
                Console.WriteLine("Video not found.");
                return;
            }

            // Remove video record from the database
            string query = "DELETE FROM Videos WHERE VideoID = @ID";
            _dataAccess.ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@ID", videoId);
            });

            Console.WriteLine("Video removed successfully!");
        }


        // method to update video availability in the BST and database.
        public void UpdateVideoAvailability(int videoId, bool availability)
        {
            Video video = _videoTree.Search(videoId);
            if (video != null)
            {
                video.Availability = availability;
                _videoTree.Update(videoId, video);
                // Update video availability in the database
                string query = "UPDATE Videos SET Availability = @Availability WHERE VideoID = @ID";
                _dataAccess.ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Availability", availability);
                    cmd.Parameters.AddWithValue("@ID", videoId);
                });
                Console.WriteLine("Video availability updated successfully!");
            }
            else
            {
                Console.WriteLine("Video with ID " + videoId + " not found.");
            }
        }
    }
}
