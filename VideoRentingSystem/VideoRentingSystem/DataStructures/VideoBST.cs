using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentingSystem.Models;

namespace VideoRentingSystem.DataStructures
{
    public class VideoNode
    {
        public Video Video { get; set; }
        public VideoNode Left { get; set; }
        public VideoNode Right { get; set; }

        public VideoNode(Video video)
        {
            Video = video;
            Left = null;
            Right = null;
        }
    }

    public class VideoBST
    {
        // Singleton instance
        private static readonly VideoBST instance = new VideoBST();
        private VideoNode _root;

        // Private constructor to prevent instantiationS
        public VideoBST()
        {
            _root = null;
        }

        // instance to access the singleton
        public static VideoBST Instance
        {
            get { return instance; }
        }

        // Clear the tree
        public void Clear()
        {
            _root = null;
        }

        // Insert a new video
        public void Insert(Video video)
        {
            _root = InsertRec(_root, video);
        }


        // Recursive insert function
        private VideoNode InsertRec(VideoNode root, Video video)
        {
            if (root == null)
                return new VideoNode(video);

            if (video.ID < root.Video.ID)
                root.Left = InsertRec(root.Left, video);
            else if (video.ID > root.Video.ID)
                root.Right = InsertRec(root.Right, video);

            return root;
        }

        // Update a video
        public VideoNode Update(int videoId, Video video)
        {
            VideoNode updatedNode = null;
            _root = UpdateRec(_root, videoId, video, ref updatedNode);
            return updatedNode;
        }

        // Recursive update function
        private VideoNode UpdateRec(VideoNode root, int videoId, Video video, ref VideoNode updatedNode)
        {
            if (root == null)
                return null;  // Video not found
            if (videoId < root.Video.ID)
                root.Left = UpdateRec(root.Left, videoId, video, ref updatedNode);
            else if (videoId > root.Video.ID)
                root.Right = UpdateRec(root.Right, videoId, video, ref updatedNode);
            else
            {
                // Found the video
                root.Video = video;
                updatedNode = root;
            }
            return root;
        }

        // Delete a video
        public bool Delete(int videoId)
        {
            bool isDeleted = false;
            _root = DeleteRec(_root, videoId, ref isDeleted);
            return isDeleted;
        }

        // Recursive delete function
        private VideoNode DeleteRec(VideoNode root, int videoId, ref bool isDeleted)
        {
            if (root == null)
                return null;  // Video not found

            if (videoId < root.Video.ID)
                root.Left = DeleteRec(root.Left, videoId, ref isDeleted);
            else if (videoId > root.Video.ID)
                root.Right = DeleteRec(root.Right, videoId, ref isDeleted);
            else
            {
                // Found the video
                isDeleted = true;

                // Node with only one child or no child
                if (root.Left == null)
                    return root.Right;
                if (root.Right == null)
                    return root.Left;

                // Node with two children
                root.Video = MinValue(root.Right);
                root.Right = DeleteRec(root.Right, root.Video.ID, ref isDeleted);
            }

            return root;
        }

        // Find the minimum value node
        private Video MinValue(VideoNode root)
        {
            while (root.Left != null)
                root = root.Left;
            return root.Video;
        }

        // Search for a video
        public Video Search(int videoId)
        {
            VideoNode resultNode = SearchRec(_root, videoId);
            if (resultNode != null)
            {
                Console.WriteLine("Video Found: ID = " + resultNode.Video.ID
                                + ", Title = " + resultNode.Video.Title
                                + ", Genre = " + resultNode.Video.Genre
                                + ", Release Date = " + resultNode.Video.ReleaseDate
                                + ", Availability = " + resultNode.Video.Availability);
                return resultNode.Video;
            }
            else
            {
                Console.WriteLine("Video with ID " + videoId + " not found.");
                return null;
            }
        }

        //display all videos
        public void DisplayAllVideos()
        {
            InOrderTraversal(video => Console.WriteLine("VideoID :" + video.ID + " |Title :" + video.Title + " |Genre :" + video.Genre + " |Release Date :" + video.ReleaseDate + " |Availability :" + video.Availability));
        }

        // Search for a video recursively
        private VideoNode SearchRec(VideoNode root, int videoId)
        {
            if (root == null)
                return null;

            if (videoId == root.Video.ID)
                return root;

            if (videoId < root.Video.ID)
                return SearchRec(root.Left, videoId);

            return SearchRec(root.Right, videoId);
        }

        // Display all videos
        public void InOrderTraversal(Action<Video> action)
        {
            InOrderTraversalRec(_root, action);
        }

        // In-order traversal of the tree
        private void InOrderTraversalRec(VideoNode root, Action<Video> action)
        {
            if (root != null)
            {
                InOrderTraversalRec(root.Left, action);
                action(root.Video);
                InOrderTraversalRec(root.Right, action);
            }
        }
    }
}
